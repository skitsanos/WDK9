using System;

namespace Evolve.TemplateEngine.CommSample
{
	/// <summary>
	/// The interface of parent controls that are able
	/// to deal with <see cref="IMessageClient"/> objects.
	/// </summary>
	public interface IMessageMaster
	{

    /// <summary>
    /// Registers a message client (implemented by the
    /// child controls).
    /// </summary>
    /// <param name="client">The client which gets messages
    /// by the master.</param>
    void RegisterClient(IMessageClient client);

	}
}
