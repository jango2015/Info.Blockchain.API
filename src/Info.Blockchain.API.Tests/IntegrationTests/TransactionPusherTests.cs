using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class TransactionPusherTests
	{
		[Fact]
		public async void PushTransaction_BadTransaction_ServerError()
		{
			//Dont want to add transactions, check to see if the server responds
			ServerApiException serverApiException = await Assert.ThrowsAsync<ServerApiException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
				{
					await apiHelper.TransactionPusher.PushTransactionAsync("Test");
				}
			});
			Assert.Contains("Parse", serverApiException.Message);
		}
	}
}
