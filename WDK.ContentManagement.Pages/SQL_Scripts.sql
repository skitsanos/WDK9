/*
	Pages Common Storage
	v.7.3
*/


--- Create Table

CREATE TABLE [dbo].[Pages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[XmlData] [xml] NOT NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

--- Insert new page {procedure}


CREATE PROCEDURE CreatePage 
	-- Add the parameters for the stored procedure here
	@ApplicationName varchar(255) = NULL, 
	@XmlData xml = '<Page />'
AS
BEGIN

	INSERT INTO Pages (ApplicationName, XmlData) VALUES (@ApplicationName, @XmlData)
END
GO


--- Get page {procedure}


CREATE PROCEDURE GetPageByFilename 
	@ApplicationName varchar(255)=NULL,
	@Filename varchar(255) = NULL
AS
BEGIN
	SELECT id, ApplicationName,	XmlData
    FROM Pages 
	WHERE ApplicationName=@ApplicationName AND XmlData.value('/Page[1]/@Filename','nvarchar(max)')=@Filename
END
GO

CREATE PROCEDURE [dbo].[GetPageById] 
	@ApplicationName varchar(255)=NULL,
	@Id int = NULL
AS
BEGIN
	SELECT id, ApplicationName,	XmlData
    FROM Pages 
	WHERE ApplicationName=@ApplicationName AND id=@Id
END


