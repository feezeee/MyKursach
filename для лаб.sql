CREATE USER 'adminka'@'localhost' identified by 'adminka';
grant all privileges on *.* to 'adminka'@'localhost';
flush privileges;
show grants for 'adminka'@'localhost';


CREATE USER 'onlyselect'@'localhost' identified by 'onlyselect';
grant select on *.* to 'onlyselect'@'localhost';
flush privileges;
show grants for 'onlyselect'@'localhost';


CREATE USER 'onlyselectinsertupdate'@'localhost' identified by 'onlyselectinsertupdate';
grant select, insert, update on *.* to 'onlyselectinsertupdate'@'localhost';
flush privileges;
show grants for 'onlyselectinsertupdate'@'localhost';


alter user 'adminka'@'localhost' identified by '123';

grant select, insert, update on *.* to 'adminka'@'localhost' with 
max_updates_per_hour = 5 
max_connections_per_hour = 10;

select md5('Привет');
select sha1('Привет');


select * From workers 
JOIN positions ON positions.position_id = workers.position_id
WHERE worker_id = 1;  

SELECT * FROM workers JOIN (SELECT * FROM positions WHERE position_id = 1) as test;


CREATE VIEW test1 AS 
SELECT * FROM workers WHERE worker_id <= 2;
SELECT * FROM test1;

CREATE VIEW test2 AS 
SELECT * FROM workers WHERE worker_id > 2;
SELECT * FROM test2;


CREATE VIEW test3 AS 
SELECT * FROM workers WHERE position_id > 2;
SELECT * FROM test3;