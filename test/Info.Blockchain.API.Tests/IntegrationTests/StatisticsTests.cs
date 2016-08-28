using Info.Blockchain.API.Statistics;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class StatisticsTests
	{
		[Fact]
		public async void GetStatistics_Valid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				StatisticsResponse statisticsResponse = await apiHelper.StatisticsExplorer.GetAsync();
				Assert.NotNull(statisticsResponse);
			}
		}
	}
}
