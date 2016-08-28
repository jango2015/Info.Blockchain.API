using System;
using System.Threading.Tasks;
using Info.Blockchain.API.Abstractions;
using Info.Blockchain.API.CreateWallet;
using Xunit;

namespace Info.Blockchain.API.Tests.UnitTests
{
	public class CreateWalletTests
	{
        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void CreateWallet_NullPassword_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper("APICODE"))
				{
					await apiHelper.WalletCreator.Create(null);
				}
			});
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void CreateWallet_NullApiCode_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.WalletCreator.Create("password");
				}
			});
		}


        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void CreateWallet_MockRequest_Valid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper(baseHttpClient: new MockCreateWalletHttpClient()))
			{
				CreateWalletResponse walletResponse = await apiHelper.WalletCreator.Create("Password");
				Assert.NotNull(walletResponse);

				Assert.Equal(walletResponse.Address, "12AaMuRnzw6vW6s2KPRAGeX53meTf8JbZS");
				Assert.Equal(walletResponse.Identifier, "4b8cd8e9-9480-44cc-b7f2-527e98ee3287");
				Assert.Equal(walletResponse.Label, "My Blockchain Wallet");
			}
		}

		public class MockCreateWalletHttpClient : IHttpClient
		{
			public void Dispose()
			{
			}

			public string ApiCode { get; set; } = "Test";

			public Task<T> GetAsync<T>(string route, QueryString queryString = null, Func<string, T> customDeserialization = null)
			{
				throw new NotImplementedException();
			}

			public Task<TResponse> PostAsync<TPost, TResponse>(string route, TPost postObject,
				Func<string, TResponse> customDeserialization = null,
				bool multiPartContent = false)
			{
				CreateWalletResponse walletResponse = ReflectionUtil.DeserializeFile<CreateWalletResponse>("create_wallet_mock");
				if (walletResponse is TResponse)
				{
					return Task.FromResult((TResponse) (object) walletResponse);
				}
				return Task.FromResult(default(TResponse));
			}
		}
	}
}
