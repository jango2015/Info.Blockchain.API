using System.Collections.Generic;
using System.Linq;
using Info.Blockchain.API.BlockExplorer;
using Info.Blockchain.API.Wallet;
using Xunit;
using Address = Info.Blockchain.API.Wallet.Address;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	//Dont actually want to do anything in these tests so most of them are just pinging the server and getting an error
	public class WalletTests
	{
		private const string WALLET_ID = "773d4edb-bffe-4790-8712-8d232ce04b0c";
		private const string WALLET_PASSWORD = "Password1!";
		private const string WALLET_PASSWORD2 = "Password2!";
		private const string FIRST_ADDRESS = "17VYDFsDxBMovM1cKGEytgeqdijNcr4L5";

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void SendPayment_SendBtc_NoFreeOutputs()
		{
			ServerApiException apiException = await Assert.ThrowsAsync<ServerApiException>(async () => {
				using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
				{
					WalletHelper walletHelper = apiHelper.CreateWalletHelper(WalletTests.WALLET_ID, WalletTests.WALLET_PASSWORD, WalletTests.WALLET_PASSWORD2);
					await walletHelper.SendAsync(WalletTests.FIRST_ADDRESS, BitcoinValue.FromBtc(1));
				}
			});
			Assert.Contains("No free", apiException.Message);
		}


        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void SendPayment_SendMultiBtc_NoFreeOutputs()
		{
			ServerApiException apiException = await Assert.ThrowsAsync<ServerApiException>(async () => {
				using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
				{
					WalletHelper walletHelper = apiHelper.CreateWalletHelper(WalletTests.WALLET_ID, WalletTests.WALLET_PASSWORD, WalletTests.WALLET_PASSWORD2);
					Dictionary<string, BitcoinValue> recipients = new Dictionary<string, BitcoinValue>()
					{
						{"17VYDFsDxBMovM1cKGEytgeqdijNcr4L5", BitcoinValue.FromBtc(1)}
					};
					await walletHelper.SendManyAsync(recipients);
				}
			});
			Assert.Contains("No free", apiException.Message);
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void ArchiveAddress_BaddAddress_ServerApiException()
		{
			ServerApiException apiException = await Assert.ThrowsAsync<ServerApiException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
				{
					WalletHelper walletHelper = apiHelper.CreateWalletHelper(WalletTests.WALLET_ID, WalletTests.WALLET_PASSWORD,
						WalletTests.WALLET_PASSWORD2);
					await walletHelper.ArchiveAddress("badAddress");
				}
			});
			Assert.Contains("Checksum", apiException.Message);
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void GetAddresses_Valid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				WalletHelper walletHelper = apiHelper.CreateWalletHelper(WalletTests.WALLET_ID, WalletTests.WALLET_PASSWORD, WalletTests.WALLET_PASSWORD2);
				List<Address> addresses = await walletHelper.ListAddressesAsync();
				Assert.NotNull(addresses);
				Assert.NotEmpty(addresses);
				Assert.True(addresses.Any(a => string.Equals(a.AddressStr, WalletTests.FIRST_ADDRESS)));
			}
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void Unarchive_BadAddress_ServerApiException()
		{
			ServerApiException apiException = await Assert.ThrowsAsync<ServerApiException>(async () => {
				using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
				{
					WalletHelper walletHelper = apiHelper.CreateWalletHelper(WalletTests.WALLET_ID, WalletTests.WALLET_PASSWORD, WalletTests.WALLET_PASSWORD2);
					await walletHelper.UnarchiveAddress("BadAddress");
				}
			});
			Assert.Contains("Checksum", apiException.Message);
		}

        [Fact(Skip = "service-my-wallet-v3 not mocked")]
        public async void NewAddress_ArchiveThenConsolidate_Valid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				WalletHelper walletHelper = apiHelper.CreateWalletHelper(WalletTests.WALLET_ID, WalletTests.WALLET_PASSWORD, WalletTests.WALLET_PASSWORD2);
				Address address = await walletHelper.NewAddress("Test");
				Assert.NotNull(address);

				string archivedAddress = await walletHelper.ArchiveAddress(address.AddressStr);
				Assert.NotNull(archivedAddress);


				string unarchivedAddress = await walletHelper.UnarchiveAddress(archivedAddress);
				Assert.NotNull(unarchivedAddress);
			}
		}
	}
}
