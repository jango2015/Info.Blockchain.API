using System;

namespace Info.Blockchain.API.BlockExplorer
{
	public struct BitcoinValue : IEquatable<BitcoinValue>
	{

		public const int SatoshisPerBitcoin = 100000000;
		public const int BitsPerBitcoin = 1000000;
		public const int MilliBitsPerBitcoin = 1000;

		private decimal btc { get; }
		public BitcoinValue(decimal btc)
		{
			this.btc = btc;
		}

		public decimal Btc => this.btc;

		public decimal MilliBits => this.btc * BitcoinValue.MilliBitsPerBitcoin;

		public decimal Bits => this.btc * BitcoinValue.BitsPerBitcoin;

		public long Satoshis => (long)(this.btc * BitcoinValue.SatoshisPerBitcoin);

		public static BitcoinValue Zero => new BitcoinValue();

		public static BitcoinValue FromSatoshis(long satoshis) => new BitcoinValue((decimal)satoshis / BitcoinValue.SatoshisPerBitcoin);

		public static BitcoinValue FromBits(decimal bits) => new BitcoinValue(bits / BitcoinValue.BitsPerBitcoin);

		public static BitcoinValue FromMilliBits(decimal mBtc) => new BitcoinValue(mBtc / BitcoinValue.MilliBitsPerBitcoin);

		public static BitcoinValue FromBtc(decimal btc) => new BitcoinValue(btc);

		public static BitcoinValue operator +(BitcoinValue x, BitcoinValue y)
		{
			decimal btc = x.Btc + y.Btc;
			return new BitcoinValue(btc);
		}

		public static BitcoinValue operator -(BitcoinValue x, BitcoinValue y)
		{
			decimal btc = x.Btc - y.Btc;
			return new BitcoinValue(btc);
		}

		public bool Equals(BitcoinValue other)
		{
			return this.Btc == other.Btc;
		}

		public override bool Equals(object obj)
		{
			if (obj is BitcoinValue)
			{
				return this.Equals((BitcoinValue) obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this.btc.GetHashCode();
		}

		public override string ToString() => this.Btc.ToString();

	}
}
