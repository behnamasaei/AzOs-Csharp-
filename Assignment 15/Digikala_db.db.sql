BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Products" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"Name"	TEXT NOT NULL,
	"Price"	REAL NOT NULL,
	"Count"	INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS "Customers" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"Name"	TEXT,
	"City"	TEXT,
	"Country"	TEXT
);
INSERT INTO "Customers" ("Id","Name","City","Country") VALUES (1,'Behnam','Mashhad','Iran');
COMMIT;
