CREATE TABLE [dbo].[LoginCredential]
(
	[LoginCredentialId] INT NOT NULL PRIMARY KEY, 
    [WebSiteId] INT NOT NULL, 
    [Name] NVARCHAR(50) NULL, 
    [Type] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_LoginCredential_WebSite] FOREIGN KEY ([WebSiteId]) REFERENCES [WebSite]([WebSiteId])
)
