show databases;
use mydb;
show tables;

-- INSERT

desc address;
select * from address;

INSERT INTO address
VALUES
(NULL, 'Belarus, Minsk, Electronic street, 21', 'Electronic street, 21'),
(NULL, 'Belarus, Minsk, Cars street, 45', 'Cars street, 45'),
(NULL, 'Belarus, Minsk, Sprts street 77', 'Sports street 77');

select * from address;

desc shop;
select * from shop;

INSERT INTO shop
VALUES
(NULL, 'Electronics shop', 1),
(NULL, 'Cars shop', 2),
(NULL, 'Sports shop', 3);

select * from shop;

desc category;
select * from category;

INSERT INTO category
VALUES
(NULL, 'Phones'),
(NULL, 'Notebooks'),
(NULL, 'Tablets'),
(NULL, 'Cars'),
(NULL, 'Trucks'),
(NULL, 'Snowboards'),
(NULL, 'Sport shoes'),
(NULL, 'Spors clothes');

select * from category;

desc product;
select * from product;

INSERT INTO product
VALUES
(NULL, 'Iphone 12', 1000, 3, 1, 1),
(NULL, 'Iphone 4s', 1500, 1, 1, 1),
(NULL, 'ASUS ZenBook', 2000, 3, 1, 2),
(NULL, 'Apple MacBook', 2500, 2, 1, 2),
(NULL, 'Apple IPad Pro', 900, 6, 1, 3),
(NULL, 'Apple IPad Mini', 700, 5, 1, 3),
(NULL, 'BMW M3', 70000, 3, 2, 4),
(NULL, 'Audi RS6', 80000, 1, 2, 4),
(NULL, 'Mercedes-Benz E63 AMG', 100000, 1, 2, 4),
(NULL, 'MAN TGS', 150000, 2, 2, 5),
(NULL, 'IVECO STRALIS', 170000, 3, 2, 5),
(NULL, 'Burton Blunt', 400, 3, 3, 6),
(NULL, 'DC Shoes Biddy', 500, 2, 3, 6),
(NULL, 'Adidas Campus', 150, 7, 3, 7),
(NULL, 'New Balance 574', 140, 6, 3, 7),
(NULL, 'Adidas Stan Smith', 160, 5, 3, 7),
(NULL, 'Sports trousers Nike', 100, 7, 3, 8),
(NULL, 'Sports trousers Adidas', 120, 6, 3, 8),
(NULL, 'Sports jersey Adidas', 70, 10, 3, 8),
(NULL, 'Sports jersey New Balance', 60, 12, 3, 8),
(NULL, 'Sport jacket Quicksilver', 170, 15, 3, 8);

select * from product;

desc customer;
select * from customer;

INSERT INTO customer
VALUES
(NULL, 'User1', 'SurUser1', 'user1@gmail.com', '333333', DEFAULT),
(NULL, 'User2', 'SurUser2', 'user2@gmail.com', '555555', DEFAULT),
(NULL, 'User3', 'SurUser3', 'user3@gmail.com', '999999', DEFAULT);

select * from customer;

desc manager;
select * from manager;

INSERT INTO manager
VALUES
(NULL, 'Manager1', 'SurManager1', 'manager1@gmail.com', '444444', DEFAULT),
(NULL, 'Manager2', 'SurManager2', 'manager2@gmail.com', '666666', DEFAULT),
(NULL, 'Manager3', 'SurManager3', 'manager3@gmail.com', '111111', DEFAULT);

select * from manager;

desc manager_to_shop;
select * from manager_to_shop;

INSERT INTO manager_to_shop
VALUES
(1, 1),
(1, 2),
(2, 2),
(3, 1),
(3, 3);

select * from manager_to_shop;

desc `order`;
select * from `order`;

INSERT INTO `order`
VALUES
(NULL, 'Order for User1', 'formalized', DEFAULT, 0, 1),
(NULL, 'Order for User2', 'formalized', DEFAULT, 0, 2),
(NULL, 'Order for User3', 'formalized', DEFAULT, 0, 3);

select * from `order`;

desc order_to_product;
select * from order_to_product;

INSERT INTO order_to_product
VALUES
(1, 3),
(1, 15),
(1, 6),
(2, 18),
(2, 10),
(2, 15),
(3, 10),
(3, 17),
(3, 20),
(3, 12),
(3, 3);

select * from order_to_product;

INSERT INTO address
VALUES
(NULL, 'Belarus, Minsk, Delivery street, 10', 'Delivery street, 10'),
(NULL, 'Belarus, Minsk, Some street, 39', 'Some street, 39');

select * from address;

desc delivery;
select * from delivery;

INSERT INTO delivery
VALUES
(NULL, 'Delivery for User1', 1, 4),
(NULL, 'Delivery for User3', 3, 5);

select * from delivery;


-- UPDATE

select * from `order`;

UPDATE `order`
SET order_status = 'sent'
WHERE orderid = 1;

UPDATE `order`
SET order_status = 'sent'
WHERE orderid = 3;

select * from `order`;

UPDATE `order`
SET order_price = order_price + 5000
WHERE orderid = 1;

UPDATE `order`
SET order_price = order_price + 3000
WHERE orderid = 2;

UPDATE `order`
SET order_price = order_price + 7000
WHERE orderid = 3;

UPDATE `order`
SET order_price = order_price + 5000
WHERE orderid = 2;

select * from `order`;

select * from address;

INSERT INTO address
VALUES
(NULL, 'Belarus, Minsk, Some Unknown street, 39', 'Some Unknown street, 39');

select * from address;

REPLACE INTO address
VALUES (6, 'Belarus, Minsk, Some Delivery street, 39', 'Belarus, Minsk, Some Delivert street, 39');

select * from address;

select * from delivery;

UPDATE delivery
SET address_id = 6
WHERE deliveryid = 2;

select * from delivery;

select * from product;

UPDATE product
SET product_price = 750
WHERE productid = 6;

select * from product;

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

select * from address;

DELETE FROM address -- delete the last field
ORDER BY addressid DESC
LIMIT 1;

select * from address;

INSERT INTO shop
VALUES
(NULL, 'Some shop', 1);

select * from shop;

INSERT INTO category
VALUES
(NULL, 'Some category');

select * from category;

INSERT INTO product
VALUES
(NULL, 'Some product', 1000000, 300, 4, 9);

select * from product;

INSERT INTO manager
VALUES
(NULL, 'Manager4', 'SurManager4', 'manager4@gmail.com', '123456', DEFAULT);

INSERT INTO manager_to_shop
VALUES
(4, 1),
(4, 2);

select * from manager;
select * from manager_to_shop;

DELETE FROM manager -- manager_to_shop will be deleted
WHERE managerid = 4;

DELETE FROM shop -- product will be deleted
WHERE shopid = 4;

DELETE FROM category
WHERE categoryid = 9;

select * from manager;

select * from manager_to_shop;

select * from shop;

select * from category;

select * from product;


