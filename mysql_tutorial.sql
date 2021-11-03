-- Database creation
CREATE DATABASE foo;
CREATE DATABASE IF NOT EXISTS foo;
DROP DATABASE IF EXISTS foo; -- delete database foo if exist
SHOW DATABASES;

CREATE DATABASE 'foo'
DEFAULT CHARACTER SET utf8 -- choose encoding
COLLATE utf8_general_ci;

CREATE SCHEMA  foo; -- the same 'CREATE DATABASE foo'
DROP SCHEMA foo;


-- table creation
CREATE DATABASE foo;
USE foo;

-- simple table
SHOW TABLES;
DROP TABLE IF EXISTS table1; -- delete table table1 if exsist
CREATE TABLE IF NOT EXISTS table1 (
    id INT
);
DESC table1; -- show table1 data

-- some table
DROP TABLE IF EXISTS table2;
CREATE TABLE table2 (
    id INT PRIMARY KEY AUTO_INCREMENT,
    txt VARCHAR(100) NOT NULL DEFAULT 'text',
    txt2 VARCHAR(100) COMMENT 'text2'
);
SHOW TABLES;
DESC table2;

-- temporary table
-- doesn't show in tables (SHOW TABLES)
-- after restart server(\r) will delete
DROP TEMPORARY TABLE IF EXISTS table3;
CREATE TEMPORARY TABLE table3 (
    id INT PRIMARY KEY AUTO_INCREMENT
);
SHOW TABLES;
DESC table3;

-- auto increment will start after 100 position
DROP TABLE IF EXISTS table4;
CREATE TABLE table4 (
    id INT PRIMARY KEY AUTO_INCREMENT,
    txt VARCHAR(100) NOT NULL DEFAULT 'text',
    txt2 VARCHAR(100) COMMENT 'text2'
) AUTO_INCREMENT 100;
SHOW TABLES;
DESC table4;
SHOW CREATE TABLE table4; -- show table4 info

-- create table5 like table mysql.user(with the same rows user, host at table mysql.user and !copy rows data!)
DROP TABLE IF EXISTS table5;
CREATE TABLE table5
AS SELECT user, host FROM mysql.user;
SHOW TABLES;
DESC table5;

SELECT user, host FROM table5; -- show rows user and host data

-- create table6 like table5, but !doesn't copy rows data! copy only structure
DROP TABLE IF EXISTS table6;
CREATE TABLE table6
LIKE table5;
SHOW TABLES;
DESC table6;

SELECT user, host FROM table6;


-- data types
DROP TABLE IF EXISTS table7;
CREATE TABLE table7 (
    id INT PRIMARY KEY AUTO_INCREMENT,
    col1 FLOAT DEFAULT 0,
    col2 DECIMAL(7,3), -- 4 signs before ',' and 3 signs after ',' (all, after ,)
    col3 SMALLINT UNSIGNED, -- only positive numbers
    col4 TIME DEFAULT '100:30:30',
    col5 DATE DEFAULT '2019-03-22',
    col6 CHAR(10), -- static bytes count for 10 symbols, symbols count <= 10
    col7 VARCHAR(100), -- dynamyc bytes count for max 100 symbols (bytes count = bytes count for symbols count(<=100) + 1(length))
    --           1         2           3
    col8 ENUM('седан', 'хэтчбек', 'кроссовер') DEFAULT 'седан', -- 1 byte on one item
    col9 SET('классика', 'детектив', 'романы', 'ужасы'), -- 'классика,детектив,романы,ужасы'
    txt VARCHAR(100) NOT NULL DEFAULT 'text',
    txt2 VARCHAR(100) COMMENT 'text2'
) AUTO_INCREMENT 100;
SHOW TABLES;
DESC table7;


-- SELECT

-- !downald database 'world' from the official mysql site!
SELECT left(name, 10), continent -- left(field, symbols count)
FROM country;

SELECT DISTINCT continent -- show ony unique fields
FROM country;

