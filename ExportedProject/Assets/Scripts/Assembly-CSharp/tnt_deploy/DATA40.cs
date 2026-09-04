using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA40")]
	public class DATA40 : IExtensible
	{
		private uint _id;

		private int _event;

		private string _name = "";

		private string _avatar = "";

		private string _option = "";

		private string _content = "";

		private string _voice = "";

		private uint _type;

		private string _extra = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public uint id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "event", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int @event
		{
			get
			{
				return _event;
			}
			set
			{
				_event = value;
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

		[ProtoMember(5, IsRequired = false, Name = "option", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string option
		{
			get
			{
				return _option;
			}
			set
			{
				_option = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "content", DataFormat = DataFormat.Default)]
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

		[ProtoMember(7, IsRequired = false, Name = "voice", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string voice
		{
			get
			{
				return _voice;
			}
			set
			{
				_voice = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0L)]
		public uint type
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

		[ProtoMember(9, IsRequired = false, Name = "extra", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string extra
		{
			get
			{
				return _extra;
			}
			set
			{
				_extra = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
