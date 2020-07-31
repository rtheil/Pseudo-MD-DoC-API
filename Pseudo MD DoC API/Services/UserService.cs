using Pseudo_MD_DoC_API.Persistence;
using Pseudo_MD_DoC_API.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pseudo_MD_DoC_API.Models;
using System.Net.Mail;
using Pseudo_MD_DoC_API.Models.Users;

namespace Pseudo_MD_DoC_API.Services
{
    public interface IUserService
    {
        User Authenticate(string emailAddress, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void UpdatePassword(ResetModel resetModel);
        void Update(User user, string password = null);
        void Delete(int id);
        void ResetPassword(ForgotModel forgotModel);
        void VerifyResetToken(TokenModel token);
        void VerifyRegisterToken(TokenModel token);
    }

    public class UserService : IUserService
    {
        private AppDbContext _context;
        private int passwordExpireMinutes = 30;
        private IConfiguration Configuration { get; }

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        private string Token()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-=";
            string resetToken = new string(Enumerable.Repeat(chars, 128).Select(s => s[random.Next(s.Length)]).ToArray());
            return resetToken;
        }

        private void SendEmail(string configString, string to, string token)
        {
            //get email details from Configuration
            var config = from c in _context.Configuration where c.propertyName.StartsWith(configString) select c;

            //PASSWORD RESET URL FOR EMAIL
            string url = config.Single(x => x.propertyName == configString+"Url").propertyValue + "/" + token;

            //send an email with a password reset link containing token
            EmailService es = new EmailService(_context);
            bool success = es.SendEmail(
                new MailAddress(
                    config.Single(x => x.propertyName == configString+"FromAddress").propertyValue,
                    config.Single(x => x.propertyName == configString + "FromName").propertyValue
                    ),
                to,
                config.Single(x => x.propertyName == configString + "Subject").propertyValue,
                config.Single(x => x.propertyName == configString + "Content").propertyValue.Replace("["+ configString + "Url]", url)
                );
            if (!success) throw new Exception("Could not connect to SMTP server");
        }

        public User Authenticate(string emailAddress, string password)
        {
            if (string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(password))
                return null;

            //try to get user
            var user = _context.Users.SingleOrDefault(x => x.EmailAddress == emailAddress);

            //check if emailAddress exists
            if (user == null)
                return null;

            //check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user, string password)
        {
            // validate password
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            //does account already exist?
            if (_context.Users.Any(x => x.EmailAddress == user.EmailAddress))
                throw new Exception("There is already an account with email \"" + user.EmailAddress + "\"");

            //Hash and set the password
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            //Create verification token
            string token = Token();
            user.AccountVerifyToken = token;

            //Save new user
            _context.Users.Add(user);
            _context.SaveChanges();

            //send verification email
            SendEmail("newUser", user.EmailAddress, user.AccountVerifyToken);

            return user;
        }

        public void ResetPassword(ForgotModel forgotModel)
        {
            //CHECK FOR EMPTY
            if (string.IsNullOrEmpty(forgotModel.EmailAddress))
                throw new Exception("No email address provided");

            User user;
            try
            {
                //Find the user with the email address provided
                user = _context.Users.Single(x => x.EmailAddress == forgotModel.EmailAddress);
            }
            catch
            {
                //Do nothing so that response is 200 regardless of whether or not we found a user
                return;
            }

            //Get random token and save token/expire date to db, good for 30 minutes
            string resetToken = Token();
            user.ResetPasswordToken = resetToken;
            user.ResetPasswordExpires = DateTime.Now.AddMinutes(passwordExpireMinutes);

            //SAVE
            _context.Users.Update(user);
            _context.SaveChanges();

            //send email
            SendEmail("forgotEmail", user.EmailAddress, user.ResetPasswordToken);
            return;
        }

        public void VerifyResetToken(TokenModel token)
        {
            try
            {
                //Find the user based on token provided
                User user = _context.Users.Single(x => x.ResetPasswordToken == token.Token && x.ResetPasswordExpires > DateTime.Now);

                //reset the token and save
                user.ResetPasswordToken = null;
                user.ResetPasswordExpires = null;

                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Invalid token");
            }

        }
        public void VerifyRegisterToken(TokenModel token)
        {
            try
            {
                //Find the user based on token provided
                User user = _context.Users.Single(x => x.AccountVerifyToken == token.Token);

                user.AccountVerified = true;
                user.AccountVerifyToken = null;

                _context.Users.Update(user);
                _context.SaveChanges();

            }
            catch
            {
                throw new Exception("Invalid token");
            }

        }

        public void UpdatePassword(ResetModel resetModel)
        {
            User user;
            try
            {
                //Find the user based on token provided
                user = _context.Users.Single(x => x.ResetPasswordToken == resetModel.Token && x.ResetPasswordExpires > DateTime.Now);
            }
            catch
            {
                throw new Exception("Invalid token");
            }

            //Update the user's password
            Update(user, resetModel.Password);
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new Exception("User not found");

            // update email address if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.EmailAddress) && userParam.EmailAddress != user.EmailAddress)
            {
                // throw error if the new email address is already taken
                if (_context.Users.Any(x => x.EmailAddress == userParam.EmailAddress))
                    throw new Exception("There is already an account with email " + userParam.EmailAddress);

                user.EmailAddress = userParam.EmailAddress;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.Name))
                user.Name = userParam.Name;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.ResetPasswordToken = null;
                user.ResetPasswordExpires = null;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}