using System;
using System.Collections.Generic;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA20_ARRAY")]
	public class DATA20_ARRAY : IExtensible
	{
		private readonly List<DATA20> _items = new List<DATA20>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DATA20> items => _items;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
