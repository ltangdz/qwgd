using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA44")]
	public class DATA44 : IExtensible
	{
		private int _id;

		private string _event = "";

		private string _name0 = "";

		private string _idnumber0 = "";

		private string _email0 = "";

		private string _gender = "";

		private string _birth = "";

		private string _birth_en = "";

		private string _position = "";

		private string _addnum = "";

		private string _address = "";

		private string _hitalkid = "";

		private string _tel = "";

		private string _itemid = "";

		private string _idlist = "";

		private string _marriage = "";

		private string _fingerPrint = "";

		private string _crime = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
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

		[ProtoMember(2, IsRequired = false, Name = "event", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string @event
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

		[ProtoMember(3, IsRequired = false, Name = "name0", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string name0
		{
			get
			{
				return _name0;
			}
			set
			{
				_name0 = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "idnumber0", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string idnumber0
		{
			get
			{
				return _idnumber0;
			}
			set
			{
				_idnumber0 = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "email0", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string email0
		{
			get
			{
				return _email0;
			}
			set
			{
				_email0 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "gender", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string gender
		{
			get
			{
				return _gender;
			}
			set
			{
				_gender = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "birth", DataFormat = DataFormat.Default)]
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

		[ProtoMember(8, IsRequired = false, Name = "birth_en", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string birth_en
		{
			get
			{
				return _birth_en;
			}
			set
			{
				_birth_en = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "position", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "addnum", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string addnum
		{
			get
			{
				return _addnum;
			}
			set
			{
				_addnum = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "address", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string address
		{
			get
			{
				return _address;
			}
			set
			{
				_address = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "hitalkid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string hitalkid
		{
			get
			{
				return _hitalkid;
			}
			set
			{
				_hitalkid = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "tel", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tel
		{
			get
			{
				return _tel;
			}
			set
			{
				_tel = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "itemid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string itemid
		{
			get
			{
				return _itemid;
			}
			set
			{
				_itemid = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "idlist", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string idlist
		{
			get
			{
				return _idlist;
			}
			set
			{
				_idlist = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "marriage", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string marriage
		{
			get
			{
				return _marriage;
			}
			set
			{
				_marriage = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "fingerPrint", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string fingerPrint
		{
			get
			{
				return _fingerPrint;
			}
			set
			{
				_fingerPrint = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "crime", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string crime
		{
			get
			{
				return _crime;
			}
			set
			{
				_crime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
