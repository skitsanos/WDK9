Imports Db4objects
Imports WDK.ContentManagement.Pages

Public Class ObjectStore

	Private server As Db4o.IObjectServer

	Public Sub Start()
		'Db4objects.Db4o.Db4oFactory.Configure.ObjectClass(GetType(PageType)).CascadeOnUpdate(True)
		'Db4objects.Db4o.Db4oFactory.Configure.ObjectClass(GetType(PageType)).CascadeOnDelete(True)
		'Db4objects.Db4o.Db4oFactory.Configure.ObjectClass(GetType(PageType)).UpdateDepth(2)

		server = Db4o.Db4oFactory.OpenServer(AppDomain.CurrentDomain.BaseDirectory + "data.odb", 5560)
		server.GrantAccess("sample", "sample")
	End Sub

	Public Sub [Stop]()
		server.Close()
	End Sub

End Class
