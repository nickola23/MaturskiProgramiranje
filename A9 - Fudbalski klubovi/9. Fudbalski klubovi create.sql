CREATE TABLE DRZAVA(
DrzavaID INT PRIMARY KEY,
Naziv NVARCHAR(30) NOT NULL
);

CREATE TABLE GRAD(
GradID INT PRIMARY KEY,
Grad NVARCHAR(100) NOT NULL,
PozivniBroj NVARCHAR(100) NOT NULL,
PostanskiBroj NVARCHAR(100) NOT NULL,
BrojStanovnika INT NOT NULL,
DrzavaID INT FOREIGN KEY REFERENCES DRZAVA(DrzavaID)

);

CREATE TABLE STADION(
StadionID INT PRIMARY KEY,
Naziv NVARCHAR(100) NOT NULL,
Adresa NVARCHAR(100) NOT NULL,
Kapacitet INT NOT NULL,
BrojUlaza INT NOT NULL,
GradID INT FOREIGN KEY REFERENCES GRAD(GradID)

);

CREATE TABLE KLUB(
KlubID INT PRIMARY KEY,
NazivKluba NVARCHAR(100) NOT NULL,
StadionID INT FOREIGN KEY REFERENCES STADION(StadionID),
Email NVARCHAR(100) NOT NULL,
Sajt NVARCHAR(100) NOT NULL,
ZiroRacun NVARCHAR(100) NOT NULL,
Amblem varbinary(max)


);

CREATE TABLE TAKMICENJE(
TakmicenjeID INT PRIMARY KEY,
Naziv NVARCHAR(100) NOT NULL

);

CREATE TABLE UTAKMICA(
UtakmicaID INT PRIMARY KEY,
DatumIgranja DATE NOT NULL,
VremeIgranja TIME NOT NULL,
TakmicenjeID INT FOREIGN KEY REFERENCES TAKMICENJE(TakmicenjeID),
DomacinID INT FOREIGN KEY REFERENCES KLUB(KlubID),
GostID INT FOREIGN KEY REFERENCES KLUB(KlubID),
GolovaDomacin INT NOT NULL,
GolovaGost INT NOT NULL

);
