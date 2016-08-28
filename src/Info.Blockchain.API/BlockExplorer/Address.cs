using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Info.Blockchain.API.Json;
using Newtonsoft.Json;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Info.Blockchain.API.BlockExplorer
{
	/// <summary>
	/// Represents an address.
	/// </summary>
	public class Address
	{
		[JsonConstructor]
		// ReSharper disable once UnusedMember.Local
		private Address()
		{
		}

		/// <summary>
		/// Address object contructor to copy from another address and associate a list of transactions
		/// </summary>
		/// <param name="address">Address to copy all properties from except the transactions</param>
		/// <param name="transactions">Transaction list to associate to the address object</param>
		internal Address(Address address, List<Transaction> transactions)
		{
			if (address == null)
			{
				throw new ArgumentNullException(nameof(address));
			}
			this.Hash160 = address.Hash160;
			this.AddressStr = address.AddressStr;
			this.TotalReceived = address.TotalReceived;
			this.TotalSent = address.TotalSent;
			this.FinalBalance = address.FinalBalance;
			this.TransactionCount = address.TransactionCount;
			this.Transactions = new ReadOnlyCollection<Transaction>(transactions);
		}

		/// <summary>
		/// Hash160 representation of the address
		/// </summary>
		[JsonProperty("hash160", Required = Required.Always)]
		public string Hash160 { get; private set; }

		/// <summary>
		/// Base58Check representation of the address
		/// </summary>
		[JsonProperty("address", Required = Required.Always)]
		public string AddressStr { get; private set; }

		/// <summary>
		/// Total amount received
		/// </summary>
		[JsonProperty("total_received", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		public BitcoinValue TotalReceived { get; private set; }

		/// <summary>
		/// Total amount sent
		/// </summary>
		[JsonProperty("total_sent", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		public BitcoinValue TotalSent { get; private set; }

		/// <summary>
		/// Final balance of the address
		/// </summary>
		[JsonProperty("final_balance", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		public BitcoinValue FinalBalance { get; private set; }

		/// <summary>
		/// Total count of all transactions of this address
		/// </summary>
		[JsonProperty("n_tx", Required = Required.Always)]
		public long TransactionCount { get; private set; }

		/// <summary>
		/// List of transactions associated with this address
		/// </summary>
		[JsonProperty("txs", Required = Required.Always)]
		public ReadOnlyCollection<Transaction> Transactions { get; private set; }
	}
}
