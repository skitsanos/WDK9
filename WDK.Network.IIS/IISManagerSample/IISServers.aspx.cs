using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RealComponents.Net.IIS;

namespace IISManagerSample
{
	/// <summary>
	/// Summary description for IISServers.
	/// </summary>
	public class IISServers : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid grdServers;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divError;
		protected System.Web.UI.WebControls.Label lblWebServerName;
		protected System.Web.UI.WebControls.TextBox txtWebServerName;
		protected System.Web.UI.WebControls.Label lblWebServerRootPath;
		protected System.Web.UI.WebControls.TextBox txtWebServerRootPath;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected IISManager iis = new IISManager();
	
		private void BindData()
		{
			DataTable dtIISServers = new DataTable();
			dtIISServers.Columns.Add("id", typeof(int));
			dtIISServers.Columns.Add("name", typeof(string));
			dtIISServers.Columns.Add("path", typeof(string));
			dtIISServers.Columns.Add("status", typeof(string));
			IISWebServerCollection wsc = iis.GetWebServers();
			for(int i = 0; i < wsc.Count; i++)
			{
				DataRow dr = dtIISServers.NewRow();
				dr["id"] = wsc[i].ID;
				dr["name"] = wsc[i].ServerName;
				dr["path"] = wsc[i].RootPath;
				dr["status"] = wsc[i].Status().ToString();
				dtIISServers.Rows.Add(dr);
			}
			dtIISServers.DefaultView.Sort = "name";
			grdServers.DataSource = dtIISServers.DefaultView;
			grdServers.DataBind();
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			divError.Visible = false;
		}

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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.IISServers_PreRender);

		}
		#endregion

		private void IISServers_PreRender(object sender, System.EventArgs e)
		{
			BindData();
		}

		protected void lnkStop_Click(Object sender, System.EventArgs e)
		{
			try
			{
				iis.StopWebServer(int.Parse((sender as LinkButton).CommandArgument));
			}
			catch(Exception ex)
			{
				divError.InnerText = "* Error during the operation : " + ex.InnerException.Message;
				divError.Visible = true;
			}
		}

		protected void lnkStart_Click(Object sender, System.EventArgs e)
		{
			try
			{
				iis.StartWebServer(int.Parse((sender as LinkButton).CommandArgument));
			}
			catch(Exception ex)
			{
				divError.InnerText = "* Error during the operation : " + ex.InnerException.Message;
				divError.Visible = true;
			}
		}

		protected void lnkPause_Click(Object sender, System.EventArgs e)
		{
			try
			{
				iis.PauseWebServer(int.Parse((sender as LinkButton).CommandArgument));
			}
			catch(Exception ex)
			{
				divError.InnerText = "* Error during the operation : " + ex.InnerException.Message;
				divError.Visible = true;
			}
		}

		protected void lnkDelete_Click(Object sender, System.EventArgs e)
		{
			try
			{
				iis.DeleteWebServer(int.Parse((sender as LinkButton).CommandArgument));
			}
			catch(Exception ex)
			{
				divError.InnerText = "* Error during the operation : " + ex.InnerException.Message;
				divError.Visible = true;
			}
		}

		protected void lnkName_Click(Object sender, System.EventArgs e)
		{
			Session["CurrentServerName"] = (sender as LinkButton).CommandArgument;
			Response.Redirect("IISWebServerDetails.aspx");
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				iis.CreateWebServer(txtWebServerName.Text.Trim(), txtWebServerRootPath.Text.Trim());
				txtWebServerName.Text = "";
				txtWebServerRootPath.Text = "";
			}
			catch(Exception ex)
			{
				divError.InnerText = "* Error during the operation : " + ex.InnerException.Message;
				divError.Visible = true;
			}
		}
	}
}
