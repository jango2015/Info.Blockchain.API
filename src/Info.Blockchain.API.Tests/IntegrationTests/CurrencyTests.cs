using System.Collections.Generic;
using Info.Blockchain.API.ExchangeRates;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class CurrencyTests
	{
		[Fact]
		public async void GetTicker_Valid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				Dictionary<string, Currency> currencies = await apiHelper.ExchangeRateExplorer.GetTickerAsync();
				Assert.NotNull(currencies);
				Assert.True(currencies.Count > 0);
			}
		}

		[Fact]
		public async void ToBtc_FromUs_HasValue()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				double btcValue = await apiHelper.ExchangeRateExplorer.ToBtcAsync("USD", 1000);
				Assert.True(btcValue > 0);
			}
		}
	}
}
