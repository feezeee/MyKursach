CREATE DEFINER=`root`@`localhost` FUNCTION `get_by_id_price_good`(id INT) RETURNS int
    READS SQL DATA
    DETERMINISTIC
BEGIN
DECLARE myVar INT;
SET myVar = (SELECT good_price FROM goods_for_sale WHERE good_for_sale_id = id);
RETURN myVar;
END