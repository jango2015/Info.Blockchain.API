using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class BlockchainApiHelperTests
	{
		[Fact]
		public void CreateHelper_Valid()
		{
			const string apiCode = "5";
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper(apiCode, new FakeHttpClient()))
			{
				Assert.NotNull(apiHelper);
				Assert.NotNull(apiHelper.StatisticsExplorer);
				Assert.NotNull(apiHelper.BlockExpolorer);
				Assert.NotNull(apiHelper.ExchangeRateExplorer);
				Assert.NotNull(apiHelper.TransactionPusher);
				Assert.Null(apiHelper.WalletCreator);
			}
		}

        [Fact]
        public void CreateHelperWithService_Valid()
        {
            const string apiCode = "5";
            using (BlockchainApiHelper apiHelper = new BlockchainApiHelper(apiCode, new FakeHttpClient(), "http://localhost:3000"))
            {
                Assert.NotNull(apiHelper);
                Assert.NotNull(apiHelper.StatisticsExplorer);
                Assert.NotNull(apiHelper.BlockExpolorer);
                Assert.NotNull(apiHelper.ExchangeRateExplorer);
                Assert.NotNull(apiHelper.TransactionPusher);
                Assert.NotNull(apiHelper.WalletCreator);
            }
        }
    }
}
