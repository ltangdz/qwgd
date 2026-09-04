using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA42")]
	public class DATA42 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _name = "";

		private string _files = "";

		private string _open = "";

		private string _type = "";

		private string _secure = "";

		private string _size = "";

		private string _time = "";

		private string _del = "";

		private string _searchfile = "";

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

		[ProtoMember(3, IsRequired = false, Name = "name", DataFormat = DataFormat.Default)]
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

		[ProtoMember(4, IsRequired = false, Name = "files", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string files
		{
			get
			{
				return _files;
			}
			set
			{
				_files = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "open", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string open
		{
			get
			{
				return _open;
			}
			set
			{
				_open = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "type", DataFormat = DataFormat.Default)]
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

		[ProtoMember(7, IsRequired = false, Name = "secure", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string secure
		{
			get
			{
				return _secure;
			}
			set
			{
				_secure = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "size", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string size
		{
			get
			{
				return _size;
			}
			set
			{
				_size = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "time", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string time
		{
			get
			{
				return _time;
			}
			set
			{
				_time = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "del", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string del
		{
			get
			{
				return _del;
			}
			set
			{
				_del = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "searchfile", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string searchfile
		{
			get
			{
				return _searchfile;
			}
			set
			{
				_searchfile = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
