CREATE DEFINER=`root`@`localhost` PROCEDURE `get_providers`()
BEGIN
	SELECT * FROM providers
    ORDER BY `providers`.`provider_id`;
END