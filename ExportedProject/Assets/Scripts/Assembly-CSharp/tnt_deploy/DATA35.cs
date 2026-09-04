using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA35")]
	public class DATA35 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private int _filetype;

		private string _fileinfo = "";

		private string _icon = "";

		private string _avatar = "";

		private string _phone = "";

		private string _time = "";

		private string _filename = "";

		private string _highlight = "";

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

		[ProtoMember(3, IsRequired = false, Name = "filetype", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int filetype
		{
			get
			{
				return _filetype;
			}
			set
			{
				_filetype = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "fileinfo", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string fileinfo
		{
			get
			{
				return _fileinfo;
			}
			set
			{
				_fileinfo = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "icon", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string icon
		{
			get
			{
				return _icon;
			}
			set
			{
				_icon = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "avatar", DataFormat = DataFormat.Default)]
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

		[ProtoMember(7, IsRequired = false, Name = "phone", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string phone
		{
			get
			{
				return _phone;
			}
			set
			{
				_phone = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "time", DataFormat = DataFormat.Default)]
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

		[ProtoMember(9, IsRequired = false, Name = "filename", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string filename
		{
			get
			{
				return _filename;
			}
			set
			{
				_filename = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "highlight", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string highlight
		{
			get
			{
				return _highlight;
			}
			set
			{
				_highlight = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
