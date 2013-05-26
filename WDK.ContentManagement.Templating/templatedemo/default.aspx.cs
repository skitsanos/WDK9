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

namespace templatedemo
{
	/// <summary>
	/// Zusammenfassung für _default1.
	/// </summary>
	public class MainPage : System.Web.UI.Page
	{
    protected Evolve.Portals.Framework.RegionProvider regionProvider1;
    protected System.Web.UI.WebControls.Image Image1;
    private System.ComponentModel.IContainer components;
  
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Hier Benutzercode zur Seiteninitialisierung einfügen
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
      // 
      // regionProvider1
      // 
      this.regionProvider1.DefaultRegion = Evolve.Portals.Framework.PortalRegion.Content;
      this.regionProvider1.HostingPage = this;
      this.regionProvider1.PropertySets.Add(new Evolve.Portals.Framework.RegionPropertySet(this.Image1, Evolve.Portals.Framework.PortalRegion.Left1, 0));
      this.regionProvider1.RegionTemplatePath = "templates/MenuTemplate.ascx";
      this.Load += new System.EventHandler(this.Page_Load);

    }
		#endregion
	}
}
