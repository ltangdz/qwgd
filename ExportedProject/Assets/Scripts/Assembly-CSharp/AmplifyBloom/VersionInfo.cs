using System;
using UnityEngine;

namespace AmplifyBloom
{
	[Serializable]
	public class VersionInfo
	{
		public const byte Major = 1;

		public const byte Minor = 0;

		public const byte Release = 8;

		private static string StageSuffix = "_dev001";

		[SerializeField]
		private int m_major;

		[SerializeField]
		private int m_minor;

		[SerializeField]
		private int m_release;

		public int Number => m_major * 100 + m_minor * 10 + m_release;

		public static string StaticToString()
		{
			return $"{(byte)1}.{(byte)0}.{(byte)8}" + StageSuffix;
		}

		public override string ToString()
		{
			return $"{m_major}.{m_minor}.{m_release}" + StageSuffix;
		}

		private VersionInfo()
		{
			m_major = 1;
			m_minor = 0;
			m_release = 8;
		}

		private VersionInfo(byte major, byte minor, byte release)
		{
			m_major = major;
			m_minor = minor;
			m_release = release;
		}

		public static VersionInfo Current()
		{
			return new VersionInfo(1, 0, 8);
		}

		public static bool Matches(VersionInfo version)
		{
			if (1 == version.m_major && version.m_minor == 0)
			{
				return 8 == version.m_release;
			}
			return false;
		}
	}
}
