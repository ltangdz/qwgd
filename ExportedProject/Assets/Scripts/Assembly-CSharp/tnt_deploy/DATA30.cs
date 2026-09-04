using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA30")]
	public class DATA30 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _photo = "";

		private string _name = "";

		private string _birth = "";

		private string _star = "";

		private string _intro = "";

		private string _tiehis = "";

		private string _tiebak = "";

		private string _follow = "";

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

		[ProtoMember(3, IsRequired = false, Name = "photo", DataFormat = DataFormat.Default)]
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

		[ProtoMember(5, IsRequired = false, Name = "birth", DataFormat = DataFormat.Default)]
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

		[ProtoMember(6, IsRequired = false, Name = "star", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string star
		{
			get
			{
				return _star;
			}
			set
			{
				_star = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "intro", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string intro
		{
			get
			{
				return _intro;
			}
			set
			{
				_intro = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "tiehis", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tiehis
		{
			get
			{
				return _tiehis;
			}
			set
			{
				_tiehis = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "tiebak", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tiebak
		{
			get
			{
				return _tiebak;
			}
			set
			{
				_tiebak = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "follow", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string follow
		{
			get
			{
				return _follow;
			}
			set
			{
				_follow = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
