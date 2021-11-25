show databases;
use mydb;
show tables;

-- INSERT

DROP PROCEDURE IF EXISTS add_brand;

DELIMITER //
CREATE PROCEDURE add_brand(IN b_name VARCHAR(100))
BEGIN
	INSERT INTO brand
	VALUES
	(NULL, b_name);
END//

DELIMITER ;

CALL add_brand('Apple');
CALL add_brand('Xiaomi');
CALL add_brand('ASUS');
CALL add_brand('BMW');
CALL add_brand('Audi');
CALL add_brand('Huawei');



DROP PROCEDURE IF EXISTS add_category;

DELIMITER //
CREATE PROCEDURE add_category(IN c_name VARCHAR(45))
BEGIN
	INSERT INTO category
	VALUES
	(NULL, c_name);
END//

DELIMITER ;

CALL add_category('Phones');
CALL add_category('Notebooks');
CALL add_category('Cars');


DROP PROCEDURE IF EXISTS add_brand_to_category;

DELIMITER //
CREATE PROCEDURE add_brand_to_category(IN b_id INT, IN c_id INT)
BEGIN
	INSERT INTO brand_to_category
	VALUES
	(b_id, c_id);
END//

DELIMITER ;

CALL add_brand_to_category(1, 1);
CALL add_brand_to_category(1, 2);
CALL add_brand_to_category(2, 1);
CALL add_brand_to_category(3, 2);
CALL add_brand_to_category(5, 3);
CALL add_brand_to_category(4, 3);


DROP PROCEDURE IF EXISTS add_product;

DELIMITER //
CREATE PROCEDURE add_product(IN p_name VARCHAR(45), IN p_description VARCHAR(200),
IN p_price INT, c_id INT, b_id INT)
BEGIN
	INSERT INTO product
	VALUES
	(NULL, p_name, p_description, p_price, c_id, b_id);
END//

DELIMITER ;

CALL add_product('Iphone 12', 'Good phone!', 1000, 1, 1);
CALL add_product('Xiaomi Redmi', 'Not good phone', 1500, 1, 2);
CALL add_product('ASUS ZenBook', 'Good notebook!', 2000, 2, 3);
CALL add_product('Apple MacBook', 'Good nout!', 2500, 2, 1);
CALL add_product('BMW M3', 'Good car!', 70000, 3, 4);
CALL add_product('Audi RS6', 'fast car!', 80000, 3, 5);

DROP PROCEDURE IF EXISTS add_role;

DELIMITER //
CREATE PROCEDURE add_role(IN n VARCHAR(45))
BEGIN
	INSERT INTO role
	VALUES
	(NULL, n);
END//

DELIMITER ;

CALL add_role('user');
CALL add_role('admin');


DROP PROCEDURE IF EXISTS add_customer;

DELIMITER //
CREATE PROCEDURE add_customer(IN c_name VARCHAR(45), IN c_surname VARCHAR(45),
IN c_email VARCHAR(255), IN c_password VARCHAR(32))
BEGIN
	INSERT INTO customer
	VALUES
	(NULL, c_name, c_surname, c_email, c_password, DEFAULT, DEFAULT);
END//

DELIMITER ;

CALL add_customer('Admin', 'SurAdmin', 'admin@gmail.com', '333333');
CALL add_customer('User2', 'SurUser2', 'user2@gmail.com', '555555');
CALL add_customer('User3', 'SurUser3', 'user3@gmail.com', '999999');


DROP PROCEDURE IF EXISTS add_order;

DELIMITER //
CREATE PROCEDURE add_order(IN o_name VARCHAR(45),
 IN o_price INT, IN customer_id INT)
BEGIN
	INSERT INTO `order`
	VALUES
	(NULL, o_name, DEFAULT, DEFAULT, o_price, customer_id);
END//

DELIMITER ;

CALL add_order('Order for User1', 0, 2);
CALL add_order('Order for User2', 0, 2);
CALL add_order('Order for User3', 0, 3);


DROP PROCEDURE IF EXISTS add_order_to_product;

DELIMITER //
CREATE PROCEDURE add_order_to_product(IN order_id VARCHAR(45), IN product_id INT, 
IN product_count INT)
BEGIN
	INSERT INTO order_to_product
	VALUES
	(order_id, product_id, product_count);
END//

DELIMITER ;

