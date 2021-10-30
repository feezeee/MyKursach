SET SQL_SAFE_UPDATES = 0;

CREATE DATABASE postal_office;

USE postal_office;

CREATE TABLE Workers (
Id int primary key not null auto_increment,
FirstName varchar(30) not null,
LastName varchar(30) not null,
DateOfBirth date not null,
PhoneNumber varchar(19) not null,
Email varchar(50) null,
PositionId int not null,
Password varchar(50) not null
);

CREATE TABLE Positions (
Id int primary key not null auto_increment,
PositionName varchar(30) not null
);

CREATE TABLE Operations (
id int primary key not null auto_increment,
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

CREATE TABLE GoodsForSale (
id int primary key not null auto_increment,
product_name varchar(30) not null,
storage_quantity int not null
);

CREATE TABLE Providers (
id int primary key not null auto_increment,
full_name varchar(50) not null,
phone_number varchar(15) not null,
email varchar(50) null
);

CREATE TABLE Providers_GoodsForSale (
id int primary key not null auto_increment,
provider_id int not null,
goodForSale_id int not null
);

CREATE TABLE PaymentMethods (
id int primary key not null auto_increment,
payment_method varchar(20) not null
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

CREATE TABLE DeliveryCountries (
id int primary key not null auto_increment,
countries varchar(20) not null
);

CREATE TABLE AvailablePayments (
id int primary key not null auto_increment,
payment_name varchar(30) not null
);

CREATE TABLE MakingPayments (
id int primary key not null auto_increment,
operation_id int not null,
paymentMethod_id int not null,
availablePayment_id int not null
);


INSERT INTO Positions (PositionName)
	VALUES 
    ("Администратор"),
    ("Директор"),
    ("Кассир"),
    ("Почтальон");
    
INSERT INTO Workers (FirstName, LastName, DateOfBirth, PhoneNumber, Email, PositionId, Password)
	VALUES 
    ("Админ", "Админович", "2001-04-23", "+375 (29) 830-63-61", "krut1@yandex.com", 1, "root")



INSERT INTO Workers (FirstName, LastName, DateOfBirth, Email, PositionId, PhoneNumber)
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


















