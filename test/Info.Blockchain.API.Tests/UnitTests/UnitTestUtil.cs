namespace Info.Blockchain.API.Tests.UnitTests
{
	internal static class UnitTestUtil
	{
		internal static BlockchainApiHelper GetFakeHelper(string apiCode = null)
		{
			return new BlockchainApiHelper(apiCode, new FakeHttpClient());
		}
	}
}