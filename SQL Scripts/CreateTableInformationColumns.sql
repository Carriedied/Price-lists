USE PriceListsInformation
GO
CREATE TABLE PriceListColumns (
    Id INT PRIMARY KEY IDENTITY,
    ColumnName NVARCHAR(255) NOT NULL,
    ColumnType INT NOT NULL,
    PriceListId INT NOT NULL,
    FOREIGN KEY (PriceListId) REFERENCES PriceLists(PriceListId)
);