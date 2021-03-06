SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Downloads]') AND type in (N'U'))
BEGIN
CREATE TABLE [Downloads](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[DownloadCatalog] [int] NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[ContentType] [nvarchar](255) NOT NULL,
	[FileContent] [image] NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [ntext] NULL,
	[Link] [ntext] NULL,
	[IsPublic] [int] NOT NULL CONSTRAINT [DF_Downloads_IsPublic]  DEFAULT ((0)),
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[Hits] [int] NULL CONSTRAINT [DF_Downloads_Hits]  DEFAULT ((0)),
 CONSTRAINT [PK_Downloads] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Faq]') AND type in (N'U'))
BEGIN
CREATE TABLE [Faq](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[Question] [ntext] NOT NULL,
	[Answer] [ntext] NULL,
 CONSTRAINT [PK_Faq] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Vendors]') AND type in (N'U'))
BEGIN
CREATE TABLE [Vendors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [nvarchar](255) NOT NULL,
	[XmlData] [xml] NOT NULL,
 CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Customers]') AND type in (N'U'))
BEGIN
CREATE TABLE [Customers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [nvarchar](255) NOT NULL,
	[XmlData] [xml] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Pages]') AND type in (N'U'))
BEGIN
CREATE TABLE [Pages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[XmlData] [xml] NOT NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Logs]') AND type in (N'U'))
BEGIN
CREATE TABLE [Logs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[Notes] [text] NOT NULL,
	[Status] [int] NOT NULL CONSTRAINT [DF_Logs_Status]  DEFAULT ((0)),
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Settings]') AND type in (N'U'))
BEGIN
CREATE TABLE [Settings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[PropertyName] [varchar](255) NOT NULL,
	[PropertyValue] [ntext] NULL,
 CONSTRAINT [PK__Settings__Provider] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Events]') AND type in (N'U'))