SELECT DISTINCT continent as cont_name -- set some table name
FROM country;

SELECT DISTINCT continent cont_name -- set some table name, the same as with 'as'
FROM country;

SELECT left(name, 10), population 
FROM country
LIMIT 5; -- only 5 fields

SELECT left(name, 10), population 
FROM country
LIMIT 2, 5; -- skip 2 fields, show 5 fields

SELECT left(name, 10), population 
FROM country
LIMIT 5 OFFSET 2; -- skip 2 fields, show 5 fields

SELECT code, left(name, 10), population 
FROM country
WHERE code = 'AFG'; -- show fields with code 'AFG'

SELECT code, left(name, 10), population 
FROM country
WHERE population > 1e7; -- show fields with population more then 10 000 000

SELECT code, left(name, 10), population 
FROM country
WHERE surfacearea > 1000000; -- show fields with surfacearea more then 1000 000

SELECT code, left(name, 10), population 
FROM country
WHERE 
 surfacearea > 1000000 and
 surfacearea < 4000000; -- show fields with surfacearea more then 1000 000 and less then 4000 000

SELECT code, left(name, 10), population 
FROM country
WHERE surfacearea
 BETWEEN 1000000 and 4000000; -- the same as the previous one
 
SELECT name, population
FROM city
WHERE name LIKE 'Moscow'; -- find cites with name 'Moscow'

SELECT name, population
FROM city
WHERE name LIKE '_osco_'; -- '_' - only !one! any symbol

SELECT name, population
FROM city
WHERE name LIKE '_os___'; -- '_' - only !one! any symbol

SELECT name, population
FROM city
WHERE name LIKE '%w'; -- '%' - any number of symbols

SELECT name, population
FROM city
WHERE name LIKE 'A%'; -- '%' - any number of symbols

SELECT name, population
FROM city
WHERE name LIKE '__an%'; -- '%' - any number of symbols

SELECT name, population
FROM city
WHERE name IN (
    'Ivanovo',
    'Orange',
    'Ulan Bator'); -- find this cites

SELECT name, population 
FROM city
ORDER BY name -- sort by name by alphabit
LIMIT 5;

SELECT name, population 
FROM city
ORDER BY name DESC -- sort by name by alphabit in reverse order
LIMIT 5;

SELECT name, population 
FROM city
ORDER BY population -- sort by population ascending
LIMIT 5;

SELECT name, population 
FROM city
ORDER BY population DESC -- sort by population descending
LIMIT 5;

SELECT name, population 
FROM city
ORDER BY 2 -- sort by population ascending
LIMIT 5;

SELECT name, population 
FROM city
ORDER BY rand() -- 5 random sites
LIMIT 5;

SELECT name, population 
FROM city
ORDER BY rand() -- 1 random sity
LIMIT 1;

SELECT continent
FROM country
GROUP BY continent; -- grouping - group the same continents (DISTINCT is the same)

SELECT continent, SUM(population) -- show population on continents
FROM country
GROUP BY 1; -- grouping by continent

SELECT continent, region, SUM(population) -- show population on regions
FROM country
GROUP BY 1,2 -- grouping by continent and region
ORDER BY 1,2; -- sort by continent and region

SELECT continent, region, SUM(population) -- show population on regions
FROM country
GROUP BY 1,2 WITH ROLLUP;-- grouping by continent and region and show sum of population on continents; ROLL UP - string of sum

SELECT continent, SUM(population)
FROM country
GROUP BY 1
HAVING SUM(population) > 400000000; -- sum population more then 400000000

-- save output in file
-- !!!write in my.ini 'secure_file_priv=C:/folder'!!!
SELECT continent, SUM(population) INTO OUTFILE 
'C:\folder\query.txt'
FROM country
GROUP BY 1
HAVING SUM(population) > 400000000;

-- save output in variable
SELECT SUM(population) INTO @count
FROM country;

SELECT @count; -- show variable


