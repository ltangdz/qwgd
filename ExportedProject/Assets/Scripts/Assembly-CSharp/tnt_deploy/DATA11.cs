using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA11")]
	public class DATA11 : IExtensible
	{
		private uint _ID;

		private string _eventid = "";

		private string _eventno = "";

		private string _date = "";

		private string _passwords1 = "";

		private string _event_title = "";

		private string _event_date = "";

		private string _name = "";

		private int _method;

		private string _photo = "";

		private string _searchchat = "";

		private string _lyingchat = "";

		private string _tbnum = "";

		private string _camcondition = "";

		private string _playerids = "";

		private string _camdes = "";

		private string _fakename = "";

		private string _fakephoto = "";

		private string _newsid2 = "";

		private string _lastresult = "";

		private string _lastresultcontent = "";

		private int _number;

		private string _need_reason = "";

		private string _taskmail = "";

		private string _app = "";

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

		[ProtoMember(2, IsRequired = false, Name = "eventid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string eventid
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

		[ProtoMember(3, IsRequired = false, Name = "eventno", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string eventno
		{
			get
			{
				return _eventno;
			}
			set
			{
				_eventno = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "date", DataFormat = DataFormat.Default)]
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

		[ProtoMember(5, IsRequired = false, Name = "passwords1", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string passwords1
		{
			get
			{
				return _passwords1;
			}
			set
			{
				_passwords1 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "event_title", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string event_title
		{
			get
			{
				return _event_title;
			}
			set
			{
				_event_title = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "event_date", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string event_date
		{
			get
			{
				return _event_date;
			}
			set
			{
				_event_date = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "name", DataFormat = DataFormat.Default)]
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

		[ProtoMember(9, IsRequired = false, Name = "method", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int method
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

		[ProtoMember(10, IsRequired = false, Name = "photo", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string photo
		{
			get
			{
				return _photo;
			}
			set
			{
				_photo = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "searchchat", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string searchchat
		{
			get
			{
				return _searchchat;
			}
			set
			{
				_searchchat = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "lyingchat", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string lyingchat
		{
			get
			{
				return _lyingchat;
			}
			set
			{
				_lyingchat = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "tbnum", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tbnum
		{
			get
			{
				return _tbnum;
			}
			set
			{
				_tbnum = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "camcondition", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string camcondition
		{
			get
			{
				return _camcondition;
			}
			set
			{
				_camcondition = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "playerids", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string playerids
		{
			get
			{
				return _playerids;
			}
			set
			{
				_playerids = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "camdes", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string camdes
		{
			get
			{
				return _camdes;
			}
			set
			{
				_camdes = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "fakename", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string fakename
		{
			get
			{
				return _fakename;
			}
			set
			{
				_fakename = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "fakephoto", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string fakephoto
		{
			get
			{
				return _fakephoto;
			}
			set
			{
				_fakephoto = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "newsid2", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string newsid2
		{
			get
			{
				return _newsid2;
			}
			set
			{
				_newsid2 = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "lastresult", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string lastresult
		{
			get
			{
				return _lastresult;
			}
			set
			{
				_lastresult = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "lastresultcontent", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string lastresultcontent
		{
			get
			{
				return _lastresultcontent;
			}
			set
			{
				_lastresultcontent = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "number", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int number
		{
			get
			{
				return _number;
			}
			set
			{
				_number = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "need_reason", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string need_reason
		{
			get
			{
				return _need_reason;
			}
			set
			{
				_need_reason = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "taskmail", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string taskmail
		{
			get
			{
				return _taskmail;
			}
			set
			{
				_taskmail = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "app", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string app
		{
			get
			{
				return _app;
			}
			set
			{
				_app = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
