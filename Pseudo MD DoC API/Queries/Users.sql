delete from users where emailaddress in ('test@test.com','rtheil@technofamily.net')
select * from users
update users set accountVerified=1

select * from configuration

insert into configuration (propertyName,propertyValue) values ('newUserFromAddress','rtheil@codirt.com')
insert into configuration (propertyName,propertyValue) values ('newUserFromName','md-doc-web')
insert into configuration (propertyName,propertyValue) values ('newUserSubject','Please verify your account')
insert into configuration (propertyName,propertyValue) values ('newUserUrl','http://localhost:3000/login/register')
insert into configuration (propertyName,propertyValue) values ('newUserContent','Click this link to verify your account: [newUserUrl]')