using System;
using System.Collections.ObjectModel;
using Info.Blockchain.API.BlockExplorer;
using KellermanSoftware.CompareNetObjects;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class BlockTests
	{
		private Block GetSingleBlock()
		{
			Block block = ReflectionUtil.DeserializeFile<Block>("single_block", Block.Deserialize);
			return block;
		}

		[Fact]
		public async void GetLatestBlock_NotNull()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				LatestBlock latestBlock = await apiHelper.BlockExpolorer.GetLatestBlockAsync();
				Assert.NotNull(latestBlock);
			}
		}


		[Fact]
		public async void GetBlocksAtHeight_Height100000_IsValid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				const int height = 100000;
				ReadOnlyCollection<Block> knownBlocks = ReflectionUtil.DeserializeFile("blocks_height_" + height, Block.DeserializeMultiple);
				ReadOnlyCollection<Block> receivedBlocks = await apiHelper.BlockExpolorer.GetBlocksAtHeightAsync(height);

				ComparisonResult comparisonResult = new CompareLogic().Compare(knownBlocks, receivedBlocks);
				bool areEqual = comparisonResult.AreEqual;
				Assert.True(areEqual);
			}
		}

		[Fact]
		public async void GetBlocks_ByTimestamp_IsValid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				const long unixMillis = 1293623863000;
				ReadOnlyCollection<SimpleBlock> knownBlocks = ReflectionUtil.DeserializeFile("blocks_timestamp_" + 1293623863000, SimpleBlock.DeserializeMultiple);
				ReadOnlyCollection<SimpleBlock> receivedBlocks = await apiHelper.BlockExpolorer.GetBlocksAsync(unixMillis);

				ComparisonResult comparisonResult = new CompareLogic().Compare(knownBlocks, receivedBlocks);
				bool areEqual = comparisonResult.AreEqual;
				Assert.True(areEqual);
			}
		}

		[Fact]
		public async void GetBlocks_ByDateTime_IsValid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				const long unixMillis = 1293623863000;
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(unixMillis);
				ReadOnlyCollection<SimpleBlock> knownBlocks = ReflectionUtil.DeserializeFile("blocks_timestamp_" + 1293623863000, SimpleBlock.DeserializeMultiple);
				ReadOnlyCollection<SimpleBlock> receivedBlocks = await apiHelper.BlockExpolorer.GetBlocksAsync(dateTime);

				ComparisonResult comparisonResult = new CompareLogic().Compare(knownBlocks, receivedBlocks);
				bool areEqual = comparisonResult.AreEqual;
				Assert.True(areEqual);
			}
		}

		[Fact]
		public async void GetBlocks_ByPool_IsValid()
		{
			using (BlockchainApiHelper apiHelper = new BlockchainApiHelper())
			{
				const string poolName = "AntPool";
				ReadOnlyCollection<SimpleBlock> receivedBlocks = await apiHelper.BlockExpolorer.GetBlocksAsync(poolName);
				
				Assert.NotNull(receivedBlocks);
			}
		}
	}
}