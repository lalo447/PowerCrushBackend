USE NuevoSistemaPowerStar
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Eduardo Fabian España Castillo
-- Create Date: 09/08/2025
-- Description: Tool loan table created
-- =============================================
BEGIN TRY
    BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM SYS.TABLES WHERE NAME = 'ToolsIoan' AND TYPE = 'U')
        BEGIN
         CREATE TABLE [dbo].[ToolsIoan](
				ToolIoanId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
				ClientName VARCHAR(100) NOT NULL ,
				NumberIoan INT NOT NULL,
				TotalItems INT NOT NULL,
				IsActive BIT NOT NULL,
                CreatedDate DATETIME NOT NULL,
                ReturnDate DATETIME NULL,
                ToolsLoanDetails VARCHAR(250) NOT NULL,
                ToolsIoanStatus VARCHAR(25) NOT NULL,
                Comments VARCHAR(250) NULL
			);
        END
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH

