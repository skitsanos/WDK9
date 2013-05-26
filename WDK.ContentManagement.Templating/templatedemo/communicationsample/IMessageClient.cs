using System;

namespace Evolve.TemplateEngine.CommSample
{
	/// <summary>
	/// A client that is able to deal with
	/// <see cref="IMessageMaster"/> objects.
	/// </summary>
	public interface IMessageClient
	{

    /// <summary>
    /// Gets the name of the client. Used by the sample web form to list
    /// the clients.
    /// </summary>
    string ClientName
    {
      get;
    }


    /// <summary>
    /// Displays a message.
    /// </summary>
    /// <param name="message">The message to be displayed.</param>
    /// <param name="append">Whether the message should be appended
    /// to previous messages. If set to <c>false</c>, earlier content
    /// is being deleted.</param>
    void DisplayMessage(string message, bool append);

	}
}
