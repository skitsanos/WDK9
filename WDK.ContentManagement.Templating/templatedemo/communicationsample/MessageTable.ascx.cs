using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evolve.TemplateEngine.CommSample
{
  /// <summary>
	///		Zusammenfassung für bluetemplate.
	/// </summary>
	public class MessageTable : UserControl, IMessageClient
	{
    protected Label Message;

    /// <summary>
    /// Gets the name of the client. This implementation just returns
    /// the ID of the control.
    /// </summary>
    public string ClientName
    {
      get { return this.ID; }
    }


    #region initialization

    /// <summary>
    /// Initializes the control and registers itself with the
    /// master page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Page_Load(object sender, EventArgs e)
    {
      //if the page is a message controller, register myself
      IMessageMaster master = this.Page as IMessageMaster;
      if (master != null)
      {
        master.RegisterClient(this);
      }
    }

    #endregion

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
      this.Load += new EventHandler(this.Page_Load);

    }
		#endregion

  }
}
