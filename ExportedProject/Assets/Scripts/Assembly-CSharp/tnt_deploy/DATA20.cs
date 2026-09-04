using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA20")]
	public class DATA20 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private int _pos;

		private string _period = "";

		private int _periodtype;

		private string _title = "";

		private string _beizhu1 = "";

		private string _content = "";

		private string _percent = "";

		private int _last;

		private int _completion;

		private string _renwu = "";

		private string _info = "";

		private string _highlight = "";

		private string _clue = "";

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

		[ProtoMember(3, IsRequired = false, Name = "pos", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int pos
		{
			get
			{
				return _pos;
			}
			set
			{
				_pos = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "period", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string period
		{
			get
			{
				return _period;
			}
			set
			{
				_period = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "periodtype", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int periodtype
		{
			get
			{
				return _periodtype;
			}
			set
			{
				_periodtype = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "title", DataFormat = DataFormat.Default)]
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

		[ProtoMember(7, IsRequired = false, Name = "beizhu1", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string beizhu1
		{
			get
			{
				return _beizhu1;
			}
			set
			{
				_beizhu1 = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "content", DataFormat = DataFormat.Default)]
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

		[ProtoMember(9, IsRequired = false, Name = "percent", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string percent
		{
			get
			{
				return _percent;
			}
			set
			{
				_percent = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "last", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int last
		{
			get
			{
				return _last;
			}
			set
			{
				_last = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "completion", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int completion
		{
			get
			{
				return _completion;
			}
			set
			{
				_completion = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "renwu", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string renwu
		{
			get
			{
				return _renwu;
			}
			set
			{
				_renwu = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "info", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string info
		{
			get
			{
				return _info;
			}
			set
			{
				_info = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "highlight", DataFormat = DataFormat.Default)]
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

		[ProtoMember(15, IsRequired = false, Name = "clue", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string clue
		{
			get
			{
				return _clue;
			}
			set
			{
				_clue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
