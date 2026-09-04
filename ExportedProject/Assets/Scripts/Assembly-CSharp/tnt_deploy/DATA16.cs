using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA16")]
	public class DATA16 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _nickname = "";

		private string _avatar = "";

		private string _content = "";

		private string _contenthighlight = "";

		private string _highlight = "";

		private string _date = "";

		private string _datehighlight = "";

		private int _imagetype;

		private string _image = "";

		private int _imagehighlight;

		private int _isimagelock;

		private string _hotcount = "";

		private string _discusscount = "";

		private string _discussid = "";

		private string _read = "";

		private string _location = "";

		private string _locationhigh = "";

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

		[ProtoMember(3, IsRequired = false, Name = "nickname", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string nickname
		{
			get
			{
				return _nickname;
			}
			set
			{
				_nickname = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "avatar", DataFormat = DataFormat.Default)]
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

		[ProtoMember(5, IsRequired = false, Name = "content", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string content
		{
			get
			{
				return _content;
			}
			set
			{
				_content = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "contenthighlight", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string contenthighlight
		{
			get
			{
				return _contenthighlight;
			}
			set
			{
				_contenthighlight = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "highlight", DataFormat = DataFormat.Default)]
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

		[ProtoMember(8, IsRequired = false, Name = "date", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "datehighlight", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string datehighlight
		{
			get
			{
				return _datehighlight;
			}
			set
			{
				_datehighlight = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "imagetype", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int imagetype
		{
			get
			{
				return _imagetype;
			}
			set
			{
				_imagetype = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "image", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string image
		{
			get
			{
				return _image;
			}
			set
			{
				_image = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "imagehighlight", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int imagehighlight
		{
			get
			{
				return _imagehighlight;
			}
			set
			{
				_imagehighlight = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "isimagelock", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int isimagelock
		{
			get
			{
				return _isimagelock;
			}
			set
			{
				_isimagelock = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "hotcount", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string hotcount
		{
			get
			{
				return _hotcount;
			}
			set
			{
				_hotcount = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "discusscount", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string discusscount
		{
			get
			{
				return _discusscount;
			}
			set
			{
				_discusscount = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "discussid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string discussid
		{
			get
			{
				return _discussid;
			}
			set
			{
				_discussid = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "read", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string read
		{
			get
			{
				return _read;
			}
			set
			{
				_read = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "location", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "locationhigh", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string locationhigh
		{
			get
			{
				return _locationhigh;
			}
			set
			{
				_locationhigh = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
