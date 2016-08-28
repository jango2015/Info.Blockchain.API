using System;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class InventoryDataTests
	{
		[Fact]
		public async void GetInventoryData_NullHash_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.BlockExpolorer.GetInventoryDataAsync(null);
				}
			});
		} 
	}
}