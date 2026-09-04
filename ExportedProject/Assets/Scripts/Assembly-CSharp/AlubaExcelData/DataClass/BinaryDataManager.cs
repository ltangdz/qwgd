using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using AlubaExcelData.Container;
using UnityEngine;

namespace AlubaExcelData.DataClass
{
	public class BinaryDataManager
	{
		public static string DATA_BINARY_PATH = Application.streamingAssetsPath + "/Binary/";

		private Dictionary<string, object> tableDic = new Dictionary<string, object>();

		private static string SAVE_PATH = Application.persistentDataPath + "/Data/";

		private static BinaryDataManager instance = new BinaryDataManager();

		public bool isDataLoaded;

		private int dataLoadedCount;

		private int dataShoudLoadCount;

		public static BinaryDataManager Instance => instance;

		private BinaryDataManager()
		{
		}

		public void InitData()
		{
			dataLoadedCount = 0;
			dataShoudLoadCount = 0;
			tableDic.Clear();
			LoadTable<PublicOpinionContainer, PublicOpinion>();
			LoadTable<PublicOpinionInitDataContainer, PublicOpinionInitData>();
			LoadTable<CityMapDataContainer, CityMapData>();
			LoadTable<TalkGroupContainer, TalkGroup>();
			LoadTable<TalkContentContainer, TalkContent>();
			LoadTable<DialogGroupContainer, DialogGroup>();
			LoadTable<DialogContentContainer, DialogContent>();
			LoadTable<VoiceLevelContainer, VoiceLevel>();
		}

		private IEnumerator LoadTableInAndroid<T, K>()
		{
			string url = "file://" + DATA_BINARY_PATH + typeof(K).Name + ".aluba";
			WWW www = new WWW(url);
			yield return www;
			while (!www.isDone)
			{
				yield return null;
			}
			LoadTableDetail<T, K>(www.bytes);
		}

		private void LoadTableDetail<T, K>(byte[] bytes)
		{
			int num = 0;
			int num2 = BitConverter.ToInt32(bytes, num);
			num += 4;
			int num3 = BitConverter.ToInt32(bytes, num);
			num += 4;
			string name = Encoding.UTF8.GetString(bytes, num, num3);
			num += num3;
			Type typeFromHandle = typeof(T);
			object obj = Activator.CreateInstance(typeFromHandle);
			Type typeFromHandle2 = typeof(K);
			FieldInfo[] fields = typeFromHandle2.GetFields();
			for (int i = 0; i < num2; i++)
			{
				object obj2 = Activator.CreateInstance(typeFromHandle2);
				int num4 = 0;
				FieldInfo[] array = fields;
				foreach (FieldInfo fieldInfo in array)
				{
					if (fieldInfo.FieldType == typeof(int))
					{
						fieldInfo.SetValue(obj2, BitConverter.ToInt32(bytes, num));
						num += 4;
					}
					else if (fieldInfo.FieldType == typeof(float))
					{
						fieldInfo.SetValue(obj2, BitConverter.ToSingle(bytes, num));
						num += 4;
					}
					else if (fieldInfo.FieldType == typeof(bool))
					{
						fieldInfo.SetValue(obj2, BitConverter.ToBoolean(bytes, num));
						num++;
					}
					else if (fieldInfo.FieldType == typeof(string))
					{
						int num5 = BitConverter.ToInt32(bytes, num);
						num += 4;
						fieldInfo.SetValue(obj2, Encoding.UTF8.GetString(bytes, num, num5));
						num += num5;
					}
					num4++;
				}
				object value = typeFromHandle.GetField("dataDic").GetValue(obj);
				MethodInfo method = value.GetType().GetMethod("Add");
				object value2 = typeFromHandle2.GetField(name).GetValue(obj2);
				method.Invoke(value, new object[2] { value2, obj2 });
			}
			tableDic.Add(typeof(T).Name, obj);
			dataLoadedCount++;
		}

		public void LoadTable<T, K>()
		{
			dataShoudLoadCount++;
			using (FileStream fileStream = File.Open(DATA_BINARY_PATH + typeof(K).Name + ".aluba", FileMode.Open, FileAccess.Read))
			{
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				LoadTableDetail<T, K>(array);
				fileStream.Close();
			}
		}

		public T GetTable<T>() where T : class
		{
			string name = typeof(T).Name;
			if (tableDic.ContainsKey(name))
			{
				return tableDic[name] as T;
			}
			return null;
		}

		public void Save(object obj, string fileName)
		{
			if (!Directory.Exists(SAVE_PATH))
			{
				Directory.CreateDirectory(SAVE_PATH);
			}
			using (FileStream fileStream = new FileStream(SAVE_PATH + fileName + ".aluba", FileMode.OpenOrCreate, FileAccess.Write))
			{
				new BinaryFormatter().Serialize(fileStream, obj);
				fileStream.Close();
			}
		}

		public T Load<T>(string fileName) where T : class
		{
			if (!File.Exists(SAVE_PATH + fileName + ".aluba"))
			{
				return null;
			}
			using (FileStream fileStream = File.Open(SAVE_PATH + fileName + ".aluba", FileMode.Open, FileAccess.Read))
			{
				T result = new BinaryFormatter().Deserialize(fileStream) as T;
				fileStream.Close();
				return result;
			}
		}
	}
}
