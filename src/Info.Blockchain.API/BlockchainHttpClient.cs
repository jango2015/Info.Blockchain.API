using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Info.Blockchain.API.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Info.Blockchain.API
{
	internal class BlockchainHttpClient : IHttpClient
	{
        private const int TIMEOUT_MS = 100000;
        private const string BASE_URI = "https://blockchain.info";
        private HttpClient httpClient { get; }
        public string ApiCode { get; set; }

		public BlockchainHttpClient(string apiCode, string uri= BlockchainHttpClient.BASE_URI)
        {
			this.ApiCode = apiCode;
			this.httpClient = new HttpClient
			{
				BaseAddress = new Uri(uri),
				Timeout = TimeSpan.FromMilliseconds(BlockchainHttpClient.TIMEOUT_MS)
			};
		}

		public async Task<T> GetAsync<T>(string route, QueryString queryString = null, Func<string, T> customDeserialization = null)
		{
			if (route == null)
			{
				throw new ArgumentNullException(nameof(route));
			}

			if (this.ApiCode != null)
			{
				queryString?.Add("api_code", this.ApiCode);
			}
			if (queryString != null && queryString.Count > 0)
			{

				int queryStringIndex = route.IndexOf('?');
				if (queryStringIndex >= 0)
				{
					//Append to querystring
					string queryStringValue = queryStringIndex.ToString();
					queryStringValue = "&" + queryStringValue.Substring(1); //replace questionmark with &
					route += queryStringValue;
				}
				else
				{
					route += queryString.ToString();
				}
			}
			HttpResponseMessage response = await this.httpClient.GetAsync(route);
			string responseString = await this.ValidateResponse(response);
			var responseObject = customDeserialization == null
				? JsonConvert.DeserializeObject<T>(responseString)
				: customDeserialization(responseString);
			return responseObject;
		}

		public async Task<TResponse> PostAsync<TPost, TResponse>(string route, TPost postObject, Func<string, TResponse> customDeserialization = null, bool multiPartContent = false)
		{
			if (route == null)
			{
				throw new ArgumentNullException(nameof(route));
			}
			if (this.ApiCode != null)
			{
				route += "?api_code=" + this.ApiCode;
			}
			string json = JsonConvert.SerializeObject(postObject);
			HttpContent httpContent;
			if (multiPartContent)
			{
				httpContent = new MultipartFormDataContent
				{
					new StringContent(json, Encoding.UTF8, "application/x-www-form-urlencoded")
				};
			}
			else
			{
				httpContent = new StringContent(json, Encoding.UTF8, "application/x-www-form-urlencoded");
			}
			HttpResponseMessage response = await this.httpClient.PostAsync(route, httpContent);
			string responseString = await this.ValidateResponse(response);
			TResponse responseObject = JsonConvert.DeserializeObject<TResponse>(responseString);
			return responseObject;
		}

		private async Task<string> ValidateResponse(HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode)
			{
				string responseString = await response.Content.ReadAsStringAsync();
				if (responseString != null && responseString.StartsWith("{\"error\":"))
				{
					JObject jObject = JObject.Parse(responseString);
					string message = jObject["error"].ToObject<string>();
					throw new ServerApiException(message, HttpStatusCode.BadRequest);
				}
				return responseString;
			}
			string responseContent = await response.Content.ReadAsStringAsync();
			if (string.Equals(responseContent, "Block Not Found"))
			{
				throw new ServerApiException("Block Not Found", HttpStatusCode.NotFound);
			}
			throw new ServerApiException(response.ReasonPhrase + ": " + responseContent, response.StatusCode);
		}

		public void Dispose()
		{
			this.httpClient.Dispose();
		}
	}
}
