using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Info.Blockchain.API.Abstractions
{
	public interface IHttpClient : IDisposable
	{
		string ApiCode { get; set; }
		Task<T> GetAsync<T>(string route, QueryString queryString = null, Func<string, T> customDeserialization = null);
		Task<TResponse> PostAsync<TPost, TResponse>(string route, TPost postObject, Func<string, TResponse> customDeserialization = null, bool multiPartContent = false);
	}

	public class QueryString
	{
		private Dictionary<string, string> queryString { get; } = new Dictionary<string, string>();

		public void Add(string key, string value)
		{
			if (this.queryString.ContainsKey(key))
			{
				throw new ClientApiException($"Query string already has a value for {key}");
			}
			this.queryString[key] = value;
		}

		public int Count => this.queryString.Count;


		public void AddOrUpdate(string key, string value)
		{
			this.queryString[key] = value;
		}

		public override string ToString()
		{
			IEnumerable<string> queryStringList = this.queryString.Select(kv => $"{kv.Key}={kv.Value}");
			return "?" + string.Join("&", queryStringList);
		}
	}
}
