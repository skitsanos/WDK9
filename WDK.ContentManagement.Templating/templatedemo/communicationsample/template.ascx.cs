using System;
using System.Web.UI;
using Evolve.Portals.Framework;

namespace Evolve.TemplateEngine.CommSample
{
  /// <summary>
	///		Zusammenfassung für template.
	/// </summary>
	public class SampleTemplate : PortalTemplate, IMessageClient
	{
    protected System.Web.UI.WebControls.Label Message;
    protected RegionPlaceHolder RegionPlaceHolder1;


    /// <summary>
    /// Gets the name of the client. This implementation just returns
    /// the class name.
    /// </summary>
    public string ClientName
    {
      get { return this.GetType().Name; }
    }



		private void Page_Load(object sender, EventArgs e)
		{
    }


    /// <summary>
    /// This method is called by the template engine. It's one
    /// of the initialization methods. Use these ones rather than
    /// <see cref="Page_Load"/>.
    /// <seealso cref="AfterTemplating"/>
    /// </summary>
    /// <param name="page">The master page that loads the template.</param>
    public override void BeforeTemplating(Page page)
    {
      //if the page is a message controller, register myself
      IMessageMaster master = page as IMessageMaster;
      if (master != null)
      {
        master.RegisterClient(this);
      }
    }


    #region IMessageClient interface implementation (called by master page)

    /// <summary>
    /// Displays a message.
    /// </summary>
    /// <param name="message">The message to be displayed.</param>
    /// <param name="append">Whether the message should be appended
    /// to previous messages. If set to <c>false</c>, earlier content
    /// is being deleted.</para
    public void DisplayMessage(string message, bool append)
    {
      if (append)
        this.Message.Text += "<br/>" + message;
      else
        this.Message.Text = message;
    }

    #endregion



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
