using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA43")]
	public class DATA43 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _type = "";

		private string _name = "";

		private float _newslv;

		private string _up = "";

		private string _down = "";

		private string _city = "";

		private string _info = "";

		private string _uprst = "";

		private string _downrst = "";

		private int _penzitype;

		private int _chufa;

		private string _danmu = "";

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

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "name", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "newslv", DataFormat = DataFormat.FixedSize)]
		[DefaultValue(0f)]
		public float newslv
		{
			get
			{
				return _newslv;
			}
			set
			{
				_newslv = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "up", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string up
		{
			get
			{
				return _up;
			}
			set
			{
				_up = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "down", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string down
		{
			get
			{
				return _down;
			}
			set
			{
				_down = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "city", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string city
		{
			get
			{
				return _city;
			}
			set
			{
				_city = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "info", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string info
		{
			get
			{
				return _info;
			}
			set
			{
				_info = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "uprst", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string uprst
		{
			get
			{
				return _uprst;
			}
			set
			{
				_uprst = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "downrst", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string downrst
		{
			get
			{
				return _downrst;
			}
			set
			{
				_downrst = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "penzitype", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int penzitype
		{
			get
			{
				return _penzitype;
			}
			set
			{
				_penzitype = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "chufa", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int chufa
		{
			get
			{
				return _chufa;
			}
			set
			{
				_chufa = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "danmu", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string danmu
		{
			get
			{
				return _danmu;
			}
			set
			{
				_danmu = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
