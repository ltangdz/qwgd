using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA37")]
	public class DATA37 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _name = "";

		private string _head = "";

		private string _phone = "";

		private string _reply = "";

		private string _condition = "";

		private string _failcondition = "";

		private string _empty = "";

		private string _video = "";

		private string _secondcall = "";

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

		[ProtoMember(4, IsRequired = false, Name = "head", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string head
		{
			get
			{
				return _head;
			}
			set
			{
				_head = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "phone", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string phone
		{
			get
			{
				return _phone;
			}
			set
			{
				_phone = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "reply", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string reply
		{
			get
			{
				return _reply;
			}
			set
			{
				_reply = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "condition", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string condition
		{
			get
			{
				return _condition;
			}
			set
			{
				_condition = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "failcondition", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string failcondition
		{
			get
			{
				return _failcondition;
			}
			set
			{
				_failcondition = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "empty", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string empty
		{
			get
			{
				return _empty;
			}
			set
			{
				_empty = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "video", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string video
		{
			get
			{
				return _video;
			}
			set
			{
				_video = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "secondcall", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string secondcall
		{
			get
			{
				return _secondcall;
			}
			set
			{
				_secondcall = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
