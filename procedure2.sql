CREATE DEFINER=`root`@`localhost` PROCEDURE `get_countries_delivery`()
BEGIN
	SELECT * FROM countries_delivery
    ORDER BY `countries_delivery`.`country_delivery_id`;
END