using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA29")]
	public class DATA29 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _name = "";

		private string _comment = "";

		private string _bakcomment = "";

		private string _time = "";

		private string _tieuserinfo = "";

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

		[ProtoMember(4, IsRequired = false, Name = "comment", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string comment
		{
			get
			{
				return _comment;
			}
			set
			{
				_comment = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "bakcomment", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string bakcomment
		{
			get
			{
				return _bakcomment;
			}
			set
			{
				_bakcomment = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "time", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string time
		{
			get
			{
				return _time;
			}
			set
			{
				_time = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "tieuserinfo", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tieuserinfo
		{
			get
			{
				return _tieuserinfo;
			}
			set
			{
				_tieuserinfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
