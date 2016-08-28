using System.Collections.ObjectModel;
using Info.Blockchain.API.BlockExplorer;
using KellermanSoftware.CompareNetObjects;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class TransactionTests
	{
		[Fact]
		public async void GetTransaction_ByHash_Valid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				Transaction knownTransaction = ReflectionUtil.DeserializeFile<Transaction>("single_transaction");
				Transaction receivedTransaction = await apiHelper.BlockExpolorer.GetTransactionAsync(knownTransaction.Hash);

				CompareLogic compareLogic = new CompareLogic();
				ComparisonResult comparisonResult = compareLogic.Compare(knownTransaction, receivedTransaction);
				Assert.True(comparisonResult.AreEqual);
			}
		}

		[Fact]
		public async void GetTransaction_ByIndex_Valid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				Transaction knownTransaction = ReflectionUtil.DeserializeFile<Transaction>("single_transaction");
				Transaction receivedTransaction = await apiHelper.BlockExpolorer.GetTransactionByIndexAsync(knownTransaction.Index);

				CompareLogic compareLogic = new CompareLogic();
				ComparisonResult comparisonResult = compareLogic.Compare(knownTransaction, receivedTransaction);
				Assert.True(comparisonResult.AreEqual);
			}
		}

		[Fact]
		public async void GetUnconfirmedTransaction_Valid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				ReadOnlyCollection<Transaction> unconfirmedTransactions = await apiHelper.BlockExpolorer.GetUnconfirmedTransactionsAsync();
				
				Assert.NotNull(unconfirmedTransactions);
			}
		}
	}
}
