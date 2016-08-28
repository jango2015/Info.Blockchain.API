using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Info.Blockchain.API.CreateWallet
{
	internal class CreateWalletRequest
	{
		[JsonProperty("email")]
		public string Email { get; }
		[JsonProperty("label")]
		public string Label { get; }
		[JsonProperty("password")]
		public string Password { get; }
		[JsonProperty("priv")]
		public string PrivateKey { get; }

		public CreateWalletRequest(string password, string privateKey = null, string label = null, string email = null)
		{
			this.Password = password;
			this.PrivateKey = privateKey;
			this.Label = label;
			this.Email = email;
		}
	}

	/// <summary>
	/// This class is used in response to the `Create` method in the `CreateWallet` class.
	/// </summary>
	public class CreateWalletResponse
	{
		[JsonConstructor]
		private CreateWalletResponse()
		{
		}

		/// <summary>
		/// Wallet identifier (GUID)
		/// </summary>
		[JsonProperty("guid", Required = Required.Always)]
		public string Identifier { get; private set; }

		/// <summary>
		/// First address in the wallet
		/// </summary>
		[JsonProperty("address", Required = Required.Always)]
		public string Address { get; private set; }

		/// <summary>
		/// Wallet label
		/// </summary>
		[JsonProperty("label", Required = Required.Always)]
		public string Label { get; private set; }
	}
}
