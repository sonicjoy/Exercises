CREATE TABLE [dbo].[UserLoginCredentialValue]
(
	[LoginCredentialId] INT NOT NULL , 
    [PersonId] INT NOT NULL, 
    [Value] NVARCHAR(500) NULL, 
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(), 
    PRIMARY KEY ([LoginCredentialId], [PersonId]), 
    CONSTRAINT [FK_UserLoginCredentialValue_LoginCredential] FOREIGN KEY ([LoginCredentialId]) REFERENCES [LoginCredential]([LoginCredentialId]), 
    CONSTRAINT [FK_UserLoginCredentialValue_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([PersonId])
)
