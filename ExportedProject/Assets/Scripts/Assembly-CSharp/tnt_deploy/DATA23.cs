using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA23")]
	public class DATA23 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _person = "";

		private string _personnikename = "";

		private int _sexuality;

		private string _personavatar = "";

		private string _screen = "";

		private int _TalkType;

		private int _value;

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

		[ProtoMember(3, IsRequired = false, Name = "person", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string person
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

		[ProtoMember(4, IsRequired = false, Name = "personnikename", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string personnikename
		{
			get
			{
				return _personnikename;
			}
			set
			{
				_personnikename = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "sexuality", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int sexuality
		{
			get
			{
				return _sexuality;
			}
			set
			{
				_sexuality = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "personavatar", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string personavatar
		{
			get
			{
				return _personavatar;
			}
			set
			{
				_personavatar = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "screen", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string screen
		{
			get
			{
				return _screen;
			}
			set
			{
				_screen = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "TalkType", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int TalkType
		{
			get
			{
				return _TalkType;
			}
			set
			{
				_TalkType = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
