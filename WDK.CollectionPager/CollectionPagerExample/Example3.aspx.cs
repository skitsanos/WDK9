using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollectionPagerExample
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class Example3 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Repeater Repeater1;
		protected SiteUtils.CollectionPager CollectionPager1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl DIV1;
		protected System.Web.UI.WebControls.Button Button1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Create DataSet
			DataSet SampleDataSet = SampleData();

			//Set DataSource of Pager to Sample Data
			CollectionPager1.DataSource = SampleDataSet.Tables[0].DefaultView;
			
			//Let the Pager know what Control it needs to DataBind during the PreRender	
			CollectionPager1.BindToControl = Repeater1;

			//Set the DataSource of the Repeater to the PagedData coming from the Pager.
			Repeater1.DataSource = CollectionPager1.DataSourcePaged;
		}

		#region Create Sample DataSet

		private DataSet SampleData()
		{
			DataSet ds = new DataSet();
			DataTable dt = ds.Tables.Add("SampleData");

			dt.Columns.Add("Column1", typeof(string));
			dt.Columns.Add("Column2", typeof(string));
			dt.Columns.Add("Column3", typeof(string));
			dt.Columns.Add("Column4", typeof(string));
			dt.Columns.Add("Column5", typeof(string));

			DataRow dr;
			for(int i=1;i<=250;i++)
			{
				dr = dt.NewRow();
				dr["Column1"] = "Cell1:"+i.ToString();
				dr["Column2"] = "Cell2:"+i.ToString();
				dr["Column3"] = "Cell3:"+i.ToString();
				dr["Column4"] = "Cell4:"+i.ToString();
				dr["Column5"] = "Cell5:"+i.ToString();
				dt.Rows.Add(dr);
			}
			return ds;
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
