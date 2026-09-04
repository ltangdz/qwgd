using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA36")]
	public class DATA36 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _rolename = "";

		private string _itemids = "";

		private string _searchcontent = "";

		private string _getitemids = "";

		private string _postcode = "";

		private string _country = "";

		private string _city = "";

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

		[ProtoMember(3, IsRequired = false, Name = "rolename", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string rolename
		{
			get
			{
				return _rolename;
			}
			set
			{
				_rolename = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "itemids", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string itemids
		{
			get
			{
				return _itemids;
			}
			set
			{
				_itemids = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "searchcontent", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string searchcontent
		{
			get
			{
				return _searchcontent;
			}
			set
			{
				_searchcontent = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "getitemids", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string getitemids
		{
			get
			{
				return _getitemids;
			}
			set
			{
				_getitemids = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "postcode", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string postcode
		{
			get
			{
				return _postcode;
			}
			set
			{
				_postcode = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "country", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string country
		{
			get
			{
				return _country;
			}
			set
			{
				_country = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "city", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string city
		{
			get
			{
				return _city;
			}
			set
			{
				_city = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
