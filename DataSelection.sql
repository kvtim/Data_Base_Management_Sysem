use mydb;

show tables;

DROP PROCEDURE IF EXISTS select_all_customers;

DELIMITER //
CREATE PROCEDURE select_all_customers()
BEGIN
	SELECT * FROM customer;
END//

DELIMITER ;

CALL select_all_customers();


DROP PROCEDURE IF EXISTS select_customer;

DELIMITER //
CREATE PROCEDURE select_customer(IN id INT)
BEGIN
	SELECT * FROM customer
    WHERE customerid = id;
END//

DELIMITER ;

CALL select_customer(1);


DROP PROCEDURE IF EXISTS select_all_categories;

DELIMITER //
CREATE PROCEDURE select_all_categories()
BEGIN
	SELECT * FROM category;
END//

DELIMITER ;

CALL select_all_categories();


DROP PROCEDURE IF EXISTS select_category_by_id;

DELIMITER //
CREATE PROCEDURE select_category_by_id(IN c_id INT)
BEGIN
	SELECT * FROM category
    WHERE categoryid = c_id;
END//

DELIMITER ;

CALL select_category_by_id(1);


DROP PROCEDURE IF EXISTS select_brand_by_id;

DELIMITER //
CREATE PROCEDURE select_brand_by_id(IN b_id INT)
BEGIN
	SELECT * FROM brand
    WHERE brandid = b_id;
END//

DELIMITER ;

CALL select_brand_by_id(1);


DROP PROCEDURE IF EXISTS select_all_brands;

DELIMITER //
CREATE PROCEDURE select_all_brands()
BEGIN
	SELECT * FROM brand;
END//

DELIMITER ;

CALL select_all_brands();


DROP PROCEDURE IF EXISTS select_brands_of_the_same_category;

DELIMITER //
CREATE PROCEDURE select_brands_of_the_same_category(IN c_id INT)
BEGIN
	SELECT * FROM brand
    WHERE brandid IN
    (SELECT brand_id
		FROM brand_to_category
        WHERE category_id = c_id);
END//

DELIMITER ;

CALL select_brands_of_the_same_category(1);


DROP PROCEDURE IF EXISTS select_categories_of_the_same_brand;

DELIMITER //
CREATE PROCEDURE select_categories_of_the_same_brand(IN b_id INT)
BEGIN
	SELECT * FROM category
    WHERE categoryid IN
    (SELECT category_id
		FROM brand_to_category
        WHERE brand_id = b_id);
END//

DELIMITER ;

CALL select_categories_of_the_same_brand(1);


DROP PROCEDURE IF EXISTS select_brand_to_category;

DELIMITER //
CREATE PROCEDURE select_brand_to_category(IN b_id INT, IN c_id INT)
BEGIN
	SELECT * FROM brand_to_category
    WHERE brand_id = b_id
    AND category_id = c_id;
END//

DELIMITER ;

CALL select_brand_to_category(1, 1);


DROP PROCEDURE IF EXISTS select_product_by_id;

DELIMITER //
CREATE PROCEDURE select_product_by_id(IN p_id INT)
BEGIN
	SELECT * FROM product
    WHERE productid = p_id;
END//

DELIMITER ;

CALL select_product_by_id(1);


DROP PROCEDURE IF EXISTS select_full_product;

DELIMITER //
CREATE PROCEDURE select_full_product(IN p_id INT)
BEGIN
	SELECT p.productid, p.product_name, p.product_description,
    p.product_price, c.category_name, b.brand_name
    FROM product AS p
    JOIN category AS c
    ON p.category_id = c.categoryid
    JOIN brand AS b
    ON p.brand_id = b.brandid
    WHERE productid = p_id;
END//

DELIMITER ;

CALL select_full_product(1);


DROP PROCEDURE IF EXISTS select_products_of_one_brand;

DELIMITER //
CREATE PROCEDURE select_products_of_one_brand(IN b_id INT)
BEGIN
	SELECT * FROM product
    WHERE brand_id = b_id;
END//

DELIMITER ;

CALL select_products_of_one_brand(1);


DROP PROCEDURE IF EXISTS select_products_of_one_category;

DELIMITER //
CREATE PROCEDURE select_products_of_one_category(IN c_id INT)
BEGIN
	SELECT * FROM product
    WHERE category_id = c_id;
END//

DELIMITER ;

CALL select_products_of_one_category(1);


DROP PROCEDURE IF EXISTS select_products_of_one_category_and_brand;

DELIMITER //
CREATE PROCEDURE select_products_of_one_category_and_brand(IN c_id INT, IN b_id INT)
BEGIN
	SELECT * FROM product
    WHERE category_id = c_id
    AND brand_id = b_id;
END//

DELIMITER ;

CALL select_products_of_one_category_and_brand(1, 2);


DROP PROCEDURE IF EXISTS select_customer_id_by_email;

DELIMITER //
CREATE PROCEDURE select_customer_id_by_email(IN c_email VARCHAR(255))
BEGIN
	SELECT customerid FROM customer
    WHERE customer_email = c_email;
END//

DELIMITER ;

CALL select_customer_id_by_email('user2@gmail.com');


DROP PROCEDURE IF EXISTS select_customer_by_email;

DELIMITER //
CREATE PROCEDURE select_customer_by_email(IN c_email VARCHAR(255))
BEGIN
	SELECT * FROM customer
    WHERE customer_email = c_email;
END//

DELIMITER ;