-- INSERT and LOAD DATA

CREATE DATABASE test34;
USE test34;

CREATE TABLE tbl1(
    id INT PRIMARY KEY AUTO_INCREMENT,
    txt VARCHAR(50) NOT NULL DEFAULT '',
    dt DATETIME,
    col4 ENUM('case1', 'case2', 'case3')
);

DESC tbl1;


SELECT * FROM tbl1; -- show all fields

INSERT INTO tbl1
VALUES (4, 'some text', now(), 'case1'); -- insert fields in table

INSERT INTO tbl1
VALUES -- insert some fields
(NULL, DEFAULT, now(), 'case1'), -- id use auto increment, txt use default
(NULL, 'foo', now(), 'case3'),
(NULL, 'Greka', now(), 'case1'),
(NULL, 'drive', now(), 'case2');

SELECT * FROM tbl1;

INSERT INTO tbl1 (txt, col4) -- insert only in txt and col4 fields
VALUES -- insert some fields
(DEFAULT, 'case1'),
('foo', 'case2');

SELECT * FROM tbl1;

INSERT IGNORE INTO tbl1 -- ignore fields if exists with the same id
VALUES
(4, DEFAULT, now(), 'case1'), -- will ignore
(24, DEFAULT, now(), 'case1'),
(25, DEFAULT, now(), 'case1'),
(26, DEFAULT, now(), 'case1'),
(27, DEFAULT, now(), 'case1');

SELECT * FROM tbl1;

INSERT INTO tbl1
SET id = 40, txt = 'dsdfd', col4 = 'case1'; -- set fields manually

SELECT * FROM tbl1;

SELECT user FROM mysql.user;

INSERT INTO tbl1
SELECT NULL, user, now(), 'case1' 
FROM mysql.user; -- insert fields 'user' from table user from db mysql in field 'txt'

LOAD DATA -- load data from file
INFILE 'C:/folder/data.txt'
INTO TABLE tbl1
FIELDS TERMINATED BY ',' -- fields are divided by ','
LINES TERMINATED BY '\n'; -- liines are divided by '\n'

SELECT *
FROM tbl1
ORDER BY id DESC
LIMIT 5;


-- UPDATE and REPLACE

USE world;

UPDATE city
SET population = population + 1e4 -- population + 10000
WHERE id = 4079;

SELECT *
FROM city
ORDER BY id DESC
LIMIT 1;

| 4079 | Rafah | PSE         | Rafah    |     102020 |

REPLACE INTO city -- if the field exists, then edit, if not, then create a new one
VALUES (4079, 'Rafah', 'PSE', 'Rafah', 95000);

SELECT *
FROM city
ORDER BY id DESC
LIMIT 1;


-- DELETE and LOAD XML, HTML

-- export data to xml
-- login,     databse, data format, execute (run command now), file name
mysql -uroot -padmin world --xml -e "SELECT * FROM city" > C:/folder/city.xml

-- export data to html
-- login,     databse, data format, execute (run command now), file name
mysql -uroot -padmin world --html -e "SELECT * FROM city" > C:/folder/city.html

DELETE FROM city 
WHERE id = 2345; -- delete field with id 2345

SELECT id, name
FROM city
WHERE id BETWEEN 2344 AND 2346;

DELETE FROM city -- delete the last field
ORDER BY id DESC
LIMIT 1;

SELECT id, name
FROM city
ORDER BY id DESC
LIMIT 5;

DELETE FROM city; -- delete all data

SELECT * FROM city;

-- import from xml
LOAD XML INFILE 'C:/folder/city.xml'
INTO TABLE city;

SELECT id, name
FROM city
ORDER BY id DESC
LIMIT 5;


-- agrerating functions

SELECT AVG(lifeexpectancy) -- average value of column 'lifeexpectancy'
FROM country;

SELECT count(lifeexpectancy) -- count of not NULL fields from column 'lifeexpectancy'
FROM country;

