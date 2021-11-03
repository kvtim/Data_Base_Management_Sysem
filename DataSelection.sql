use mydb;

show tables;

SELECT
product_name, 
product_price, 
product_count, 
product_price * product_count AS total_price
FROM product
ORDER BY total_price, product_count DESC;

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
product_count,
product_price 
FROM product
WHERE product_price
BETWEEN 1500 AND 3000;


SELECT
product_count,
AVG(product_price) AS price_sum
FROM product
GROUP BY product_count
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
HAVING products_count >= 3
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


SELECT * FROM customer -- show customers, which have products from 7 category
WHERE customerid IN 
(
	SELECT customer_id FROM `order`
    WHERE orderid IN
    (
		SELECT order_id FROM order_to_product
		WHERE product_id IN
        (
			SELECT productid FROM product 
            WHERE category_id = 7
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


SELECT full_address, short_address FROM address -- show address for delivery for 1st order
WHERE addressid = 
(
	SELECT address_id FROM delivery
    WHERE order_id = 1
);


SELECT * FROM `order` -- show orders for delivery for 4th address
WHERE orderid IN
(
	SELECT order_id FROM delivery
    WHERE address_id = 4
);


SELECT * FROM shop -- show 3st manager shops
WHERE shopid IN
(
	SELECT shop_id FROM manager_to_shop
    WHERE manager_id = 3
);


