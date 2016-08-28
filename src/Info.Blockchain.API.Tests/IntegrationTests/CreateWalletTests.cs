using System;﻿
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class CreateWalletTests
	{
        [Fact(Skip = "service-my-wallet-v3 not mocked")]
		public async void CreateWallet_BadCredentials_ServerApiException()
		{
			//Dont want to spam to create wallets. Check to see if serialization works and get a message from the server
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
				{
					await apiHelper.WalletCreator.Create("badpassword");
				}
			});
		}
	}
}
