using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA14")]
	public class DATA14 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private int _type;

		private string _user = "";

		private string _clueID = "";

		private string _password = "";

		private string _passworditemid = "";

		private string _nickname = "";

		private string _nick_clueID = "";

		private string _avatar = "";

		private string _sign = "";

		private string _hobby = "";

		private string _profession = "";

		private string _birth = "";

		private string _address = "";

		private string _addressID = "";

		private string _missionID = "";

		private string _discussid = "";

		private string _inbox = "";

		private string _email = "";

		private string _findpassword = "";

		private string _related_nickname = "";

		private string _related_avatar = "";

		private string _related_introduce = "";

		private string _likes = "";

		private string _like = "";

		private string _playerid = "";

		private int _lock;

		private string _newsid = "";

		private uint _data2ID;

		private int _logoff;

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

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "user", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string user
		{
			get
			{
				return _user;
			}
			set
			{
				_user = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "clueID", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string clueID
		{
			get
			{
				return _clueID;
			}
			set
			{
				_clueID = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "password", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "passworditemid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string passworditemid
		{
			get
			{
				return _passworditemid;
			}
			set
			{
				_passworditemid = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "nickname", DataFormat = DataFormat.Default)]
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

		[ProtoMember(9, IsRequired = false, Name = "nick_clueID", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string nick_clueID
		{
			get
			{
				return _nick_clueID;
			}
			set
			{
				_nick_clueID = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "avatar", DataFormat = DataFormat.Default)]
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

		[ProtoMember(11, IsRequired = false, Name = "sign", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string sign
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

		[ProtoMember(12, IsRequired = false, Name = "hobby", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string hobby
		{
			get
			{
				return _hobby;
			}
			set
			{
				_hobby = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "profession", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string profession
		{
			get
			{
				return _profession;
			}
			set
			{
				_profession = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "birth", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string birth
		{
			get
			{
				return _birth;
			}
			set
			{
				_birth = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "address", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string address
		{
			get
			{
				return _address;
			}
			set
			{
				_address = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "addressID", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string addressID
		{
			get
			{
				return _addressID;
			}
			set
			{
				_addressID = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "missionID", DataFormat = DataFormat.Default)]
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

		[ProtoMember(18, IsRequired = false, Name = "discussid", DataFormat = DataFormat.Default)]
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

		[ProtoMember(19, IsRequired = false, Name = "inbox", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string inbox
		{
			get
			{
				return _inbox;
			}
			set
			{
				_inbox = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "email", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string email
		{
			get
			{
				return _email;
			}
			set
			{
				_email = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "findpassword", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string findpassword
		{
			get
			{
				return _findpassword;
			}
			set
			{
				_findpassword = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "related_nickname", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string related_nickname
		{
			get
			{
				return _related_nickname;
			}
			set
			{
				_related_nickname = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "related_avatar", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string related_avatar
		{
			get
			{
				return _related_avatar;
			}
			set
			{
				_related_avatar = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "related_introduce", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string related_introduce
		{
			get
			{
				return _related_introduce;
			}
			set
			{
				_related_introduce = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "likes", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string likes
		{
			get
			{
				return _likes;
			}
			set
			{
				_likes = value;
			}
		}

		[ProtoMember(26, IsRequired = false, Name = "like", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string like
		{
			get
			{
				return _like;
			}
			set
			{
				_like = value;
			}
		}

		[ProtoMember(27, IsRequired = false, Name = "playerid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string playerid
		{
			get
			{
				return _playerid;
			}
			set
			{
				_playerid = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "lock", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int @lock
		{
			get
			{
				return _lock;
			}
			set
			{
				_lock = value;
			}
		}

		[ProtoMember(29, IsRequired = false, Name = "newsid", DataFormat = DataFormat.Default)]
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

		[ProtoMember(30, IsRequired = false, Name = "data2ID", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0L)]
		public uint data2ID
		{
			get
			{
				return _data2ID;
			}
			set
			{
				_data2ID = value;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "logoff", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int logoff
		{
			get
			{
				return _logoff;
			}
			set
			{
				_logoff = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
