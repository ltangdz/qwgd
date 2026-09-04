using System;
using System.Globalization;

namespace Aluba
{
	public class AlubaUtils
	{
		public static long TimeStampSeconds()
		{
			return Convert.ToInt64((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
		}

		public static long TimeStampMilliseconds()
		{
			return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000000;
		}

		public static double StringParseToDouble(string str)
		{
			return double.Parse(str, CultureInfo.InvariantCulture);
		}

		public static float StringParseToFloat(string str)
		{
			return float.Parse(str, CultureInfo.InvariantCulture);
		}

		public static decimal StringParseToDecimal(string str)
		{
			return decimal.Parse(str, CultureInfo.InvariantCulture);
		}

		public static int StringParseToInt(string str)
		{
			return int.Parse(str);
		}

		public static long StringParseToLong(string str)
		{
			return long.Parse(str);
		}

		public static ulong StringParseToULong(string str)
		{
			return ulong.Parse(str);
		}

		public static bool StringParseToBool(string str)
		{
			return bool.Parse(str);
		}
	}
}
