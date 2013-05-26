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
	/// Summary description for IISWebServerDetails.
	/// </summary>
	public class IISWebServerDetails : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblWebVirtDirName;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.Label lblWebVirtDirRootPath;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Label lblVirtDirApp;
		protected System.Web.UI.WebControls.CheckBox chkApplication;
		protected System.Web.UI.WebControls.DataGrid grdVirtualDirectories;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divServerName;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divError;
		protected System.Web.UI.WebControls.TextBox txtVirtualDirectoryName;
		protected System.Web.UI.WebControls.TextBox txtVirtualDirectoryRootPath;
		protected System.Web.UI.WebControls.Button btnBack;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divWebServerName;
		protected System.Web.UI.WebControls.DataGrid grdWebDirectories;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected IISManager iis = new IISManager();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			divWebServerName.EnableViewState = false;
			divServerName.InnerText = divServerName.InnerText.Replace("{0}", (string)Session["CurrentServerName"]);
			divWebServerName.InnerText = divWebServerName.InnerText.Replace("{0}", (string)Session["CurrentServerName"]);
			divError.Visible = false;
		}

		private void BindData()
		{
			DataTable dtIISVirtualDirs = new DataTable();
			dtIISVirtualDirs.Columns.Add("name", typeof(string));
			dtIISVirtualDirs.Columns.Add("path", typeof(string));
			dtIISVirtualDirs.Columns.Add("isApplication", typeof(string));
			IISWebServer ws = iis.GetWebServer((string)Session["CurrentServerName"]);
			Session["VirtualDirectories"] = ws.VirtualDirectories;
			for(int i = 0; i < ws.VirtualDirectories.Count; i++)
			{
				DataRow dr = dtIISVirtualDirs.NewRow();
				dr["name"] = ws.VirtualDirectories[i].Name;
				dr["path"] = ws.VirtualDirectories[i].Path;
				dr["isApplication"] = ws.VirtualDirectories[i].IsApplication.ToString();
				dtIISVirtualDirs.Rows.Add(dr);
			}
			dtIISVirtualDirs.DefaultView.Sort = "name";
			grdVirtualDirectories.DataSource = dtIISVirtualDirs.DefaultView;
			grdVirtualDirectories.DataBind();
			if(Session["WebDirectories"]== null)
			{
				ArrayList _arr = new ArrayList();
				_arr.Add(ws.WebDirectories);
				Session["WebDirectories"] = _arr;
			}
			dtIISVirtualDirs.Rows.Clear();
			ArrayList _al = ((ArrayList)Session["WebDirectories"]);
			if(_al.Count < 2)
				btnReturn.Visible = false;
			else
				btnReturn.Visible = true;
//			if(_al.Count > 1)
//				divWebServerName += " Path: " + ((IISWebDirectoryCollection)_al[_al.Count - 2]).
			IISWebDirectoryCollection _wdc = (IISWebDirectoryCollection)_al[_al.Count - 1];
			for(int i = 0; i < _wdc.Count; i++)
			{
				DataRow dr = dtIISVirtualDirs.NewRow();
				dr["name"] = _wdc[i].Name;
				dr["path"] = _wdc[i].Path;
				dr["isApplication"] = _wdc[i].IsApplication.ToString();
				dtIISVirtualDirs.Rows.Add(dr);
			}
			dtIISVirtualDirs.DefaultView.Sort = "name";
			grdWebDirectories.DataSource = dtIISVirtualDirs.DefaultView;
			grdWebDirectories.DataBind();
			if( Session["VirtualPath"] != null)
				divWebServerName.InnerText += Session["VirtualPath"];

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
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.IISWebServerDetails_PreRender);

		}
		#endregion

		private void IISWebServerDetails_PreRender(object sender, System.EventArgs e)
		{
			BindData();
		}

		protected void lnkDelete_Click(Object sender, System.EventArgs e)
		{
			try
			{
				IISWebServer ws = iis.GetWebServer((string)Session["CurrentServerName"]);
				ws.DeleteVirtualDirectory((sender as LinkButton).CommandArgument);
			}
			catch(Exception ex)
			{
				divError.InnerText = "* Error during the operation : " + ex.InnerException.Message;
				divError.Visible = true;
			}
		}

		protected void lnkCreateApplication_Click(Object sender, System.EventArgs e)
		{
			try
			{
				IISWebServer ws = iis.GetWebServer((string)Session["CurrentServerName"]);
				ws.CreateApplication((sender as LinkButton).CommandArgument);
			}
			catch(Exception ex)
			{
				divError.InnerText = "* Error during the operation : " + ex.InnerException.Message;
				divError.Visible = true;
			}
		}

		protected void lnkDeleteApplication_Click(Object sender, System.EventArgs e)
		{
			try
			{
				IISWebServer ws = iis.GetWebServer((string)Session["CurrentServerName"]);
				ws.DeleteApplication((sender as LinkButton).CommandArgument);
			}
			catch(Exception ex)
			{
				divError.InnerText = "* Error during the operation : " + ex.InnerException.Message;
				divError.Visible = true;
			}
		}

		protected void lnkRestartApplication_Click(Object sender, System.EventArgs e)
		{
			try
			{
				IISWebServer ws = iis.GetWebServer((string)Session["CurrentServerName"]);
				ws.GetVirtualDirectory((sender as LinkButton).CommandArgument).RestartApplication();
			}
			catch(Exception ex)
			{
				divError.InnerText = "* Error during the operation : " + ex.InnerException.Message;
				divError.Visible = true;
			}
		}

		protected void lnkName_Click(Object sender, System.EventArgs e)
		{
			IISWebVirtualDirectoryCollection _vdc = (IISWebVirtualDirectoryCollection)Session["VirtualDirectories"];
			for(int i = 0; i < _vdc.Count; i++)
			{
				string sName = ((LinkButton)sender).CommandArgument;
				if(_vdc[i].Name.Equals(sName))
				{
					ArrayList _al = (ArrayList)Session["WebDirectories"];
					IISWebDirectoryCollection _wdc = (IISWebDirectoryCollection)_al[0];
					_al.Clear();
					_al.Add(_wdc);
					_al.Add(_vdc[i].WebDirectories);
					Session["VirtualPath"] = "Path: /" + _vdc[i].Name;
					break;
				}
			}
		}

		protected void lnkWebName_Click(Object sender, System.EventArgs e)
		{
			ArrayList _al = (ArrayList)Session["WebDirectories"];
			IISWebDirectoryCollection _wdc = (IISWebDirectoryCollection)_al[_al.Count - 1];
			for(int i = 0; i < _wdc.Count; i++)
			{
				string sName = ((LinkButton)sender).CommandArgument;
				if(_wdc[i].Name.Equals(sName))
				{
					Session["VirtualPath"] = " Path: " + _wdc[i].Path;
					_al.Add(_wdc[i].NestedWebDirectories);
					break;
				}
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				IISWebServer ws = iis.GetWebServer((string)Session["CurrentServerName"]);
				ws.CreateVirtualDirectory(txtVirtualDirectoryName.Text.Trim(), txtVirtualDirectoryRootPath.Text.Trim(),chkApplication.Checked);
				txtVirtualDirectoryName.Text = "";
				txtVirtualDirectoryRootPath.Text = "";
				chkApplication.Checked = false;
			}
			catch(Exception ex)
			{
				divError.InnerText = "* Error during the operation : " + ex.InnerException.Message;
				divError.Visible = true;
			}
		}

		private void btnBack_Click(object sender, System.EventArgs e)
		{
				Session["CurrentServerName"] = null;
				Session["WebDirectories"] = null;
				Session["VirtualDirectories"] = null;
				Session["VirtualPath"] = null;
				Response.Redirect("IISServers.aspx");
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			ArrayList _al = (ArrayList)Session["WebDirectories"];
			_al.RemoveAt(_al.Count - 1);
			if(_al.Count == 1) 
				Session["VirtualPath"] = null;
			else
			{
				string[] sArr =  ((string)Session["VirtualPath"]).Split('/');
				Session["VirtualPath"] = string.Join("/",sArr,0, sArr.Length - 1);
			}
		}
	}
}
