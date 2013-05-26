CREATE TABLE [dbo].[Events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[XmlData] [xml] NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE PROCEDURE GetEvent 
	@ApplicationName varchar(255)=NULL,
	@id int = NULL
AS
BEGIN
	SELECT id, ApplicationName,
    XmlData.value('/Event[1]/@Title','nvarchar(max)') AS Title,
    XmlData.value('/Event[1]/@Description','nvarchar(max)') AS Description,
    XmlData.value('/Event[1]/@Content','nvarchar(max)') AS EventContent,
    XmlData.value('/Event[1]/@Hits','nvarchar(max)') AS Hits, 
    XmlData.value('/Event[1]/@Hits','nvarchar(max)') AS AllowComments,
    XmlData.value('count(/Event[1]/Comments[1]/Comment)','int') AS Comments,
	XmlData.value('count(/Event[1]/Comments[1])','nvarchar(max)') AS CommentsXml,
    XmlData.value('/Event[1]/@CreatedOn','nvarchar(max)') AS CreatedOn, 
    XmlData.value('/Event[1]/@UpdatedOn','nvarchar(max)') AS UpdatedOn
    FROM Pages 
	WHERE ApplicationName=@ApplicationName AND id=@id
END
GO

CREATE PROCEDURE UpdateEvent
	@ApplicationName varchar(255) = NULL, 
	@id int = NULL, 
	@Title nvarchar(max) = NULL, 
	@Description nvarchar(max) = NULL, 
	@Content nvarchar(max) = NULL, 
	@UpdatedOn nvarchar(max) = NULL
AS
BEGIN
	UPDATE Events SET XmlData.modify('replace value of (/Event/@Title)[1] with sql:variable("@Title")') WHERE ApplicationName=@ApplicationName AND id=@id;
	UPDATE Events SET XmlData.modify('replace value of (/Event/@Description)[1] with sql:variable("@Description")') WHERE ApplicationName=@ApplicationName AND id=@id;
	UPDATE Events SET XmlData.modify('replace value of (/Event/@Content)[1] with sql:variable("@Content")') WHERE ApplicationName=@ApplicationName AND id=@id;
	UPDATE Events SET XmlData.modify('replace value of (/Event/@UpdatedOn)[1] with sql:variable("@UpdatedOn")') WHERE ApplicationName=@ApplicationName AND id=@id;
END 

GO