CALL select_customer_by_email('user2@gmail.com');


DROP PROCEDURE IF EXISTS select_customer_by_email_and_password;

DELIMITER //
CREATE PROCEDURE select_customer_by_email_and_password(IN c_email VARCHAR(255),
IN c_password VARCHAR(32))
BEGIN
	SELECT * FROM customer
    WHERE customer_email = c_email
    AND customer_password = c_password;
END//

DELIMITER ;

CALL select_customer_by_email_and_password('user2@gmail.com', '555555');


DROP PROCEDURE IF EXISTS select_role_name;

DELIMITER //
CREATE PROCEDURE select_role_name(IN r_id INT)
BEGIN
	SELECT role_name FROM role
    WHERE roleid = r_id;
END//

DELIMITER ;

CALL select_role_name(2);


DROP PROCEDURE IF EXISTS select_last_order_id;

DELIMITER //
CREATE PROCEDURE select_last_order_id()
BEGIN
	SELECT orderid FROM `order`
    ORDER BY orderid DESC
    LIMIT 1;
END//

DELIMITER ;

CALL select_last_order_id();


DROP PROCEDURE IF EXISTS select_customer_full_orders;

DELIMITER //
CREATE PROCEDURE select_customer_full_orders(IN c_id INT)
BEGIN
	SELECT o.orderid, o.order_name, o.order_status, o.order_price, o.order_time, d.address, 
	o_t_p.product_count, p.product_name, p.product_price
	FROM `order` AS o
	JOIN delivery AS d
	ON d.order_id = o.orderid
	JOIN order_to_product AS o_t_p
	ON o_t_p.order_id = o.orderid
	JOIN product AS p
	ON p.productid = o_t_p.product_id
	WHERE o.customer_id = c_id
    ORDER BY o.orderid DESC;
END//

DELIMITER ;

CALL select_customer_full_orders(4);


DROP PROCEDURE IF EXISTS select_all_full_orders;

DELIMITER //
CREATE PROCEDURE select_all_full_orders()
BEGIN
	SELECT o.orderid, o.order_name, o.order_status, o.order_price, o.order_time, d.address, 
	o_t_p.product_count, p.product_name, p.product_price
	FROM `order` AS o
	JOIN delivery AS d
	ON d.order_id = o.orderid
	JOIN order_to_product AS o_t_p
	ON o_t_p.order_id = o.orderid
	JOIN product AS p
	ON p.productid = o_t_p.product_id
    ORDER BY o.orderid DESC;
END//

DELIMITER ;

CALL select_all_full_orders();
    
    
    
CREATE FULLTEXT INDEX idx ON product(product_name, product_description);


DROP PROCEDURE IF EXISTS select_products_found;

DELIMITER //
CREATE PROCEDURE select_products_found(IN search_text VARCHAR(200))
BEGIN
	SELECT * FROM product
	WHERE MATCH (product_name, product_description)
	AGAINST (search_text IN NATURAL LANGUAGE MODE);
END//

DELIMITER ;

CALL select_products_found('Great');
    

SELECT * FROM customer
WHERE role_id = 2
ORDER BY customer_name DESC;

SELECT -- show products from order with id 3
product_id
FROM order_to_product
WHERE order_id = 3;

SELECT category_name
FROM category
WHERE category_name IN (
    'Phones',
    'Snowboards',
    'Something',
    'Cars'); -- find this categories


SELECT customer_name, customer_email
FROM customer
WHERE customer_email LIKE '_____@gmail.com';

SELECT customer_name, customer_email
FROM customer
WHERE customer_email LIKE '%3@_____.%';

SELECT
product_name,
product_description,
product_price 
FROM product
WHERE product_price
BETWEEN 1500 AND 3000;


SELECT
brand_id,
AVG(product_price) AS price_sum
FROM product
GROUP BY brand_id
ORDER BY price_sum DESC;

SELECT
category_id,
COUNT(*) AS products_count
FROM product
GROUP BY category_id
ORDER BY category_id;


SELECT
category_id,
COUNT(*) AS products_count
FROM product
GROUP BY category_id
HAVING products_count >= 2
ORDER BY category_id;

SELECT * FROM `order`
WHERE orderid NOT IN (SELECT order_id FROM delivery);

SELECT * FROM `order`
WHERE orderid IN (SELECT order_id FROM delivery);

SELECT * FROM customer -- show customers, which wait a delivery
WHERE customerid IN 
(
	SELECT customer_id FROM `order`
	WHERE orderid IN (SELECT order_id FROM delivery)
);


SELECT * FROM customer -- show customers, which have products from 3 category
WHERE customerid IN 
(
	SELECT customer_id FROM `order`
    WHERE orderid IN
    (
		SELECT order_id FROM order_to_product
		WHERE product_id IN
        (
			SELECT productid FROM product 
            WHERE category_id = 3
        )
    )
);

SELECT * FROM product -- show sent products for 1st customer
WHERE productid IN
(
	SELECT product_id FROM order_to_product
    WHERE order_id IN 
    (
		SELECT orderid FROM `order`
        WHERE customer_id = 1 AND order_status = 'sent'
    )
);

SELECT * FROM `order` -- show orders for delivery for 'Delivry street'
WHERE orderid IN
(
	SELECT order_id FROM delivery
    WHERE address = 'Delivery street, 10'
);


SELECT * FROM brand -- show brands in 3rd category
WHERE brandid IN
(
	SELECT brand_id FROM brand_to_category
    WHERE category_id = 3
);


