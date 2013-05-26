using System.Web.UI;

namespace templatedemo.nestedtemplates
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Zusammenfassung für nestedtemplate.
	/// </summary>
	public class nestedtemplate : UserControl
	{
    protected Evolve.Portals.Framework.RegionPlaceHolder ContentPlaceHolder;

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
