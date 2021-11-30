CREATE DEFINER=`root`@`localhost` PROCEDURE `get_available_payments`()
BEGIN
	SELECT * FROM available_payments
    ORDER BY `available_payments`.`available_payment_id`;
END