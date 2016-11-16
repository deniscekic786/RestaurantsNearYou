CREATE TABLE dbo.Restaurant
(
    Id int Identity NOT NULL,
    Name varchar(100) NOT NULL,
	 Category varchar(100) NOT NULL,
	  ImageReference varchar(100) NOT NULL,
	   ImageSource varchar(100) NOT NULL,
	    AddressId int NOT NULL,
		PhoneNumber varchar(20) NOT NULL
CONSTRAINT [pk_dbo.RestaurantId] Primary KEY CLUSTERED ([Id] ASC),
CONSTRAINT [fk_dbo.RestaurantId] FOREIGN KEY (AddressId)     
    REFERENCES dbo.Address (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE    
);

CREATE TABLE dbo.Address
(
    Id int NOT NULL,
    State varchar(100) NOT NULL,
	 City varchar(100) NOT NULL,
	  PostalCode varchar(5) NOT NULL,
	   Latitude Float NOT NULL,
	    Longitude Float NOT NULL,
CONSTRAINT [pk_dbo.AddressId] Primary KEY CLUSTERED ([Id] ASC),
);