SET SQL_SAFE_UPDATES = 0;
USE postal_office;

INSERT INTO positions (position_name)
	VALUES 
    ("Администратор"),
    ("Директор"),
    ("Старший кассир"),
    ("Младший кассир");
    
INSERT INTO group_users (group_name)
	VALUES 
    ("Администратор"),
    ("Директор"),
    ("Кассир");
    
INSERT INTO countries_delivery (country_delivery_name)
	VALUES 
    ("РБ"),
    ("РФ"),
    ("США");
 
INSERT INTO payment_methods (peyment_method_name)
	VALUES 
    ("Карта"),
    ("Наличка");
    
INSERT INTO available_payments (payment_name)
	VALUES 
    ("Газ"),
    ("Вода");
 
 INSERT INTO workers (worker_last_name, worker_first_name, worker_middle_name, worker_date_of_birth, worker_phone_number, position_id, group_user_id)
	VALUES 
    ("Админович", "Админ", "Админ", "2001-04-23", "+375 (29) 111-11-11", 1, 1),
    ("Сащеко", "Максим", "Андреевич", "2001-04-23", "+375 (29) 358-17-24", 2, 2),
    ("Марцев", "Артем", "Андреевич", "2001-04-23", "+375 (29) 222-22-22", 3, 3),
    ("Денис", "Скурат", "Андреевич", "2001-04-23", "+375 (29) 830-63-61", 4, 3);
    
INSERT INTO `postal_office`.`goods_for_sale`
(`good_name`,`good_amount`,`good_price`)
VALUES
("test", 100, 10);
    
    
INSERT INTO `postal_office`.`operations`
(`operation_date_time`,`worker_id`)
VALUES
("2001-04-23",1);
    
    
INSERT INTO `sold_goods`
(`number_sold`,
`operation_id`,
`good_for_sale_id`)
VALUES
(10,1,1);


DELETE FROM sold_goods WHERE sold_good_id = 2;

  