namespace Evolve.Site.templates
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Zusammenfassung für MenuTemplate.
	/// </summary>
	public class MenuTemplate : Evolve.Portals.Framework.PortalTemplate
	{
    protected Evolve.Portals.Framework.RegionPlaceHolder ToolPane;
    protected Evolve.Portals.Framework.RegionPlaceHolder MenuPlaceHolder;
    protected Evolve.Portals.Framework.RegionPlaceHolder ContentPlaceHolder;
    protected System.Web.UI.WebControls.Image EvolveLogo;
    protected System.Web.UI.WebControls.HyperLink HomeLink;
    protected System.Web.UI.WebControls.HyperLink CompanyLink;
    protected System.Web.UI.WebControls.HyperLink ProjectLink;
    protected System.Web.UI.WebControls.HyperLink TechnologyLink;
    protected System.Web.UI.WebControls.HyperLink DownloadLink;
    protected System.Web.UI.WebControls.HyperLink LoginLink;
    protected System.Web.UI.WebControls.Label UserName;
    protected System.Web.UI.WebControls.HyperLink HyperLink1;

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
		///		Erforderliche Methode für die Designerunterstützung
		///		Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
      this.Load += new System.EventHandler(this.Page_Load);

    }
		#endregion
	}
}
