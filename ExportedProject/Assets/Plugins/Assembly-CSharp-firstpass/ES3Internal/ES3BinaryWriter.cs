using System;
using System.ComponentModel;
using System.IO;
using ES3Types;
using UnityEngine;

namespace ES3Internal
{
	internal class ES3BinaryWriter : ES3Writer
	{
		internal BinaryWriter baseWriter;

		public ES3BinaryWriter(Stream stream, ES3Settings settings)
			: this(stream, settings, writeHeaderAndFooter: true, mergeKeys: true)
		{
		}

		internal ES3BinaryWriter(Stream stream, ES3Settings settings, bool writeHeaderAndFooter, bool mergeKeys)
			: base(settings, writeHeaderAndFooter, mergeKeys)
		{
			baseWriter = new BinaryWriter(stream, settings.encoding);
			StartWriteFile();
		}

		internal override void Write(string key, Type type, byte[] value)
		{
			StartWriteProperty(key);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ES3Writer eS3Writer = ES3Writer.Create(memoryStream, new ES3Settings(ES3.EncryptionType.None, ES3.CompressionType.None, ES3.Format.Binary_Alpha), writeHeaderAndFooter: false, overwriteKeys: false))
				{
					eS3Writer.StartWriteObject(key);
					eS3Writer.WriteType(type);
					eS3Writer.WriteRawProperty("value", value);
					eS3Writer.EndWriteObject(key);
				}
				byte[] array = memoryStream.ToArray();
				Write7BitEncodedInt(array.Length);
				baseWriter.Write(array);
			}
			EndWriteProperty(key);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Write(Type type, string key, object value)
		{
			StartWriteProperty(key);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ES3Writer eS3Writer = ES3Writer.Create(memoryStream, new ES3Settings(ES3.EncryptionType.None, ES3.CompressionType.None, ES3.Format.Binary_Alpha), writeHeaderAndFooter: false, overwriteKeys: false))
				{
					eS3Writer.StartWriteObject(key);
					eS3Writer.WriteType(type);
					eS3Writer.WriteProperty("value", value, ES3TypeMgr.GetOrCreateES3Type(type), settings.referenceMode);
					eS3Writer.EndWriteObject(key);
				}
				byte[] array = memoryStream.ToArray();
				Write7BitEncodedInt(array.Length);
				baseWriter.Write(array);
			}
			EndWriteProperty(key);
			MarkKeyForDeletion(key);
		}

		public override void WriteProperty(string name, object value, ES3.ReferenceMode memberReferenceMode)
		{
			if (SerializationDepthLimitExceeded())
			{
				return;
			}
			StartWriteProperty(name);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ES3Writer eS3Writer = ES3Writer.Create(memoryStream, new ES3Settings(ES3.EncryptionType.None, ES3.CompressionType.None, ES3.Format.Binary_Alpha), writeHeaderAndFooter: false, overwriteKeys: false))
				{
					eS3Writer.Write(value, memberReferenceMode);
				}
				byte[] array = memoryStream.ToArray();
				Write7BitEncodedInt(array.Length);
				baseWriter.Write(array);
			}
			EndWriteProperty(name);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void WriteProperty(string name, object value, ES3Type type, ES3.ReferenceMode memberReferenceMode)
		{
			if (SerializationDepthLimitExceeded())
			{
				return;
			}
			StartWriteProperty(name);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ES3Writer eS3Writer = ES3Writer.Create(memoryStream, new ES3Settings(ES3.EncryptionType.None, ES3.CompressionType.None, ES3.Format.Binary_Alpha), writeHeaderAndFooter: false, overwriteKeys: false))
				{
					eS3Writer.Write(value, type, memberReferenceMode);
				}
				byte[] array = memoryStream.ToArray();
				Write7BitEncodedInt(array.Length);
				baseWriter.Write(array);
			}
			EndWriteProperty(name);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void WritePropertyByRef(string name, UnityEngine.Object value)
		{
			if (SerializationDepthLimitExceeded())
			{
				return;
			}
			StartWriteProperty(name);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ES3Writer eS3Writer = ES3Writer.Create(memoryStream, new ES3Settings(ES3.EncryptionType.None, ES3.CompressionType.None, ES3.Format.Binary_Alpha), writeHeaderAndFooter: false, overwriteKeys: false))
				{
					if (value == null)
					{
						WriteNull();
						return;
					}
					eS3Writer.StartWriteObject(name);
					eS3Writer.WriteRef(value);
					eS3Writer.EndWriteObject(name);
				}
				byte[] array = memoryStream.ToArray();
				Write7BitEncodedInt(array.Length);
				baseWriter.Write(array);
			}
			EndWriteProperty(name);
		}

		internal override void WritePrimitive(int value)
		{
			baseWriter.Write((byte)8);
			Write7BitEncodedInt(value);
		}

		internal override void WritePrimitive(float value)
		{
			baseWriter.Write((byte)7);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(bool value)
		{
			baseWriter.Write((byte)1);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(decimal value)
		{
			baseWriter.Write((byte)5);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(double value)
		{
			baseWriter.Write((byte)6);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(long value)
		{
			baseWriter.Write((byte)10);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(ulong value)
		{
			baseWriter.Write((byte)11);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(uint value)
		{
			baseWriter.Write((byte)9);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(byte value)
		{
			baseWriter.Write((byte)2);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(sbyte value)
		{
			baseWriter.Write((byte)3);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(short value)
		{
			baseWriter.Write((byte)12);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(ushort value)
		{
			baseWriter.Write((byte)13);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(char value)
		{
			baseWriter.Write((byte)4);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(byte[] value)
		{
			baseWriter.Write((byte)15);
			baseWriter.Write(value.Length);
			baseWriter.Write(value);
		}

		internal override void WritePrimitive(string value)
		{
			baseWriter.Write((byte)14);
			baseWriter.Write(value);
		}

		private void Write7BitEncodedInt(int value)
		{
			uint num;
			for (num = (uint)value; num >= 128; num >>= 7)
			{
				baseWriter.Write((byte)(num | 0x80));
			}
			baseWriter.Write((byte)num);
		}

		internal override void WriteNull()
		{
			baseWriter.Write((byte)0);
		}

		internal override void WriteRawProperty(string name, byte[] value)
		{
			StartWriteProperty(name);
			Write7BitEncodedInt(value.Length);
			baseWriter.Write(value);
			EndWriteProperty(name);
		}

		internal override void StartWriteFile()
		{
			base.StartWriteFile();
		}

		internal override void EndWriteFile()
		{
			baseWriter.Write(".");
			base.EndWriteFile();
		}

		internal override void StartWriteProperty(string name)
		{
			base.StartWriteProperty(name);
			baseWriter.Write(name);
		}

		internal override void EndWriteProperty(string name)
		{
			base.EndWriteProperty(name);
		}

		internal override void StartWriteObject(string name)
		{
			base.StartWriteObject(name);
			baseWriter.Write((byte)254);
		}

		internal override void EndWriteObject(string name)
		{
			baseWriter.Write(".");
			base.EndWriteObject(name);
		}

		internal override void StartWriteCollection()
		{
			base.StartWriteCollection();
			baseWriter.Write((byte)128);
		}

		internal override void EndWriteCollection()
		{
			baseWriter.Write(byte.MaxValue);
			base.EndWriteCollection();
		}

		internal override void StartWriteCollectionItem(int index)
		{
		}

		internal override void EndWriteCollectionItem(int index)
		{
			baseWriter.Write((byte)130);
		}

		internal override void StartWriteDictionary()
		{
			StartWriteObject(null);
			baseWriter.Write((byte)129);
		}

		internal override void EndWriteDictionary()
		{
			baseWriter.Write(byte.MaxValue);
			EndWriteObject(null);
		}

		internal override void StartWriteDictionaryKey(int index)
		{
		}

		internal override void EndWriteDictionaryKey(int index)
		{
		}

		internal override void StartWriteDictionaryValue(int index)
		{
		}

		internal override void EndWriteDictionaryValue(int index)
		{
		}

		public override void Dispose()
		{
			baseWriter.Close();
		}
	}
}