SELECT count(*) -- count of fields from table 'country'
FROM country;

SELECT count(DISTINCT continent) -- count of not repetitive fields from column 'continent'
FROM country;

SELECT GROUP_CONCAT(name) -- select one string of all fields from column 'name'
FROM country;

SELECT GROUP_CONCAT(name)
FROM country
WHERE population BETWEEN 10000 AND 50000; -- select one string of fields from column 'name' with population between 10000 and 50000
-- !don't use LIMIT, it doesn't change anything, because we get onlu one string!

SELECT max(surfacearea) -- max value of fields from column 'surfacearea'
FROM country;

SELECT min(surfacearea) -- min value of fields from column 'surfacearea'
FROM country;

SELECT name, max(surfacearea) -- ERROR: must use GROUP BY
FROM country;

SELECT max(surfacearea) -- show all countries with surfacearea
FROM country
GROUP BY name;

SELECT name, surfacearea -- the same with previous
FROM country;

SELECT name, surfacearea -- the country name with the biggest surfacearea
FROM country
ORDER BY 2 DESC
LIMIT 1;

SELECT continent, sum(surfacearea) -- continents with sum of surfacearea
FROM country
GROUP BY continent;


-- foreign keys (ralationships between tables)


-- ON DELETE CASCADE


DROP TABLE IF EXISTS child;
DROP TABLE IF EXISTS parent;

CREATE TABLE parent (
    id INT NOT NULL,
    PRIMARY KEY (id)
) ENGINE=INNODB;

CREATE TABLE child (
    id INT,
    parent_id INT,
    INDEX par_ind (parent_id),
    FOREIGN KEY (parent_id) -- fogeign key will be parent_id
        REFERENCES parent(id) -- parent_id will be associated with a field from the parent table
        ON DELETE CASCADE -- if something is deleted from the parent table, then everything will be deleted from the child too
) ENGINE=INNODB;

INSERT INTO parent VALUES (234), (238); -- insers parent fields (id)
INSERT INTO child VALUES (1,234), (2,234), (3,238); -- insert child fields (id, parent_id) !parent_id must match one of the id's in the parent table
INSERT INTO child VALUES (1,239); -- ERROR: parent_id doesn't match one of the id's in the parent table

SELECT * FROM parent;
SELECT * FROM child;

DELETE FROM child WHERE id = 3; -- delete field with id 3 in child table
DELETE FROM parent WHERE id = 234; -- delete field with id 234 in parent table and all fields in child table with parent_id 234, because while creating we write 'ON DELETE CASCADE'


--------------------------------------------------------------
-- ON DELETE RESTRICT


DROP TABLE IF EXISTS child;
DROP TABLE IF EXISTS parent;

CREATE TABLE parent (
    id INT NOT NULL,
    PRIMARY KEY (id)
) ENGINE=INNODB;

CREATE TABLE child (
    id INT,
    parent_id INT,
    INDEX par_ind (parent_id),
    FOREIGN KEY (parent_id) -- fogeign key will be parent_id
        REFERENCES parent(id) -- parent_id will be associated with a field from the parent table
        ON DELETE RESTRICT -- will not allow to delete a field from the parent table it there is a field in the child table related to the parent field
) ENGINE=INNODB;

INSERT INTO parent VALUES (234), (238); -- insers parent fields (id)
INSERT INTO child VALUES (1,234), (2,234), (3,238); -- insert child fields (id, parent_id) !parent_id must match one of the id's in the parent table

SELECT * FROM parent;
SELECT * FROM child;

SHOW CREATE TABLE child;

DELETE FROM child WHERE id = 3; -- delete field with id 3 in child table
DELETE FROM parent WHERE id = 234; -- ERROR: can't delete because there is field with parent_id 234 in child table, because while creating we write 'ON DELETE RESTRICT'


-------------------------------------------------------------
-- ON DELETE SET NULL


DROP TABLE IF EXISTS child;
DROP TABLE IF EXISTS parent;

