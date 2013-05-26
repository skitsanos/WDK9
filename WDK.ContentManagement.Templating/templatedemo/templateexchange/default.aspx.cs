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
using Evolve.Portals.Framework;

namespace templatedemo
{
	/// <summary>
	/// Simple sample page that works with the template engine.
	/// </summary>
	public class _default : System.Web.UI.Page
	{

    #region controls

    protected System.Web.UI.WebControls.Image Image1;
    protected Evolve.Portals.Framework.RegionProvider regionProvider1;
    protected Evolve.Portals.Framework.HelperPanel HelperPanel1;
    protected System.Web.UI.WebControls.Button Button1;
    private System.ComponentModel.IContainer components;

    #endregion

  
		private void Page_Load(object sender, System.EventArgs e)
		{
      if (!this.IsPostBack)
      {
        this.RenderTemplate();
      }
		}

		#region Vom Web Form-Designer generierter Code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{    
      this.components = new System.ComponentModel.Container();
      this.regionProvider1 = new Evolve.Portals.Framework.RegionProvider(this.components);
      this.Button1.Click += new System.EventHandler(this.Button1_Click);
      // 
      // regionProvider1
      // 
      this.regionProvider1.HostingPage = this;
      this.regionProvider1.PropertySets.Add(new Evolve.Portals.Framework.RegionPropertySet(this.Image1, Evolve.Portals.Framework.PortalRegion.Left1, 0));
      this.regionProvider1.PropertySets.Add(new Evolve.Portals.Framework.RegionPropertySet(this.HelperPanel1, Evolve.Portals.Framework.PortalRegion.Content, 0));
      this.regionProvider1.RegionTemplatePath = "../templates/menutemplate.ascx";
      this.regionProvider1.TemplatingTime = Evolve.Portals.Framework.TemplatingTime.Manual;
      this.Load += new System.EventHandler(this.Page_Load);

    }
		#endregion


    /// <summary>
    /// This event switches the used template. I had to change the
    /// templating time from OnLoad to PreRender, as OnLoad happens
    /// before this event is being called.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button1_Click(object sender, System.EventArgs e)
    {
      this.RenderTemplate();
    }


    /// <summary>
    /// Merges the web form with one of the available templates.
    /// </summary>
    private void RenderTemplate()
    {
      //quick and dirty...     
      string t1 = "../templates/menutemplate.ascx";
      string t2 = "../templates/alternativetemplate.ascx";

      string current = Session["Template"] as string;
      if (current == null || current.Equals(t2))
      {
        current = t1;
      }
      else
      {
        current = t2;
      }

      this.regionProvider1.RegionTemplatePath = current;
      Session["Template"] = current;

      TemplateRenderer.PerformTemplating(this, this.regionProvider1);
    }


	}
}
