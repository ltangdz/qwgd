using System;
using System.Collections.Generic;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA16_ARRAY")]
	public class DATA16_ARRAY : IExtensible
	{
		private readonly List<DATA16> _items = new List<DATA16>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DATA16> items => _items;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
