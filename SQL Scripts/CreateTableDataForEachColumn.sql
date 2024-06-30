USE PriceListsInformation
GO

CREATE TABLE PriceListColumnValues (
    Id INT PRIMARY KEY IDENTITY,
    PriceListColumnId INT NOT NULL,
    RowIndex INT NOT NULL,
    Value NVARCHAR(MAX),
    FOREIGN KEY (PriceListColumnId) REFERENCES PriceListColumns(Id)
);