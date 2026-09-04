using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA27")]
	public class DATA27 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _tieimg = "";

		private string _tietitle = "";

		private string _tiesender = "";

		private int _tiemsg;

		private string _tiemonth = "";

		private string _tieday = "";

		private string _tieinfo = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public uint ID
		{
			get
			{
				return _ID;
			}
			set
			{
				_ID = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "eventid", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int eventid
		{
			get
			{
				return _eventid;
			}
			set
			{
				_eventid = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "tieimg", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tieimg
		{
			get
			{
				return _tieimg;
			}
			set
			{
				_tieimg = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "tietitle", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tietitle
		{
			get
			{
				return _tietitle;
			}
			set
			{
				_tietitle = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "tiesender", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tiesender
		{
			get
			{
				return _tiesender;
			}
			set
			{
				_tiesender = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "tiemsg", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int tiemsg
		{
			get
			{
				return _tiemsg;
			}
			set
			{
				_tiemsg = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "tiemonth", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tiemonth
		{
			get
			{
				return _tiemonth;
			}
			set
			{
				_tiemonth = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "tieday", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tieday
		{
			get
			{
				return _tieday;
			}
			set
			{
				_tieday = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "tieinfo", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tieinfo
		{
			get
			{
				return _tieinfo;
			}
			set
			{
				_tieinfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
