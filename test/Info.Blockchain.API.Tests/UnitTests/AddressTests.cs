using System;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class AddressTests
	{
		[Fact]
		public async void GetAddress_NullHash_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.BlockExpolorer.GetAddressAsync(null);
				}
			});
		}

		[Fact]
		public async void GetAddress_NegativeTransactions_ArgumentOutOfRangeException()
		{
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.BlockExpolorer.GetAddressAsync("test", -1);
				}
			});
		}
	}
}
