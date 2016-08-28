using System.Threading.Tasks;
using Info.Blockchain.API.Abstractions;

namespace Info.Blockchain.API.Statistics
{
	/// <summary>
	/// This class allows users to get the bitcoin network statistics.
	/// It reflects the functionality documented at https://blockchain.info/api/charts_api
	/// </summary>
	public class StatisticsExplorer
	{
		private IHttpClient httpClient { get; }
		internal StatisticsExplorer(IHttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		/// <summary>
		/// Gets the network statistics.
		/// </summary>
		/// <returns>An instance of the StatisticsResponse class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<StatisticsResponse> GetAsync()
		{
			QueryString queryString = new QueryString();
			queryString.Add("format", "json");

			StatisticsResponse statisticsResponse = await this.httpClient.GetAsync<StatisticsResponse>("stats", queryString);
			return statisticsResponse;
		}
	}
}
