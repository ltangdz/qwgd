using System;
using System.Collections.Generic;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA44_ARRAY")]
	public class DATA44_ARRAY : IExtensible
	{
		private readonly List<DATA44> _items = new List<DATA44>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DATA44> items => _items;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
