using System;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class TransactionPusherTests
	{
		[Fact]
		public async void GetTransaction_BadIds_ArgumentExecption()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.TransactionPusher.PushTransactionAsync(null);
				}
			});
		}
	}
}
