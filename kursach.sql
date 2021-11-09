SET SQL_SAFE_UPDATES = 0;

CREATE DATABASE postal_office;

USE postal_office;

CREATE TABLE workers (
worker_id int primary key not null auto_increment,
first_name varchar(30) not null,
last_name varchar(30) not null,
date_of_birth date not null,
phone_number varchar(19) not null,
email varchar(50) null,
gender_id int not null,
position_id int not null,
password varchar(50) not null
);

INSERT INTO workers (first_name, last_name, gender_id, date_of_birth, phone_number, email, position_id, password)
	VALUES 
    ("Админ", "Админович",1 ,"2001-04-23", "+375 (29) 111-11-11", "krut1@yandex.com", 1, "root"),
    ("Максим", "Сащеко",1 ,"2001-04-23", "+375 (29) 358-17-24", "krut1@yandex.com", 2, "root"),
    ("Марцев", "Артем",1 ,"2001-04-23", "+375 (29) 222-22-22", "krut1@yandex.com", 3, "root"),
    ("Денис", "Скурат",1 ,"2001-04-23", "+375 (29) 830-63-61", "krut1@yandex.com", 4, "root");


CREATE TABLE positions (
position_id int primary key not null auto_increment,
position_name varchar(30) not null
);

INSERT INTO positions (position_name)
	VALUES 
    ("Администратор"),
    ("Директор"),
    ("Кассир"),
    ("Почтальон");


CREATE TABLE genders (
gender_id int primary key not null auto_increment,
gender_name varchar(10) not null
);

INSERT INTO genders (gender_name)
	VALUES 
    ("Мужчина"),
    ("Женщина");


CREATE TABLE operations (
operation_id int primary key not null auto_increment,
operation_date_time datetime not null,
worker_id int not null
);


CREATE TABLE SaleGoods (
id int primary key not null auto_increment,
number_sold int not null,
operation_id int not null,
paymentMethod_id int not null,
goodForSale_id int not null
);

CREATE TABLE goodsforsale (
goodsforsale_id int primary key not null auto_increment,
goodsforsale_name varchar(30) not null,
quantity_in_stock int not null
);

CREATE TABLE providers (
provider_id int primary key not null auto_increment,
provider_name varchar(50) not null,
phone_number varchar(30) not null,
email varchar(50) null
);

INSERT INTO providers (provider_name, phone_number, email)
	VALUES 
    ("ООО Тест", "тест", "test"),
    ("ООО Родничок", "тест", "test"),
    ("ООО БГУИР", "тест", "test"),
    ("ООО БНТУ", "тест", "test");

CREATE TABLE goodsforsale_providers (
goodsforsale_providers_id int primary key not null auto_increment,
goodsforsale_id int not null,
providers_id int not null
);

CREATE TABLE paymentmethods (
paymentmethod_id int primary key not null auto_increment,
paymentmethod_name varchar(20) not null
);

CREATE TABLE DeliveryGoods (
id int primary key not null auto_increment,
delivery_address varchar(50) not null,
customers_first_name varchar(30) not null,
customers_last_name varchar(30) not null,
operation_id int not null,
paymentMethod_id int not null,
deliveryCountries_id int not null
);

CREATE TABLE deliverycountries (
deliverycountry_id int primary key not null auto_increment,
deliverycountry_name varchar(20) not null
);

CREATE TABLE availablepayments (
availablepayment_id int primary key not null auto_increment,
availablepayment_name varchar(30) not null
);

CREATE TABLE MakingPayments (
id int primary key not null auto_increment,
operation_id int not null,
paymentMethod_id int not null,
availablePayment_id int not null
);


INSERT INTO Position (PositionName)
	VALUES 
    ("Администратор"),
    ("Директор"),
    ("Кассир"),
    ("Почтальон");
    
    
    

INSERT INTO Gender (GenderName)
	VALUES 
    ("Мужчина"),
    ("Женщина");

INSERT INTO Workers (first_name, last_name, date_of_birth, email, position_id, phone_number)
	VALUES 
		("Максим", "Сащеко", "2001-04-23", "krut1@yandex.com", 1, "+375 (29) 830-63-61"),
		("Артем", "Марцев", "2001-04-23", "krut2@yandex.com", 2,"+375 (29) 830-63-61"),
		("Никита", "Шуринов", "2001-04-23", "krut3@yandex.com", 3,"+375 (29) 830-63-61"),
		("Денис", "Скурат", "2001-04-23", "krut4@yandex.com", 2,"+375 (29) 830-63-61"),
        ("Максим", "Синяга", "2001-04-23", "krut4@yandex.com", 3,"+375 (29) 830-63-61");
        
            
UPDATE worker SET first_name = 'Рома' WHERE first_name = 'Максим';

UPDATE worker SET last_name = 'Суриков' WHERE last_name = 'Синяга';

UPDATE worker SET last_name = '2222-02-22' WHERE id > 2;



SELECT * FROM worker;
SELECT first_name FROM worker;
SELECT last_name FROM worker;
SELECT last_name, first_name FROM worker;

SELECT * FROM worker ORDER BY position_id;

SELECT * FROM worker ORDER BY position_id DESC;

DELETE FROM worker WHERE position_id = 3


















