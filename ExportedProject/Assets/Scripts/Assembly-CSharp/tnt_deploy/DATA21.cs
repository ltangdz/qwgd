using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA21")]
	public class DATA21 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private int _person;

		private string _title = "";

		private string _content = "";

		private string _imgTitle = "";

		private string _commentName = "";

		private string _commentInfo = "";

		private string _newsImg = "";

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

		[ProtoMember(3, IsRequired = false, Name = "person", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int person
		{
			get
			{
				return _person;
			}
			set
			{
				_person = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "title", DataFormat = DataFormat.Default)]
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

		[ProtoMember(6, IsRequired = false, Name = "imgTitle", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string imgTitle
		{
			get
			{
				return _imgTitle;
			}
			set
			{
				_imgTitle = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "commentName", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string commentName
		{
			get
			{
				return _commentName;
			}
			set
			{
				_commentName = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "commentInfo", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string commentInfo
		{
			get
			{
				return _commentInfo;
			}
			set
			{
				_commentInfo = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "newsImg", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string newsImg
		{
			get
			{
				return _newsImg;
			}
			set
			{
				_newsImg = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
