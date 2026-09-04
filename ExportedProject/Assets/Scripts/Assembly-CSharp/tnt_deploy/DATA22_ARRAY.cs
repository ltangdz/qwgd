using System;
using System.Collections.Generic;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA22_ARRAY")]
	public class DATA22_ARRAY : IExtensible
	{
		private readonly List<DATA22> _items = new List<DATA22>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DATA22> items => _items;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
