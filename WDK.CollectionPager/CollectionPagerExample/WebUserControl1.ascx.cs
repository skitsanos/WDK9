using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CollectionPagerExample
{
	public class WebUserControl1 : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Repeater RepeaterInUC;

		//*** YOU NEED TO HAVE A PUBLIC ACCESSOR FOR YOUR REPEATER
		//*** OTHERWISE YOU WILL NOT BE ABLE TO ACCESS IT FROM THE PARENT PAGE.

		public Repeater PublicRepeaterInUC
		{
			get{return RepeaterInUC;}
			set{RepeaterInUC = value;}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
