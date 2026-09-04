using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA34")]
	public class DATA34 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _title = "";

		private string _img = "";

		private string _link = "";

		private int _pish;

		private string _file = "";

		private int _type;

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

		[ProtoMember(3, IsRequired = false, Name = "title", DataFormat = DataFormat.Default)]
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

		[ProtoMember(4, IsRequired = false, Name = "img", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string img
		{
			get
			{
				return _img;
			}
			set
			{
				_img = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "link", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string link
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

		[ProtoMember(6, IsRequired = false, Name = "pish", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int pish
		{
			get
			{
				return _pish;
			}
			set
			{
				_pish = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "file", DataFormat = DataFormat.Default)]
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

		[ProtoMember(8, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
