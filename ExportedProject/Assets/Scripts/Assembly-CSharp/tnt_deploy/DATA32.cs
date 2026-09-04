using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA32")]
	public class DATA32 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private int _optional;

		private string _information = "";

		private string _contentID = "";

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

		[ProtoMember(3, IsRequired = false, Name = "optional", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int optional
		{
			get
			{
				return _optional;
			}
			set
			{
				_optional = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "information", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string information
		{
			get
			{
				return _information;
			}
			set
			{
				_information = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "contentID", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string contentID
		{
			get
			{
				return _contentID;
			}
			set
			{
				_contentID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
