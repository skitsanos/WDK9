using Evolve.Portals.Framework;

namespace templatedemo.templateinitialization
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Zusammenfassung für initializationtemplate.
	/// </summary>
	public class initializationtemplate : PortalTemplate
	{
    #region controls

	  protected Label PageBeforeTemplating;
	  protected Label PageAfterTemplating;
	  protected Label ChildsAfterTemplating;
	  protected RegionPlaceHolder RegionPlaceHolder1;
	  protected Label ParameterBefore;
	  protected Label ParameterAfter;
    protected System.Web.UI.WebControls.Label PageLoadStatus;
	  protected Label ChildsBeforeTemplating;

	  #endregion


    /// <summary>
    /// Do NOT rely on this one.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	  private void Page_Load(object sender, System.EventArgs e)
		{
      this.PageLoadStatus.Text = "Has been called. Controls: " + this.Controls.Count.ToString();
		}


    /// <summary>
    /// Called by the framework before the template is merged with the
    /// master page.
    /// </summary>
    /// <param name="page">The master page object.</param>
    public override void BeforeTemplating(System.Web.UI.Page page)
    {
      PageBeforeTemplating.Text = (this.Page == null) ? "null" : this.Page.GetType().Name;
      this.ParameterBefore.Text = page.GetType().Name;
      this.ChildsBeforeTemplating.Text = this.Controls.Count.ToString();
      base.BeforeTemplating (page);
    }


    /// <summary>
    /// Called by the framework after the template has been merged with the
    /// master page.
    /// </summary>
    /// <param name="page">The master page object.</param>
    public override void AfterTemplating(System.Web.UI.Page page)
    {
      PageAfterTemplating.Text = (this.Page == null) ? "null" : this.Page.GetType().Name;
      this.ParameterAfter.Text = page.GetType().Name;
      this.ChildsAfterTemplating.Text = this.Controls.Count.ToString();
      base.BeforeTemplating (page);

      base.AfterTemplating (page);
    }



    #region designer code
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
