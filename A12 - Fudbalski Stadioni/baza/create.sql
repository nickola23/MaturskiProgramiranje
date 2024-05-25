CREATE TABLE Drzave (
  DrzavaID INT PRIMARY KEY,
  Naziv VARCHAR(100)
);

CREATE TABLE Grad (
  GradID INT PRIMARY KEY,
  Grad VARCHAR(100),
  PozivniBroj VARCHAR(10),
  PostanskiBroj VARCHAR(10),
  BrojStanovnika INT,
  DrzavaID INT,
  FOREIGN KEY (DrzavaID) REFERENCES Drzave(DrzavaID)
);

CREATE TABLE Stadion (
  StadionID INT PRIMARY KEY,
  Naziv VARCHAR(100),
  Adresa VARCHAR(200),
  Kapacitet INT,
  BrojUlaza INT,
  GradID INT,
  FOREIGN KEY (GradID) REFERENCES Grad(GradID)
);

CREATE TABLE Takmicenje (
  TakmicenjeID INT PRIMARY KEY,
  Naziv VARCHAR(100)
);


CREATE TABLE Klub (
  KlubID INT PRIMARY KEY,
  NazivKluba VARCHAR(100),
  StadionID INT,
  Email VARCHAR(100),
  Sajt VARCHAR(100),
  ZiroRacun VARCHAR(20),
  Amblem VARCHAR(100),
  FOREIGN KEY (StadionID) REFERENCES Stadion(StadionID)
);

CREATE TABLE Utakmica (
  UtakmicaID INT PRIMARY KEY,
  DatumIgranja DATE,
  VremeIgranja TIME,
  TakmicenjeID INT,
  DomacinID INT,
  GostID INT,
  GolovaDomacin INT,
  GolovaGost INT,
  FOREIGN KEY (TakmicenjeID) REFERENCES Takmicenje(TakmicenjeID),
  FOREIGN KEY (DomacinID) REFERENCES Klub(KlubID),
  FOREIGN KEY (GostID) REFERENCES Klub(KlubID)
);