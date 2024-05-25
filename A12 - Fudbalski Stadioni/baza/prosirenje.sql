CREATE TABLE igrac (
    Ime VARCHAR(50),
    Prezime VARCHAR(50),
    NazivDrzave VARCHAR(100),
    DatumRodjenja DATE,
    DatumPocetka DATE,
    DatumZavrsetka DATE,
	KlubID INT REFERENCES Klub(KlubID),
	CHECK(DatumPocetka<=DatumZavrsetka),
	PRIMARY KEY(Ime,Prezime,Datumrodjenja)
);

INSERT INTO Igrac (Ime, Prezime, NazivDrzave, DatumRodjenja, DatumPocetka, DatumZavrsetka, KlubID) VALUES
	('Robert', 'Lewandowski', 'Poland', '1988-08-21', '2005-08-05', NULL, 1),
	('Virgil', 'van Dijk', 'Netherlands', '1991-07-08', '2009-05-01', NULL, 4),	
	('Mohamed', 'Salah', 'Egypt', '1992-06-15', '2008-06-01', NULL, 4),
	('Luka', 'Modric', 'Croatia', '1985-09-09', '2002-04-07', NULL, 2),
	('Kylian', 'Mbappe', 'France', '1998-12-20', '2015-12-07', NULL, 11),
	('Harry', 'Kane', 'England', '1993-07-28', '2010-07-07', NULL, 7),	
	('Raheem', 'Sterling', 'England', '1994-12-08', '2010-07-01', NULL, 13),
	('Joshua', 'Kimmich', 'Germany', '1995-02-08', '2013-08-01', NULL, 7),
	('Bruno', 'Fernandes', 'Portugal', '1994-09-08', '2012-07-01', NULL, 3),	
	('Jan', 'Oblak', 'Slovenia', '1993-01-07', '2009-07-01', NULL, 18),
	('Antoine', 'Griezmann', 'France', '1991-03-21', '2009-09-01', NULL, 18),
	('Alisson', 'Becker', 'Brazil', '1992-10-02', '2008-07-01', NULL, 4),	
	('Marco', 'Reus', 'Germany', '1989-05-31', '2006-07-01', NULL, 8),	
	('Jadon', 'Sancho', 'England', '2000-03-25', '2017-08-01', NULL, 8),
	('Romelu', 'Lukaku', 'Belgium', '1993-05-13', '2009-07-01', NULL, 10),
	('Jorginho', 'Frello', 'Italy', '1991-12-20', '2008-07-01', NULL, 15),
	('Lautaro', 'Martínez', 'Argentina', '1997-08-22', '2018-07-01', NULL, 6),
	('José', 'Gayà', 'Spain', '1995-05-25', '2012-09-01', NULL, 9),
	('Heung-Min', 'Son', 'South Korea', '1992-07-08', '2015-08-28', NULL, 16),
	('Nicolás', 'Otamendi', 'Argentina', '1988-02-12', '2020-09-29', NULL, 19),
	('Bukayo', 'Saka', 'England', '2001-09-05', '2018-11-29', NULL, 15),
	('Jordan', 'Henderson', 'England', '1990-06-17', '2011-06-09', NULL, 17),
	('Victor', 'Osimhen', 'Nigeria', '1998-12-29', '2020-07-31', NULL, 23),
	('Marcus', 'Rashford', 'England', '1997-10-31', '2015-11-21', NULL, 3),
	('Dušan', 'Tadić', 'Serbia', '1988-11-20', '2018-07-01', NULL, 24),
	('Ivan', 'Marcano', 'Spain', '1987-06-23', '2018-07-01', NULL, 20);

