using System;
using System.Threading.Tasks;
using Info.Blockchain.API.Abstractions;

namespace Info.Blockchain.API.PushTx
{
	/// <summary>
	/// This class reflects the functionality provided at https://blockchain.info/pushtx. 
	/// It allows users to broadcast hex encoded transactions to the bitcoin network.
	/// </summary>
	public class TransactionPusher
	{
		private IHttpClient httpClient { get; }
		internal TransactionPusher(IHttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		/// <summary>
		/// Pushes a hex encoded transaction to the network.
		/// </summary>
		/// <param name="transactionString">Hex encoded transaction</param>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task PushTransactionAsync(string transactionString)
		{
			if (string.IsNullOrWhiteSpace(transactionString))
			{
				throw new ArgumentNullException(nameof(transactionString));
			}
			await this.httpClient.PostAsync<string, object>("pushtx", transactionString, multiPartContent: true);
		}
	}
}
