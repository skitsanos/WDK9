Imports Sleepycat

Public Class XmlDb
    Implements IDisposable

    ' libdbxml_dotnet21.dll

#Region " Properties "
    Private _CacheSize As Integer = 8 * 1024 * 1024
    Public Property CacheSize() As Integer
        Get
            Return _CacheSize
        End Get
        Set(ByVal Value As Integer)
            _CacheSize = Value
        End Set
    End Property

    Private _EnvironmentPath As String = AppDomain.CurrentDomain.BaseDirectory
    Public Property EnvironmentPath() As String
        Get
            Return _EnvironmentPath
        End Get
        Set(ByVal Value As String)
            _EnvironmentPath = Value
        End Set
    End Property

    Public ReadOnly Property Version() As String
        Get
            Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString
        End Get
    End Property

    Private mgrContext As Sleepycat.DbXml.Manager = Nothing
#End Region

#Region " CreateManager() "
    Private Function CreateManager(ByVal EnvPath As String) As DbXml.Manager
        Dim env As Db.Environment = Nothing

        Try
            Dim envConfig As New Db.EnvironmentConfig
            envConfig.CacheSize = CacheSize
            envConfig.Create = True
            envConfig.Transactional = True
            envConfig.InitializeCache = True
            envConfig.InitializeLocking = True
            envConfig.InitializeLogging = True
            envConfig.Recover = True

            Dim manConfig As New DbXml.ManagerConfig
            manConfig.AdoptEnvironment = True
            manConfig.AllowExternalAccess = True

            env = New Db.Environment(EnvPath, envConfig)

            Return New DbXml.Manager(env, manConfig)

        Catch ex As Exception
            If Not env Is Nothing Then env.Dispose()
            Debug.WriteLine(ex.InnerException.ToString)

            Throw New Exception(ex.InnerException.ToString)
        End Try
    End Function
#End Region

