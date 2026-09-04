using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA1")]
	public class DATA1 : IExtensible
	{
		private uint _ID;

		private string _title = "";

		private int _eventid;

		private int _form;

		private int _link;

		private int _sign;

		private string _image = "";

		private string _message = "";

		private string _point = "";

		private string _name = "";

		private int _passwordnumber;

		private string _missionID = "";

		private string _percent = "";

		private string _passwordID = "";

		private int _fx;

		private string _role = "";

		private string _label = "";

		private string _aimspercent = "";

		private int _isshowreasoning;

		private int _changename;

		private string _sources = "";

		private string _track = "";

		private string _needotherid = "";

		private string _videoid = "";

		private string _newsid = "";

		private int _changeavatar;

		private int _dieavatar;

		private string _newemail = "";

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

		[ProtoMember(2, IsRequired = false, Name = "title", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "eventid", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "form", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int form
		{
			get
			{
				return _form;
			}
			set
			{
				_form = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "link", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int link
		{
			get
			{
				return _link;
			}
			set
			{
				_link = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "sign", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int sign
		{
			get
			{
				return _sign;
			}
			set
			{
				_sign = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "image", DataFormat = DataFormat.Default)]
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

		[ProtoMember(8, IsRequired = false, Name = "message", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "point", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string point
		{
			get
			{
				return _point;
			}
			set
			{
				_point = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "name", DataFormat = DataFormat.Default)]
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

		[ProtoMember(11, IsRequired = false, Name = "passwordnumber", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int passwordnumber
		{
			get
			{
				return _passwordnumber;
			}
			set
			{
				_passwordnumber = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "missionID", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string missionID
		{
			get
			{
				return _missionID;
			}
			set
			{
				_missionID = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "percent", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string percent
		{
			get
			{
				return _percent;
			}
			set
			{
				_percent = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "passwordID", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string passwordID
		{
			get
			{
				return _passwordID;
			}
			set
			{
				_passwordID = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "fx", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int fx
		{
			get
			{
				return _fx;
			}
			set
			{
				_fx = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "role", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string role
		{
			get
			{
				return _role;
			}
			set
			{
				_role = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "label", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string label
		{
			get
			{
				return _label;
			}
			set
			{
				_label = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "aimspercent", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string aimspercent
		{
			get
			{
				return _aimspercent;
			}
			set
			{
				_aimspercent = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "isshowreasoning", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int isshowreasoning
		{
			get
			{
				return _isshowreasoning;
			}
			set
			{
				_isshowreasoning = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "changename", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int changename
		{
			get
			{
				return _changename;
			}
			set
			{
				_changename = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "sources", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string sources
		{
			get
			{
				return _sources;
			}
			set
			{
				_sources = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "track", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string track
		{
			get
			{
				return _track;
			}
			set
			{
				_track = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "needotherid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string needotherid
		{
			get
			{
				return _needotherid;
			}
			set
			{
				_needotherid = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "videoid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string videoid
		{
			get
			{
				return _videoid;
			}
			set
			{
				_videoid = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "newsid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string newsid
		{
			get
			{
				return _newsid;
			}
			set
			{
				_newsid = value;
			}
		}

		[ProtoMember(26, IsRequired = false, Name = "changeavatar", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int changeavatar
		{
			get
			{
				return _changeavatar;
			}
			set
			{
				_changeavatar = value;
			}
		}

		[ProtoMember(27, IsRequired = false, Name = "dieavatar", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int dieavatar
		{
			get
			{
				return _dieavatar;
			}
			set
			{
				_dieavatar = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "newemail", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string newemail
		{
			get
			{
				return _newemail;
			}
			set
			{
				_newemail = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
