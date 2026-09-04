using System;
using System.Collections.Generic;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA14_ARRAY")]
	public class DATA14_ARRAY : IExtensible
	{
		private readonly List<DATA14> _items = new List<DATA14>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DATA14> items => _items;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
