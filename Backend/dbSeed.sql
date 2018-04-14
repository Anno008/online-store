DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Brands;
DROP TABLE IF EXISTS ComponentTypes;
DROP TABLE IF EXISTS Components;
DROP TABLE IF EXISTS ShoppingCarts;
DROP TABLE IF EXISTS ShoppingCartItem;
DROP TABLE IF EXISTS Tokens;


CREATE TABLE Users
(
    Id integer PRIMARY KEY,
    Role integer NOT NULL,
    Username text UNIQUE NOT NULL,
    Password text NOT NULL
);

CREATE TABLE Brands
(
    Id integer PRIMARY KEY,
    Name text NOT NULL
);

CREATE TABLE ComponentTypes
(
    Id integer PRIMARY KEY,
    Name text NOT NULL
);

CREATE TABLE Components
(
    Id integer PRIMARY KEY,
    Name text NOT NULL,
    Price real NOT NULL,
    BrandId integer NOT NULL,
    ComponentTypeId integer NOT NULL,
    FOREIGN KEY(BrandId) REFERENCES Brands(Id),
    FOREIGN KEY(ComponentTypeId) REFERENCES ComponentTypes(Id)
);

CREATE TABLE ShoppingCarts
(
    Id integer PRIMARY KEY,
    TotalPrice real,
    UserId integer NOT NULL,
    FOREIGN KEY(UserId) REFERENCES Users(Id)
);

CREATE TABLE ShoppingCartItem
(
    Id integer PRIMARY KEY,
    ComponentId integer NOT NULL,
    ShoppingCartId integer NOT NULL,
    FOREIGN KEY(ComponentId) REFERENCES Components(Id),
    FOREIGN KEY(ShoppingCartId) REFERENCES ShoppingCarts(Id)
);

CREATE TABLE Tokens
(
    Id integer PRIMARY KEY,
    Token text,
    TokenId text
);

/*Users*/
/* Username: Admin Password: Admin */
INSERT INTO Users ( Username, Password, Role)
VALUES ("Admin", "c1c224b03cd9bc7b6a86d77f5dace40191766c485cd55dc48caf9ac873335d6f", 1);
/* Username: User Password: User */
INSERT INTO Users ( Username, Password, Role)
VALUES ("User", "b512d97e7cbf97c273e4db073bbb547aa65a84589227f8f3d9e4a72b9372a24d", 0);

/* Brands */
INSERT INTO Brands ( Name) VALUES ("Intel");
INSERT INTO Brands ( Name) VALUES ("Nvidia");
INSERT INTO Brands ( Name) VALUES ("AMD");
INSERT INTO Brands ( Name) VALUES ("SAMSUNG");
INSERT INTO Brands ( Name) VALUES ("Corsair");

/* Component types */
INSERT INTO ComponentTypes ( Name) VALUES ("CPU");
INSERT INTO ComponentTypes ( Name) VALUES ("GPU");
INSERT INTO ComponentTypes ( Name) VALUES ("SSD");
INSERT INTO ComponentTypes ( Name) VALUES ("RAM");


/* Components */
INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "Inte Core i5-6600K 3.50 GHz Quad Core Skypelake Desktop Processor", 235, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "Intel") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "CPU") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "MSI GeForce GTX 1070", 235, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "Nvidia") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "GPU") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "AMD RYZEN 7 1700 8-Core 3.0 GHz (3.7 GHz Turbo) ", 280, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "AMD") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "CPU") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "AMD Radeon RX 480 4GB", 270, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "AMD") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "GPU") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "NVIDIA - Founders Edition GeForce GTX 1080", 589, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "Nvidia") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "GPU") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "Inte Core i7 8700K", 359, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "Intel") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "CPU") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "Inte Core i7 8750H", 395, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "Intel") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "CPU") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "AMD Ryzen Threadripper 1950X (16-core/32-thread)", 922, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "AMD") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "CPU") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "XFX Radeon RX Vega 64 8 GB", 1300, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "AMD") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "GPU") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "Samsung 850 EVO 250GB", 108, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "SAMSUNG") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "SSD") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "Samsung 950 PRO Series - 512GB PCIe NVMe", 239, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "SAMSUNG") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "SSD") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "Samsung 960 EVO 250GB", 140, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "SAMSUNG") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "SSD") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "Corsair Vengeance LPX 16GB (2x8GB) DDR4 DRAM 3000MHz C15 Desktop Memory Kit", 189, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "Corsair") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "RAM") AS typesF;

INSERT INTO Components (Name, Price, BrandId, ComponentTypeId)
SELECT "Corsair Vengeance 8 GB (2 x 4 GB) DDR3 1600 MHz", 67, brandsF.Id, typesF.Id
FROM (SELECT Id FROM Brands WHERE Name like "Corsair") AS brandsF
CROSS JOIN (SELECT Id FROM ComponentTypes WHERE Name like "RAM") AS typesF;