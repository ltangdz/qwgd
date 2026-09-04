using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA3")]
	public class DATA3 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _describe = "";

		private string _name = "";

		private string _head = "";

		private string _reply = "";

		private string _log = "";

		private string _missionID = "";

		private int _record;

		private string _condition = "";

		private string _target = "";

		private string _targetAvatar = "";

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

		[ProtoMember(3, IsRequired = false, Name = "describe", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string describe
		{
			get
			{
				return _describe;
			}
			set
			{
				_describe = value;
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

		[ProtoMember(5, IsRequired = false, Name = "head", DataFormat = DataFormat.Default)]
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

		[ProtoMember(7, IsRequired = false, Name = "log", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string log
		{
			get
			{
				return _log;
			}
			set
			{
				_log = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "missionID", DataFormat = DataFormat.Default)]
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

		[ProtoMember(9, IsRequired = false, Name = "record", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int record
		{
			get
			{
				return _record;
			}
			set
			{
				_record = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "condition", DataFormat = DataFormat.Default)]
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

		[ProtoMember(11, IsRequired = false, Name = "target", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string target
		{
			get
			{
				return _target;
			}
			set
			{
				_target = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "targetAvatar", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string targetAvatar
		{
			get
			{
				return _targetAvatar;
			}
			set
			{
				_targetAvatar = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