CREATE TABLE parent (
    id INT NOT NULL,
    PRIMARY KEY (id)
) ENGINE=INNODB;

CREATE TABLE child (
    id INT,
    parent_id INT,
    txt VARCHAR(50),
    INDEX par_ind (parent_id),
    FOREIGN KEY (parent_id) -- fogeign key will be parent_id
        REFERENCES parent(id) -- parent_id will be associated with a field from the parent table
        ON DELETE SET NULL -- if something is deleted from the parent table, then appropriate parent_id will become NULL
) ENGINE=INNODB;

INSERT INTO parent VALUES (234), (238); -- insers parent fields (id)
INSERT INTO child VALUES (1,234,'ASA'), (2,234,'sdsc'), (3,238,'asxasxa'); -- insert child fields (id, parent_id) !parent_id must match one of the id's in the parent table

SELECT * FROM parent;
SELECT * FROM child;

SHOW CREATE TABLE child; -- show child table code

DELETE FROM child WHERE id = 3; -- delete field with id 3 in child table
DELETE FROM parent WHERE id = 234; -- parent_id in child table will be become NULL, because while creating we write 'ON DELETE SET NULL'


-- NO ACTION = DISTRICT
-- SET DEFAULT:  if something is deleted from the parent table, then appropriate parent_id will become default values



-- JOIN, INNER(CROSS) JOIN, LEFT JOIN, RIGHT JOIN

USE world;

SELECT code, name, capital
FROM country
ORDER BY rand()
LIMIT 1;

-- | CHN  | China |    1891 |

-- show ids and cites
SELECT id, ci.name
FROM city ci, country co
WHERE code = countrycode AND 
co.name = 'China';

-- show ids and capital
SELECT id, ci.name
FROM city ci, country co
WHERE id = capital AND 
co.name = 'China';

-- show ids and capital
SELECT id, ci.name
FROM city ci INNER JOIN country co  -- can be not one join
ON id = capital  -- can be not one condition
WHERE co.name = 'China';

-- show ids, contrycode and capital
SELECT id, ci.countrycode, ci.name
FROM city ci INNER JOIN country co
ON id = capital
WHERE co.name = 'China';

-- show langueges in Peking
SELECT language
FROM city ci INNER JOIN countrylanguage cl
USING (countrycode)
WHERE ci.name = 'Peking';




USE test;

DROP TABLE IF EXISTS `order`;
DROP TABLE IF EXISTS customer;

CREATE TABLE customer (
    idcustomer INT AUTO_INCREMENT NOT NULL,
    firstName VARCHAR(100),
    PRIMARY KEY (idcustomer)
) ENGINE=INNODB;

CREATE TABLE `order` ( -- name order in `` because it is keyword
    idorder INT AUTO_INCREMENT NOT NULL,
    order_name VARCHAR(100),
    customer_id INT,
    PRIMARY KEY (idorder)
) ENGINE=INNODB;

DESC customer;
DESC `order`;

INSERT INTO customer(firstName)
VALUES ('Vasya'), ('Petya'), ('Dasha');

INSERT INTO `order`(order_name, customer_id)
VALUES ('Travka', 1), ('Gash', 1), ('Extazy', 3), ('Amf', 4);

SELECT * FROM customer;
SELECT * FROM `order`;

-- contactination of all fields of the left table with fields of the right table
SELECT *
FROM customer LEFT JOIN `order`
ON  customer_id = idcustomer;

-- contactination of not NULL fields of the left table with not NULL fields of the right table
SELECT *
FROM customer INNER JOIN `order`
ON  customer_id = idcustomer;

-- contactination of all fields of the right table with fields of the left table
SELECT *
FROM customer RIGHT JOIN `order`
ON  customer_id = idcustomer;


USE world;

-- show what percentage of language in China
SELECT language, percentage
FROM country INNER JOIN countrylanguage
ON (code=countrycode)
WHERE name = 'China'
ORDER BY 2 DESC;


