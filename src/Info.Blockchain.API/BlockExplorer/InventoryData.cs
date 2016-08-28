using System;
using Info.Blockchain.API.Json;
using Newtonsoft.Json;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Info.Blockchain.API.BlockExplorer
{
	/// <summary>
	/// This class contains data related to inventory messages 
	/// that Blockchain.info received for an object.
	/// </summary>
	public class InventoryData
	{
		[JsonConstructor]
		private InventoryData()
		{
		}

		/// <summary>
		/// Object hash
		/// </summary>
		[JsonProperty("hash", Required = Required.Always)]
		public string Hash { get; private set; }

		/// <summary>
		/// Object type
		/// </summary>
		[JsonProperty("type", Required = Required.Always)]
		public string Type { get; private set; }

		/// <summary>
		/// The time Blockchain.info first received an inventory message
		/// containing a hash for this transaction.
		/// </summary>
		[JsonProperty("initial_time", Required = Required.Always)]
		[JsonConverter(typeof(UnixDateTimeJsonConverter), true)]
		public DateTime InitialTime { get; private set; }

		/// <summary>
		/// The last time Blockchain.info received an inventory message 
		/// containing a hash for this transaction.
		/// </summary>
		[JsonProperty("last_time", Required = Required.Always)]
		[JsonConverter(typeof(UnixDateTimeJsonConverter), true)]
		public DateTime LastTime { get; private set; }

		/// <summary>
		/// IP of the peer from which Blockchain.info first received an inventory 
		/// message containing a hash for this transaction.
		/// </summary>
		[JsonProperty("initial_ip", Required = Required.Always)]
		public string InitialIp { get; private set; }

		/// <summary>
		/// Number of nodes that Blockchain.info is currently connected to.
		/// </summary>
		[JsonProperty("nconnected", Required = Required.Always)]
		public int NConnected { get; private set; }

		/// <summary>
		/// Number of nodes Blockchain.info received an inventory message containing 
		/// a hash for this transaction from.
		/// </summary>
		[JsonProperty("relayed_count", Required = Required.Always)]
		public int RelayedCount { get; private set; }

		/// <summary>
		/// Ratio of nodes that Blockchain.info received an inventory message
		/// containing a hash for this transaction from and the number of connected nodes.
		/// </summary>
		[JsonProperty("relayed_percent", Required = Required.Always)]
		public int RelayedPercent { get; private set; }
	}
}
