using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA13")]
	public class DATA13 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _title = "";

		private string _picname = "";

		private string _arrowid = "";

		private string _arrowidhighlight = "";

		private string _newsTime = "";

		private string _website = "";

		private string _unlock = "";

		private int _newstype;

		private string _highlight = "";

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

		[ProtoMember(3, IsRequired = false, Name = "title", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "picname", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string picname
		{
			get
			{
				return _picname;
			}
			set
			{
				_picname = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "arrowid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string arrowid
		{
			get
			{
				return _arrowid;
			}
			set
			{
				_arrowid = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "arrowidhighlight", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string arrowidhighlight
		{
			get
			{
				return _arrowidhighlight;
			}
			set
			{
				_arrowidhighlight = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "newsTime", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string newsTime
		{
			get
			{
				return _newsTime;
			}
			set
			{
				_newsTime = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "website", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string website
		{
			get
			{
				return _website;
			}
			set
			{
				_website = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "unlock", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string unlock
		{
			get
			{
				return _unlock;
			}
			set
			{
				_unlock = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "newstype", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int newstype
		{
			get
			{
				return _newstype;
			}
			set
			{
				_newstype = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "highlight", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string highlight
		{
			get
			{
				return _highlight;
			}
			set
			{
				_highlight = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