CALL add_order_to_product(1, 3, 1);
CALL add_order_to_product(1, 5, 3);
CALL add_order_to_product(1, 6, 2);
CALL add_order_to_product(2, 1, 1);
CALL add_order_to_product(2, 3, 1);
CALL add_order_to_product(3, 1, 3);
CALL add_order_to_product(3, 2, 4);
CALL add_order_to_product(3, 3, 1);


DROP PROCEDURE IF EXISTS add_delivery;

DELIMITER //
CREATE PROCEDURE add_delivery(IN d_name VARCHAR(45),
 IN d_address VARCHAR(100), IN order_id INT)
BEGIN
	INSERT INTO delivery
	VALUES
	(NULL, d_name, d_address, order_id);
END//

DELIMITER ;

CALL add_delivery('Delivery for User1', 'Delivery street, 10', 1);
CALL add_delivery('Delivery for User3', 'Some street, 39', 3);


-- UPDATE

DROP PROCEDURE IF EXISTS update_order_status;

DELIMITER //
CREATE PROCEDURE update_order_status(IN o_status ENUM('formalized', 'sent', 'delivered'),
IN o_id INT)
BEGIN
	UPDATE `order`
	SET order_status = o_status
	WHERE orderid = o_id;
END//

DELIMITER ;

CALL update_order_status('sent', 1);
CALL update_order_status('sent', 3);


DROP PROCEDURE IF EXISTS update_add_to_order_price;

DELIMITER //
CREATE PROCEDURE update_add_to_order_price(IN price INT, IN o_id INT)
BEGIN
	UPDATE `order`
	SET order_price = order_price + price
	WHERE orderid = o_id;
END//

DELIMITER ;

CALL update_add_to_order_price(5000, 1);
CALL update_add_to_order_price(3000, 2);
CALL update_add_to_order_price(7000, 3);
CALL update_add_to_order_price(5000, 2);


DROP PROCEDURE IF EXISTS update_product;

DELIMITER //
CREATE PROCEDURE update_product(IN p_id INT, IN p_name VARCHAR(45), IN p_description VARCHAR(200),
IN p_price INT, IN c_id INT, IN b_id INT)
BEGIN
	UPDATE product
	SET 
    product_name = p_name,
    product_description = p_description,
    product_price = p_price,
    category_id = c_id,
    brand_id = b_id
	WHERE productid = p_id;
END//

DELIMITER ;

-- CALL update_product(750, 2);


DROP PROCEDURE IF EXISTS update_product_price;

DELIMITER //
CREATE PROCEDURE update_product_price(IN price INT, IN p_id INT)
BEGIN
	UPDATE product
	SET product_price = price
	WHERE productid = p_id;
END//

DELIMITER ;

CALL update_product_price(750, 2);


DROP PROCEDURE IF EXISTS delete_product;

DELIMITER //
CREATE PROCEDURE delete_product(IN p_id INT)
BEGIN
	DELETE FROM product
	WHERE productid = p_id;
END//

DELIMITER ;

CALL delete_product(300);


DROP PROCEDURE IF EXISTS delete_order;

DELIMITER //
CREATE PROCEDURE delete_order(IN o_id INT)
BEGIN
	DELETE FROM `order`
	WHERE orderid = o_id;
END//

DELIMITER ;

CALL delete_order(1);




select * from delivery;

DELETE FROM delivery -- delete the last field
ORDER BY deliveryid DESC
LIMIT 1;

select * from delivery;

select * from `order`;

UPDATE `order`
SET order_status = "delivered"
WHERE orderid = 3;

select * from `order`;

INSERT INTO brand
VALUES
(NULL, 'Some brand');

INSERT INTO category
VALUES
(NULL, 'Some category');

INSERT INTO brand_to_category
VALUES
(6, 1),
(1, 4);

select * from category;

INSERT INTO product
VALUES
(NULL, 'Some product1', 1000000, 300, 1, 6);

INSERT INTO product
VALUES
(NULL, 'Some product2', 1000000, 300, 4, 4);

select * from product;

DELETE FROM brand -- brand_to_category and SomeProduct2 will be deleted
WHERE brandid = 6;

DELETE FROM category -- brand_to_category and SomeProduct1 will be deleted  
WHERE categoryid = 4;

select * from brand;

select * from category;

select * from product;


