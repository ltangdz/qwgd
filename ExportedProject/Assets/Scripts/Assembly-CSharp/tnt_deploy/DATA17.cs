using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA17")]
	public class DATA17 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _nickname = "";

		private string _Replyname = "";

		private string _toothbook = "";

		private string _disscuss = "";

		private string _avatar = "";

		private string _date = "";

		private string _content = "";

		private string _contentid = "";

		private string _highlight = "";

		private string _discussid = "";

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

		[ProtoMember(4, IsRequired = false, Name = "Replyname", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string Replyname
		{
			get
			{
				return _Replyname;
			}
			set
			{
				_Replyname = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "toothbook", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string toothbook
		{
			get
			{
				return _toothbook;
			}
			set
			{
				_toothbook = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "disscuss", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string disscuss
		{
			get
			{
				return _disscuss;
			}
			set
			{
				_disscuss = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "avatar", DataFormat = DataFormat.Default)]
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

		[ProtoMember(9, IsRequired = false, Name = "content", DataFormat = DataFormat.Default)]
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

		[ProtoMember(10, IsRequired = false, Name = "contentid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string contentid
		{
			get
			{
				return _contentid;
			}
			set
			{
				_contentid = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "highlight", DataFormat = DataFormat.Default)]
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

		[ProtoMember(12, IsRequired = false, Name = "discussid", DataFormat = DataFormat.Default)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
