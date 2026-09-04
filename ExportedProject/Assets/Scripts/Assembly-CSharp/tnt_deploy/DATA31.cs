using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA31")]
	public class DATA31 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _photo = "";

		private int _fakephoto;

		private string _name = "";

		private string _sign = "";

		private string _signs = "";

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

		[ProtoMember(4, IsRequired = false, Name = "fakephoto", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int fakephoto
		{
			get
			{
				return _fakephoto;
			}
			set
			{
				_fakephoto = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "name", DataFormat = DataFormat.Default)]
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

		[ProtoMember(6, IsRequired = false, Name = "sign", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string sign
		{
			get
			{
				return _sign;
			}
			set
			{
				_sign = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "signs", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string signs
		{
			get
			{
				return _signs;
			}
			set
			{
				_signs = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