BEGIN
CREATE TABLE [Events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[XmlData] [xml] NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[StoreCatalogs]') AND type in (N'U'))
BEGIN
CREATE TABLE [StoreCatalogs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[rid] [int] NOT NULL,
	[ApplicationName] [nvarchar](255) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_StoreCatalogs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Products]') AND type in (N'U'))
BEGIN
CREATE TABLE [Products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [nvarchar](255) NOT NULL,
	[StoreCatalog] [int] NOT NULL,
	[XmlData] [xml] NOT NULL,
	[Hits] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Orders]') AND type in (N'U'))
BEGIN
CREATE TABLE [Orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[XmlData] [xml] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Scheduler]') AND type in (N'U'))
BEGIN
CREATE TABLE [Scheduler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[AccountUsername] [varchar](255) NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [ntext] NULL,
	[StartsOn] [datetime] NOT NULL,
	[EndsOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Scheduler] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Files]') AND type in (N'U'))
BEGIN
CREATE TABLE [Files](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[Owner] [varchar](255) NULL,
	[FileName] [varchar](255) NOT NULL,
	[ContentType] [varchar](255) NOT NULL,
	[FileContent] [image] NOT NULL,
	[Description] [ntext] NULL,
	[FileSecurity] [xml] NULL,
	[Hits] [int] NULL CONSTRAINT [DF_Files_Hits]  DEFAULT ((0)),
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PageComments]') AND type in (N'U'))
BEGIN
CREATE TABLE [PageComments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[PageId] [int] NOT NULL,
	[XmlData] [xml] NOT NULL,
 CONSTRAINT [PK_PageComments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[XmlDb]') AND type in (N'U'))
BEGIN
CREATE TABLE [XmlDb](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[XmlData] [xml] NOT NULL,
 CONSTRAINT [PK_XmlDb] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ImageGalleryFolders]') AND type in (N'U'))
BEGIN
CREATE TABLE [ImageGalleryFolders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_ImageGalleryFolders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Testimonials]') AND type in (N'U'))
BEGIN
CREATE TABLE [Testimonials](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[XmlData] [xml] NOT NULL,
 CONSTRAINT [PK_Testimonials] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DistributionLists]') AND type in (N'U'))
BEGIN
CREATE TABLE [DistributionLists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Description] [text] NULL,
	[Template] [text] NULL,
 CONSTRAINT [PK_DistributionLists] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Subscriptions]') AND type in (N'U'))
BEGIN
CREATE TABLE [Subscriptions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[List] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Subscriptions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Newsletters]') AND type in (N'U'))
BEGIN
CREATE TABLE [Newsletters](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[List] [int] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[NewsletterContent] [ntext] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[IsEnabled] [int] NOT NULL CONSTRAINT [DF_Newsletters_IsEnabled]  DEFAULT ((1)),
 CONSTRAINT [PK_Newsletters] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DownloadCatalogs]') AND type in (N'U'))
BEGIN
CREATE TABLE [DownloadCatalogs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_DownloadCatalogs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ImageGalleryItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [ImageGalleryItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](255) NOT NULL,
	[ImageGalleryFolder] [int] NOT NULL,
	[Filename] [varchar](255) NOT NULL,
	[Description] [ntext] NULL,
	[ContentType] [varchar](255) NOT NULL,
	[FileContent] [image] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Orientation] [int] NULL,
 CONSTRAINT [PK_ImageGalleryItems] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[viewPages]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [viewPages]
AS
SELECT     id, ApplicationName, XmlData.value(''/Page[1]/@file'', ''nvarchar(max)'') AS Filename, XmlData.value(''/Page[1]/@masterPage'', ''nvarchar(max)'') 
                      AS MasterPage, XmlData.value(''/Page[1]/@title'', ''nvarchar(max)'') AS Title, XmlData.value(''/Page[1]/Meta[1]/@keywords'', ''nvarchar(max)'') AS Keywords,
                       XmlData.value(''/Page[1]/Meta[1]/@description'', ''nvarchar(max)'') AS Description, XmlData.value(''/Page[1]/@content'', ''nvarchar(max)'') AS PageContent, 
                      XmlData.value(''/Page[1]/@hits'', ''int'') AS Hits, XmlData.value(''/Page[1]/@createdOn'', ''nvarchar(max)'') AS CreatedOn, 
                      XmlData.value(''/Page[1]/@updatedOn'', ''nvarchar(max)'') AS UpdatedOn, XmlData.value(''/Page[1]/@type'', ''nvarchar(max)'') AS ContentType, 
                      XmlData.value(''/Page[1]/@allowComments'', ''nvarchar(max)'') AS AllowComments,
                          (SELECT     COUNT(id) AS Expr1
                            FROM          dbo.PageComments
                            WHERE      (PageId = dbo.Pages.id)) AS Comments
FROM         dbo.Pages
' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CreatePage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--- Insert new page {procedure}


CREATE PROCEDURE [CreatePage] 
	-- Add the parameters for the stored procedure here
	@ApplicationName varchar(255) = NULL, 
	@XmlData xml = ''<Page />''
AS
BEGIN

	INSERT INTO Pages (ApplicationName, XmlData) VALUES (@ApplicationName, @XmlData)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetPageByFilename]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [GetPageByFilename] 
	@ApplicationName varchar(255)=NULL,
	@Filename nvarchar(max) = NULL
AS
BEGIN
	SELECT	
	id, 
	ApplicationName, 
	CONVERT(nvarchar(max), XmlData) As XmlData
    FROM Pages 
	WHERE ApplicationName=@ApplicationName AND XmlData.value(''/Page[1]/@file'',''nvarchar(max)'')=@Filename
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetPageById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [GetPageById] 
	@ApplicationName varchar(255)=NULL,
	@Id int = NULL
AS
BEGIN
	SELECT id, ApplicationName,	CONVERT(nvarchar(max), XmlData) As XmlData
    FROM Pages 
	WHERE ApplicationName=@ApplicationName AND id=@Id
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UpdatePage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [UpdatePage] 
	@ApplicationName varchar(255)=NULL,
	@Id varchar(255) = NULL,
	@Filename varchar(255) = NULL,
	@MasterPage varchar(255) = ''~/default.master'',
	@Title nvarchar(255) = NULL,
	@Keywords nvarchar(max) = '''',
	@Description nvarchar(max) = '''',
	@Content nvarchar(max) = '''',
	@UpdatedOn nvarchar(max) = ''''
AS
BEGIN
	UPDATE Pages SET
	XmlData.modify(''replace value of (/Page/@file)[1] with sql:variable("@Filename")'')
	WHERE ApplicationName=@ApplicationName AND id=@Id

	UPDATE Pages SET
	XmlData.modify(''replace value of (/Page/@masterPage)[1] with sql:variable("@MasterPage")'')
	WHERE ApplicationName=@ApplicationName AND id=@Id
	
	UPDATE Pages SET
	XmlData.modify(''replace value of (/Page/@title)[1] with sql:variable("@Title")'')
	WHERE ApplicationName=@ApplicationName AND id=@Id
	
	UPDATE Pages SET
	XmlData.modify(''replace value of (/Page/Meta/@keywords)[1] with sql:variable("@Keywords")'')
	WHERE ApplicationName=@ApplicationName AND id=@Id

	UPDATE Pages SET
	XmlData.modify(''replace value of (/Page/Meta/@description)[1] with sql:variable("@Description")'')
	WHERE ApplicationName=@ApplicationName AND id=@Id

	UPDATE Pages SET
	XmlData.modify(''replace value of (/Page/@content)[1] with sql:variable("@Content")'')
	WHERE ApplicationName=@ApplicationName AND id=@Id

	UPDATE Pages SET
	XmlData.modify(''replace value of (/Page/@updatedOn)[1] with sql:variable("@UpdatedOn")'')
	WHERE ApplicationName=@ApplicationName AND id=@Id
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [GetEvent] 
	@ApplicationName varchar(255)=NULL,
	@id int = NULL
AS
BEGIN
	SELECT id, ApplicationName,
    CONVERT(nvarchar(max), XmlData) As XmlData
    FROM Events
	WHERE ApplicationName=@ApplicationName AND id=@id
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UpdateEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [UpdateEvent]
	@ApplicationName varchar(255) = NULL, 
	@id int = NULL, 
	@Title nvarchar(max) = NULL, 
	@Description nvarchar(max) = NULL, 
	@Content nvarchar(max) = NULL, 
	@UpdatedOn nvarchar(max) = NULL
AS
BEGIN
	UPDATE Events SET XmlData.modify(''replace value of (/Event/@title)[1] with sql:variable("@Title")'') WHERE ApplicationName=@ApplicationName AND id=@id;
	UPDATE Events SET XmlData.modify(''replace value of (/Event/@description)[1] with sql:variable("@Description")'') WHERE ApplicationName=@ApplicationName AND id=@id;
	UPDATE Events SET XmlData.modify(''replace value of (/Event/@content)[1] with sql:variable("@Content")'') WHERE ApplicationName=@ApplicationName AND id=@id;
	UPDATE Events SET XmlData.modify(''replace value of (/Event/@updatedOn)[1] with sql:variable("@UpdatedOn")'') WHERE ApplicationName=@ApplicationName AND id=@id;
END 


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[viewEvents]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [viewEvents]
AS
SELECT     ApplicationName, id, XmlData.value(''/Event[1]/@title'', ''nvarchar(max)'') AS Title, XmlData.value(''/Event[1]/@createdOn'', ''nvarchar(max)'') AS CreatedOn, 
                      XmlData.value(''/Event[1]/@content'', ''nvarchar(max)'') AS EventContent
FROM         dbo.Events
' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UpdateTestimonial]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [UpdateTestimonial]
	@ApplicationName varchar(255) = NULL, 
	@id int = NULL, 
	@Author nvarchar(max) = NULL, 
	@Email nvarchar(max) = NULL, 
	@Content nvarchar(max) = NULL, 
	@UpdatedOn nvarchar(max) = NULL
AS
BEGIN
	UPDATE Events SET XmlData.modify(''replace value of (/Testimonial/@author)[1] with sql:variable("@Author")'') WHERE ApplicationName=@ApplicationName AND id=@id;
	UPDATE Events SET XmlData.modify(''replace value of (/Testimonial/@email)[1] with sql:variable("@Email")'') WHERE ApplicationName=@ApplicationName AND id=@id;
	UPDATE Events SET XmlData.modify(''replace value of (/Testimonial/@content)[1] with sql:variable("@Content")'') WHERE ApplicationName=@ApplicationName AND id=@id;
	UPDATE Events SET XmlData.modify(''replace value of (/Testimonial/@updatedOn)[1] with sql:variable("@UpdatedOn")'') WHERE ApplicationName=@ApplicationName AND id=@id;
END 

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PostPageComment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [PostPageComment] 
	-- Add the parameters for the stored procedure here
	@ApplicationName varchar(255) = NULL, 
	@PageId int,
	@XmlData xml = ''<Comment />''
AS
BEGIN

	INSERT INTO PageComments (ApplicationName, PageId, XmlData) VALUES (@ApplicationName, @PageId, @XmlData)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[viewImageGalleryItems]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [viewImageGalleryItems]
AS
SELECT     dbo.ImageGalleryItems.id, dbo.ImageGalleryItems.ApplicationName, dbo.ImageGalleryItems.ImageGalleryFolder, dbo.ImageGalleryItems.Filename, 
                      dbo.ImageGalleryItems.Description, dbo.ImageGalleryItems.ContentType, dbo.ImageGalleryItems.CreatedOn, dbo.ImageGalleryItems.Orientation, 
                      dbo.ImageGalleryFolders.Title AS FolderTitle, dbo.ImageGalleryFolders.Description AS FolderDescription
FROM         dbo.ImageGalleryItems INNER JOIN
                      dbo.ImageGalleryFolders ON dbo.ImageGalleryItems.ImageGalleryFolder = dbo.ImageGalleryFolders.id
' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[viewTestimonials]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [viewTestimonials]
AS
SELECT     ApplicationName, id, XmlData.value(''/Testimonial[1]/@author'', ''nvarchar(max)'') AS Author, XmlData.value(''/Testimonial[1]/@email'', ''nvarchar(max)'') 
                      AS Email, XmlData.value(''/Testimonial[1]/@createdOn'', ''nvarchar(max)'') AS CreatedOn, XmlData.value(''/Testimonial[1]/@content'', ''nvarchar(max)'') 
                      AS Content
FROM         dbo.Testimonials
' 
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[ImageGalleryRelations]') AND parent_object_id = OBJECT_ID(N'[ImageGalleryItems]'))
ALTER TABLE [ImageGalleryItems]  WITH CHECK ADD  CONSTRAINT [ImageGalleryRelations] FOREIGN KEY([ImageGalleryFolder])
REFERENCES [ImageGalleryFolders] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [ImageGalleryItems] CHECK CONSTRAINT [ImageGalleryRelations]
