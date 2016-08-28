using System;
using System.Threading.Tasks;
using Info.Blockchain.API.Abstractions;

namespace Info.Blockchain.API.CreateWallet
{
	/// <summary>
	/// This class reflects the functionality documented at https://blockchain.info/api/create_wallet.
	/// It allows users to create new wallets as long as their API code has the 'generate wallet' permission.
	/// </summary>
	public class WalletCreator
	{
		private IHttpClient httpClient { get; }
		internal WalletCreator(IHttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		/// <summary>
		/// Creates a new Blockchain.info wallet. It can be created containing a pre-generated private key
		/// or will otherwise generate a new private key.
		/// </summary>
		/// <param name="password">Password for the new wallet. At least 10 characters.</param>
		/// <param name="privateKey">Private key to add to the wallet</param>
		/// <param name="label">Label for the first address in the wallet</param>
		/// <param name="email">Email to associate with the new wallet</param>
		/// <returns>An instance of the CreateWalletResponse class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<CreateWalletResponse> Create(string password, string privateKey = null, string label = null, string email = null)
		{
			if (string.IsNullOrWhiteSpace(password))
			{
			throw new ArgumentNullException(nameof(password));
			}
			if (string.IsNullOrWhiteSpace(this.httpClient.ApiCode))
			{
					throw new ArgumentNullException("Api code must be specified", innerException: null);
			}
			CreateWalletRequest request = new CreateWalletRequest(password, privateKey, label, email);
			CreateWalletResponse walletResponse = await this.httpClient.PostAsync<CreateWalletRequest, CreateWalletResponse>("api/v2/create_wallet", request);
			return walletResponse;
		}
	}
}
