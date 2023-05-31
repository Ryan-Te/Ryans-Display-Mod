using LogicAPI.Client;
using RyansDisplayMod.Client;

namespace RyansDisplayMod.Client
{
	public class RyansDisplayModClient : ClientMod
	{
		protected override void Initialize()
		{
			Logger.Info("RyansDisplayMod - Client Loaded");
		}
	}
}