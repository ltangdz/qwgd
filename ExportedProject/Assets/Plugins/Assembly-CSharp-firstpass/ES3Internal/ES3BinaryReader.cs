using System;
using System.ComponentModel;
using System.IO;
using ES3Types;

namespace ES3Internal
{
	public class ES3BinaryReader : ES3Reader
	{
		public BinaryReader baseReader;

		internal ES3BinaryReader(Stream stream, ES3Settings settings, bool readHeaderAndFooter = true)
			: base(settings, readHeaderAndFooter)
		{
			baseReader = new BinaryReader(stream, settings.encoding);
		}

		public override string ReadPropertyName()
		{
			string text = baseReader.ReadString();
			if (text == null)
			{
				throw new FormatException("Stream isn't positioned before a property.");
			}
			if (text == ".")
			{
				return null;
			}
			ES3Debug.Log("<b>" + text + "</b> (reading property)", null, serializationDepth);
			return text;
		}

		protected override Type ReadKeyPrefix(bool ignoreType = false)
		{
			Read7BitEncodedInt();
			StartReadObject();
			Type result = null;
			string text = ReadPropertyName();
			if (text == "__type")
			{
				Read7BitEncodedInt();
				baseReader.ReadByte();
				string typeName = baseReader.ReadString();
				result = (ignoreType ? null : Type.GetType(typeName));
				text = ReadPropertyName();
			}
			if (text != "value")
			{
				throw new FormatException("This data is not Easy Save Key Value data. Expected property name \"value\", found \"" + text + "\".");
			}
			return result;
		}

		protected override void ReadKeySuffix()
		{
			string text = baseReader.ReadString();
			if (text != ".")
			{
				throw new FormatException("This data is not Easy Save Key Value data. Expected terminator, found \"" + text + "\".");
			}
		}

		internal override bool StartReadObject()
		{
			baseReader.ReadByte();
			return base.StartReadObject();
		}

		internal override void EndReadObject()
		{
			base.EndReadObject();
		}

		internal override bool StartReadDictionary()
		{
			baseReader.ReadByte();
			return true;
		}

		internal override void EndReadDictionary()
		{
		}

		internal override bool StartReadDictionaryKey()
		{
			baseReader.ReadByte();
			return true;
		}

		internal override void EndReadDictionaryKey()
		{
		}

		internal override void StartReadDictionaryValue()
		{
		}

		internal override bool EndReadDictionaryValue()
		{
			return true;
		}

		internal override bool StartReadCollection()
		{
			baseReader.ReadByte();
			return true;
		}

		internal override void EndReadCollection()
		{
		}

		internal override bool StartReadCollectionItem()
		{
			return true;
		}

		internal override bool EndReadCollectionItem()
		{
			return true;
		}

		internal override byte[] ReadElement(bool skip = false)
		{
			using (BinaryWriter binaryWriter = (skip ? null : new BinaryWriter(new MemoryStream(settings.bufferSize))))
			{
				ReadElement(binaryWriter, skip);
				if (skip)
				{
					return null;
				}
				binaryWriter.Flush();
				return ((MemoryStream)binaryWriter.BaseStream).ToArray();
			}
		}

		private void ReadElement(BinaryWriter writer, bool skip = false)
		{
			if (!skip)
			{
				writer.Write(baseReader.ReadBytes(Read7BitEncodedInt()));
			}
			else
			{
				baseReader.ReadBytes(Read7BitEncodedInt());
			}
		}

		internal override long Read_ref()
		{
			if (ES3ReferenceMgrBase.Current == null)
			{
				throw new InvalidOperationException("An Easy Save 3 Manager is required to load references. To add one to your scene, exit playmode and go to Assets > Easy Save 3 > Add Manager to Scene");
			}
			Read7BitEncodedInt();
			baseReader.ReadByte();
			return long.Parse(baseReader.ReadString());
		}

		internal override string Read_string()
		{
			baseReader.ReadByte();
			return baseReader.ReadString();
		}

		internal override char Read_char()
		{
			baseReader.ReadByte();
			return baseReader.ReadChar();
		}

		internal override float Read_float()
		{
			baseReader.ReadByte();
			return baseReader.ReadSingle();
		}

		internal override int Read_int()
		{
			baseReader.ReadByte();
			return Read7BitEncodedInt();
		}

		internal override bool Read_bool()
		{
			baseReader.ReadByte();
			return baseReader.ReadBoolean();
		}

		internal override decimal Read_decimal()
		{
			baseReader.ReadByte();
			return baseReader.ReadDecimal();
		}

		internal override double Read_double()
		{
			baseReader.ReadByte();
			return baseReader.ReadDouble();
		}

		internal override long Read_long()
		{
			baseReader.ReadByte();
			return baseReader.ReadInt64();
		}

		internal override ulong Read_ulong()
		{
			baseReader.ReadByte();
			return baseReader.ReadUInt64();
		}

		internal override uint Read_uint()
		{
			baseReader.ReadByte();
			return baseReader.ReadUInt32();
		}

		internal override byte Read_byte()
		{
			baseReader.ReadByte();
			return baseReader.ReadByte();
		}

		internal override sbyte Read_sbyte()
		{
			baseReader.ReadByte();
			return baseReader.ReadSByte();
		}

		internal override short Read_short()
		{
			baseReader.ReadByte();
			return baseReader.ReadInt16();
		}

		internal override ushort Read_ushort()
		{
			baseReader.ReadByte();
			return baseReader.ReadUInt16();
		}

		internal override byte[] Read_byteArray()
		{
			baseReader.ReadByte();
			return baseReader.ReadBytes(baseReader.ReadInt32());
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override T Read<T>(ES3Type type)
		{
			Read7BitEncodedInt();
			return base.Read<T>(type);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ReadInto<T>(object obj, ES3Type type)
		{
			Read7BitEncodedInt();
			base.ReadInto<T>(obj, type);
		}

		private int Read7BitEncodedInt()
		{
			int num = 0;
			int num2 = 0;
			byte b;
			do
			{
				if (num2 == 35)
				{
					throw new FormatException("The int being read is not a 7-bit encoded int");
				}
				b = baseReader.ReadByte();
				num |= (b & 0x7F) << num2;
				num2 += 7;
			}
			while ((b & 0x80) != 0);
			return num;
		}

		public override void Dispose()
		{
			baseReader.Close();
		}
	}
}
