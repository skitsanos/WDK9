using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evolve.Portals.Framework;

namespace Evolve.TemplateEngine.CommSample
{
	/// <summary>
	/// Zusammenfassung für communicationsample.
	/// </summary>
	public class ParentClass : Page, IMessageMaster
	{
    #region controls

	  protected RegionProvider regionProvider1;
	  private IContainer components;
    protected System.Web.UI.WebControls.TextBox MessageText;
    protected System.Web.UI.WebControls.Button SendMessage;
	  #endregion
    protected System.Web.UI.WebControls.DropDownList ClientList;



    /// <summary>
    /// A list of <see cref="IMessageClient"/> objects
    /// that registered themselves at runtime.
    /// </summary>
    private ArrayList clients = new ArrayList();

    private void Page_Load(object sender, EventArgs e)
    {  }


    #region IMessageMaster Member

    /// <summary>
    /// Registers a message client (implemented by the
    /// child controls).
    /// </summary>
    /// <param name="client">The client which gets messages
    /// by the master.</param>
    public void RegisterClient(IMessageClient client)
    {
      //store reference
      this.clients.Add(client);

      //add client to dropdown
      if (!this.IsPostBack)
      {
        ListItem item = new ListItem(client.ClientName, this.clients.IndexOf(client).ToString());
        this.ClientList.Items.Add(item);
      }
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
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{    
      this.components = new System.ComponentModel.Container();
      this.regionProvider1 = new Evolve.Portals.Framework.RegionProvider(this.components);
      this.SendMessage.Click += new System.EventHandler(this.SendMessage_Click);
      // 
      // regionProvider1
      // 
      this.regionProvider1.DefaultRegion = Evolve.Portals.Framework.PortalRegion.Content;
      this.regionProvider1.HostingPage = this;
      this.regionProvider1.RegionTemplatePath = "template.ascx";
      this.regionProvider1.TemplatingTime = Evolve.Portals.Framework.TemplatingTime.OnInit;
      this.Load += new System.EventHandler(this.Page_Load);

    }
		#endregion


    #region button click

	  /// <summary>
	  /// Sends a message to all / a specific client.
	  /// </summary>
	  /// <param name="sender"></param>
	  /// <param name="e"></param>
	  private void SendMessage_Click(object sender, EventArgs e)
	  {
      int index = int.Parse(this.ClientList.SelectedValue);
	    foreach (IMessageClient client in this.clients)
	    {
        if (index == -1 || this.clients.IndexOf(client) == index)
        {
          client.DisplayMessage(this.MessageText.Text, false);
        }
	    }
	  }

	  #endregion
	}
}