#Region " CreateDatabase() "
    Public Function CreateDatabase(ByVal Name As String, Optional ByVal Overwrite As Boolean = False) As Boolean
        If IO.Directory.Exists(EnvironmentPath) = False Then
            Throw New Exception(".EnvironmentPath not exists")
        End If

        Dim filePath As String = EnvironmentPath & "\" & Name
        If IO.Directory.Exists(filePath) Then
            If Overwrite = False Then
                Throw New Exception("Database already exists")
            Else
                DeleteDatabase(Name)
                IO.Directory.CreateDirectory(filePath)
            End If
        Else
            IO.Directory.CreateDirectory(filePath)
        End If

        If mgrContext Is Nothing Then mgrContext = CreateManager(EnvironmentPath & "\" & Name)

        Try
            Dim containerConfig As New DbXml.ContainerConfig
            containerConfig.Transactional = True

            Dim txn As DbXml.Transaction = mgrContext.CreateTransaction

            Dim container As DbXml.Container = mgrContext.CreateContainer(txn, Name, containerConfig)
            txn.Commit()

            txn.Dispose()

            Return True

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
#End Region

#Region " AddDocument "
    Public Function AddDocument(ByVal Database As String, ByVal Name As String, ByVal Document As String) As Boolean
        If Exists(Database) = False Then
            Throw New Exception("Database {" & Database & "} not exists")
        End If

        Debug.WriteLine("Document: " & Document)

        If mgrContext Is Nothing Then mgrContext = CreateManager(EnvironmentPath & "\" & Database)

        Try
            Dim containerConfig As New DbXml.ContainerConfig
            containerConfig.Transactional = True

            Dim txn As DbXml.Transaction = mgrContext.CreateTransaction
            Dim uc As DbXml.UpdateContext = mgrContext.CreateUpdateContext

            Dim container As DbXml.Container = mgrContext.OpenContainer(Nothing, Database, containerConfig)

            Dim docConfig As New DbXml.DocumentConfig
            docConfig.GenerateName = False

            container.PutDocument(txn, Name, Document, uc, docConfig)

            txn.Commit()

            txn.Dispose()

            Return True

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
            Throw New Exception(ex.ToString)
        End Try
    End Function
#End Region

#Region " AddDocument {Xml.XmlDocument}"
    Public Function AddDocument(ByVal Database As String, ByVal Name As String, ByVal Document As Xml.XmlDocument) As Boolean
        If Exists(Database) = False Then
            Throw New Exception("Database {" & Database & "} not exists")
        End If

        If mgrContext Is Nothing Then mgrContext = CreateManager(EnvironmentPath)

        Try
            Dim containerConfig As New DbXml.ContainerConfig
            containerConfig.Transactional = True

            Dim txn As DbXml.Transaction = mgrContext.CreateTransaction
            Dim uc As DbXml.UpdateContext = mgrContext.CreateUpdateContext

            Dim container As DbXml.Container = mgrContext.OpenContainer(Nothing, Database, containerConfig)

            Dim docConfig As New DbXml.DocumentConfig
            docConfig.GenerateName = False

            container.PutDocument(txn, Name, Document.OuterXml, uc, docConfig)

            txn.Commit()

            txn.Dispose()

            Return True

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
#End Region

#Region " AddDocument {Unnamed Document} "
    Public Function AddDocument(ByVal Database As String, ByVal Document As String) As Boolean
        If Exists(Database) = False Then
            Throw New Exception("Database {" & Database & "} not exists")
        End If

        If mgrContext Is Nothing Then mgrContext = CreateManager(EnvironmentPath)

        Try
            Dim containerConfig As New DbXml.ContainerConfig
            containerConfig.Transactional = True

            Dim txn As DbXml.Transaction = mgrContext.CreateTransaction
            Dim uc As DbXml.UpdateContext = mgrContext.CreateUpdateContext

            Dim container As DbXml.Container = mgrContext.OpenContainer(Nothing, Database, containerConfig)

            Dim docConfig As New DbXml.DocumentConfig
            docConfig.GenerateName = True

            container.PutDocument(txn, "", Document, uc, docConfig)

            txn.Commit()

            txn.Dispose()

            Return True

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
#End Region

#Region " DocumentsCount() "
    Function DocumentsCount(ByVal Database As String) As Integer
        If mgrContext Is Nothing Then mgrContext = CreateManager(EnvironmentPath & "\" & Database)

        Dim cont As Sleepycat.DbXml.Container = mgrContext.OpenContainer(Nothing, Database)
        Return cont.GetNumDocuments(Nothing)
    End Function
#End Region

#Region " ExecuteXquery() "
    Public Function ExecuteXquery(ByVal Database As String, ByVal Query As String) As String
        If mgrContext Is Nothing Then mgrContext = CreateManager(EnvironmentPath & "\" & Database)

        Try
            Dim context As Sleepycat.DbXml.QueryContext = mgrContext.CreateQueryContext()

            Dim cont As Sleepycat.DbXml.Container = mgrContext.OpenContainer(Nothing, Database)

            Dim res As Sleepycat.DbXml.Results = mgrContext.Query(Nothing, Query, context, New Sleepycat.DbXml.DocumentConfig)

            Dim resString As String = ""

            While res.MoveNext
                resString += res.Current.ToString
            End While

            Return resString

        Catch ex As Exception
            Return ex.ToString
        End Try

    End Function
#End Region

#Region " DeleteDatabase() "
    Public Sub DeleteDatabase(ByVal Name As String)
        If Exists(Name) Then
            'IO.File.Delete(EnvironmentPath & "\" & Name & "\*.*")
            IO.Directory.Delete(EnvironmentPath & "\" & Name, True)
        End If
    End Sub
#End Region

#Region " DeleteDocument() "
    Public Function DeleteDocument(ByVal Database As String, ByVal Document As String) As Boolean
        If mgrContext Is Nothing Then mgrContext = CreateManager(EnvironmentPath & "\" & Database)

        Try

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
#End Region

#Region " Exists() "
    Public Function Exists(ByVal Database As String) As Boolean
        Return IO.Directory.Exists(EnvironmentPath & "\" & Database)
    End Function
#End Region

#Region " Dispose() "
    Public Sub Dispose() Implements System.IDisposable.Dispose
        If IsNothing(mgrContext) = False Then mgrContext.Dispose()

    End Sub
#End Region

End Class