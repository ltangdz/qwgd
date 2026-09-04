using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA33")]
	public class DATA33 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _avatar = "";

		private string _name = "";

		private string _method = "";

		private string _condition = "";

		private string _sqlname = "";

		private string _logo = "";

		private string _ip = "";

		private string _port = "";

		private string _url = "";

		private string _file = "";

		private string _collect = "";

		private int _type;

		private string _imgselect = "";

		private string _mission = "";

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

		[ProtoMember(3, IsRequired = false, Name = "avatar", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string avatar
		{
			get
			{
				return _avatar;
			}
			set
			{
				_avatar = value;
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

		[ProtoMember(5, IsRequired = false, Name = "method", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string method
		{
			get
			{
				return _method;
			}
			set
			{
				_method = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "condition", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string condition
		{
			get
			{
				return _condition;
			}
			set
			{
				_condition = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "sqlname", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string sqlname
		{
			get
			{
				return _sqlname;
			}
			set
			{
				_sqlname = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "logo", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string logo
		{
			get
			{
				return _logo;
			}
			set
			{
				_logo = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "ip", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string ip
		{
			get
			{
				return _ip;
			}
			set
			{
				_ip = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "port", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string port
		{
			get
			{
				return _port;
			}
			set
			{
				_port = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "url", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string url
		{
			get
			{
				return _url;
			}
			set
			{
				_url = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "file", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string file
		{
			get
			{
				return _file;
			}
			set
			{
				_file = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "collect", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string collect
		{
			get
			{
				return _collect;
			}
			set
			{
				_collect = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int type
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

		[ProtoMember(15, IsRequired = false, Name = "imgselect", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string imgselect
		{
			get
			{
				return _imgselect;
			}
			set
			{
				_imgselect = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "mission", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string mission
		{
			get
			{
				return _mission;
			}
			set
			{
				_mission = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
