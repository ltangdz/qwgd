using System.Collections.Generic;
using System.IO;
using Honeti;
using ProtoBuf;
using UnityEngine;
using tnt_deploy;

public class DataManager : MonoBehaviour
{
	private DATA0_ARRAY data0;

	private DATA1_ARRAY data1;

	private DATA2_ARRAY data2;

	private DATA3_ARRAY data3;

	private DATA11_ARRAY data11;

	private DATA13_ARRAY data13;

	private DATA14_ARRAY data14;

	private DATA15_ARRAY data15;

	private DATA16_ARRAY data16;

	private DATA17_ARRAY data17;

	private DATA20_ARRAY data20;

	private DATA21_ARRAY data21;

	private DATA22_ARRAY data22;

	private DATA23_ARRAY data23;

	private DATA24_ARRAY data24;

	private DATA31_ARRAY data31;

	private DATA33_ARRAY data33;

	private DATA34_ARRAY data34;

	private DATA35_ARRAY data35;

	private DATA36_ARRAY data36;

	private DATA37_ARRAY data37;

	private DATA38_ARRAY data38;

	private DATA39_ARRAY data39;

	private DATA40_ARRAY data40;

	private DATA41_ARRAY data41;

	private DATA42_ARRAY data42;

	private DATA43_ARRAY data43;

	private DATA44_ARRAY data44;

	public Dictionary<string, DATA0> dic0;

	public Dictionary<string, DATA0> dic0EventID;

	public Dictionary<string, DATA1> dic1;

	public Dictionary<string, DATA2> dic2;

	public Dictionary<string, DATA3> dic3;

	public Dictionary<string, DATA11> dic11;

	public Dictionary<string, DATA13> dic13;

	public Dictionary<string, DATA14> dic14;

	public Dictionary<string, DATA14> dic14_userid;

	public Dictionary<string, DATA15> dic15;

	public Dictionary<string, DATA16> dic16;

	public Dictionary<string, DATA17> dic17;

	public Dictionary<string, DATA20> dic20;

	public Dictionary<string, DATA21> dic21;

	public Dictionary<string, DATA22> dic22;

	public Dictionary<string, DATA23> dic23;

	public Dictionary<string, DATA24> dic24;

	public Dictionary<string, DATA31> dic31;

	public Dictionary<string, DATA33> dic33;

	public Dictionary<string, DATA34> dic34;

	public Dictionary<string, DATA35> dic35;

	public Dictionary<string, DATA36> dic36;

	public Dictionary<string, DATA37> dic37;

	public Dictionary<string, DATA38> dic38;

	public Dictionary<string, DATA39> dic39;

	public Dictionary<string, DATA40> dic40;

	public Dictionary<string, DATA41> dic41;

	public Dictionary<string, DATA42> dic42;

	public Dictionary<string, DATA43> dic43;

	public Dictionary<string, DATA44> dic44;

	private Dictionary<string, Dictionary<string, List<DATA1>>> passwordItemDic1;

	public GameManager gameManager;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		data0 = ReadOneDataConfig<DATA0_ARRAY>("data0");
		data1 = ReadOneDataConfig<DATA1_ARRAY>("data1");
		data2 = ReadOneDataConfig<DATA2_ARRAY>("data2");
		data3 = ReadOneDataConfig<DATA3_ARRAY>("data3");
		data11 = ReadOneDataConfig<DATA11_ARRAY>("data11");
		data13 = ReadOneDataConfig<DATA13_ARRAY>("data13");
		data14 = ReadOneDataConfig<DATA14_ARRAY>("data14");
		data15 = ReadOneDataConfig<DATA15_ARRAY>("data15");
		data16 = ReadOneDataConfig<DATA16_ARRAY>("data16");
		data17 = ReadOneDataConfig<DATA17_ARRAY>("data17");
		data20 = ReadOneDataConfig<DATA20_ARRAY>("data20");
		data21 = ReadOneDataConfig<DATA21_ARRAY>("data21");
		data22 = ReadOneDataConfig<DATA22_ARRAY>("data22");
		data23 = ReadOneDataConfig<DATA23_ARRAY>("data23");
		data24 = ReadOneDataConfig<DATA24_ARRAY>("data24");
		data31 = ReadOneDataConfig<DATA31_ARRAY>("data31");
		data33 = ReadOneDataConfig<DATA33_ARRAY>("data33");
		data34 = ReadOneDataConfig<DATA34_ARRAY>("data34");
		data35 = ReadOneDataConfig<DATA35_ARRAY>("data35");
		data36 = ReadOneDataConfig<DATA36_ARRAY>("data36");
		data37 = ReadOneDataConfig<DATA37_ARRAY>("data37");
		data38 = ReadOneDataConfig<DATA38_ARRAY>("data38");
		data39 = ReadOneDataConfig<DATA39_ARRAY>("data39");
		data41 = ReadOneDataConfig<DATA41_ARRAY>("data41");
		data40 = ReadOneDataConfig<DATA40_ARRAY>("data40");
		data42 = ReadOneDataConfig<DATA42_ARRAY>("data42");
		data43 = ReadOneDataConfig<DATA43_ARRAY>("data43");
		data44 = ReadOneDataConfig<DATA44_ARRAY>("data44");
		ReadDictionary();
	}

	public List<DATA44> GetAllSqlItem(string eventid)
	{
		Debug.LogError("shijianid:" + eventid);
		List<DATA44> list = new List<DATA44>();
		foreach (DATA44 value in dic44.Values)
		{
			if (value.@event.ToString().Replace(".0", "").Equals(eventid))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<DATA1> GetAllItems(string eventid)
	{
		List<DATA1> list = new List<DATA1>();
		foreach (DATA1 value in dic1.Values)
		{
			if (value.eventid.ToString().Equals(eventid))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<DATA2> GetSearchResults(string eventid, string condition)
	{
		List<DATA2> list = new List<DATA2>();
		foreach (DATA2 value in dic2.Values)
		{
			if (gameManager.IsBasic() || !string.IsNullOrEmpty(I18N.instance.getValue(value.method).Trim()))
			{
				if (!value.method.Equals("") && value.eventid.ToString().Equals(eventid) && value.pic.Equals("") && condition.ToLower().Contains(I18N.instance.getValue(value.method).ToLower()))
				{
					list.Add(value);
				}
				if (!value.method.Equals("") && value.eventid.ToString().Equals("111111") && condition.ToLower().Contains(I18N.instance.getValue(value.method).ToLower()))
				{
					list.Add(value);
				}
			}
		}
		return list;
	}

	public List<DATA14> GetData14ByEventid(string eventid)
	{
		List<DATA14> list = new List<DATA14>();
		foreach (DATA14 value in dic14.Values)
		{
			if (value.eventid.ToString().Equals(eventid) || value.eventid.ToString().Equals("0"))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<DATA15> GetMailItems(string eventid)
	{
		List<DATA15> list = new List<DATA15>();
		foreach (DATA15 value in dic15.Values)
		{
			if (value.eventid.ToString().Equals(eventid))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<DATA33> GetAll33Items(string eventid)
	{
		List<DATA33> list = new List<DATA33>();
		foreach (DATA33 value in dic33.Values)
		{
			if (value.eventid.ToString().Equals(eventid))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<DATA37> GetAll37Items(string eventid)
	{
		List<DATA37> list = new List<DATA37>();
		foreach (DATA37 value in dic37.Values)
		{
			if (value.eventid.ToString().Equals(eventid))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<DATA20> GetAll20Items(string eventid)
	{
		List<DATA20> list = new List<DATA20>();
		foreach (DATA20 value in dic20.Values)
		{
			if (value.eventid.ToString().Equals(eventid))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public Dictionary<string, int> GetCurrentMissionItems(string eventid)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (KeyValuePair<string, DATA20> allMissionItem in GetAllMissionItems(eventid))
		{
			if (gameManager.player.playerdata.missionlist.ContainsKey(allMissionItem.Key))
			{
				dictionary.Add(allMissionItem.Key, int.Parse(gameManager.player.playerdata.missionlist[allMissionItem.Key]));
			}
			else
			{
				Debug.LogError("item.Key:" + allMissionItem.Key);
			}
		}
		return dictionary;
	}

	public Dictionary<string, DATA20> GetAllMissionItems(string eventid)
	{
		Dictionary<string, DATA20> dictionary = new Dictionary<string, DATA20>();
		foreach (DATA20 value in dic20.Values)
		{
			if (value.eventid.ToString().Equals(eventid))
			{
				dictionary.Add(value.ID.ToString(), value);
			}
		}
		return dictionary;
	}

	public string GetAllMissionFirstItems(string eventid)
	{
		new Dictionary<string, DATA20>();
		foreach (DATA20 value in dic20.Values)
		{
			if (value.eventid.ToString().Equals(eventid) && value.pos == 1)
			{
				return value.ID.ToString();
			}
		}
		return "";
	}

	public List<DATA20> GetAllMissionItems2(string eventid)
	{
		List<DATA20> list = new List<DATA20>();
		foreach (DATA20 value in dic20.Values)
		{
			if (value.eventid.ToString().Equals(eventid))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<DATA20> GetAllBaseMissionItems(string eventid)
	{
		List<DATA20> list = new List<DATA20>();
		foreach (DATA20 value in dic20.Values)
		{
			if (value.eventid.ToString().Equals(eventid) && value.pos == 0)
			{
				list.Add(value);
			}
			else if (value.eventid.ToString().Equals(eventid) && value.pos == 9)
			{
				list.Add(value);
			}
			else if (value.eventid.ToString().Equals(eventid) && value.pos == 8)
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<DATA20> GetMissionItems(string eventid, int type)
	{
		List<DATA20> list = new List<DATA20>();
		foreach (DATA20 value in dic20.Values)
		{
			if (value.eventid.ToString().Equals(eventid) && value.pos == type)
			{
				list.Add(value);
			}
		}
		return list;
	}

	public string GetLastMissionItem(string eventid)
	{
		foreach (DATA20 value in dic20.Values)
		{
			if (value.eventid.ToString().Equals(eventid) && value.last == 2)
			{
				return value.ID.ToString();
			}
		}
		return "";
	}

	public List<DATA20> GetMissionItems(string eventid)
	{
		List<DATA20> list = new List<DATA20>();
		foreach (DATA20 value in dic20.Values)
		{
			if (value.eventid.ToString().Equals(eventid))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<DATA36> GetSurveillanceItems(string eventid)
	{
		List<DATA36> list = new List<DATA36>();
		foreach (DATA36 value in dic36.Values)
		{
			if (value.eventid.ToString().Equals(eventid))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public List<DATA36> GetShowSurveillanceItems(string eventid)
	{
		List<DATA36> list = new List<DATA36>();
		List<DATA36> surveillanceItems = GetSurveillanceItems(eventid);
		for (int i = 0; i < surveillanceItems.Count; i++)
		{
			string[] array = surveillanceItems[i].itemids.Substring(1).Split(';');
			for (int j = 0; j < array.Length; j++)
			{
				if (gameManager.player.playerdata.itemlist.Contains(array[j]) || gameManager.isbug)
				{
					list.Add(surveillanceItems[i]);
					break;
				}
			}
		}
		return list;
	}

	private T ReadOneDataConfig<T>(string FileName)
	{
		FileStream dataFileStream = GetDataFileStream(FileName);
		if (dataFileStream != null)
		{
			T result = Serializer.Deserialize<T>(dataFileStream);
			dataFileStream.Close();
			return result;
		}
		return default(T);
	}

	private FileStream GetDataFileStream(string fileName)
	{
		string dataConfigPath = GetDataConfigPath(fileName);
		if (File.Exists(dataConfigPath))
		{
			return new FileStream(dataConfigPath, FileMode.Open);
		}
		return null;
	}

	public Dictionary<string, DATA43> GetYulunNewsInfo(string eventid, string newsType)
	{
		Dictionary<string, DATA43> dictionary = new Dictionary<string, DATA43>();
		foreach (DATA43 value in dic43.Values)
		{
			if (value.eventid.ToString().Equals(eventid) && value.type.Replace(".0", "") == newsType)
			{
				dictionary.Add(value.ID.ToString(), value);
			}
		}
		return dictionary;
	}

	public Dictionary<string, List<DATA1>> getEventPasswordItem(string eventid)
	{
		return passwordItemDic1[eventid];
	}

	private string GetDataConfigPath(string fileName)
	{
		return Application.streamingAssetsPath + "/DataConfig/tnt_deploy_" + fileName + ".data";
	}

	private void ReadDictionary()
	{
		dic0 = new Dictionary<string, DATA0>();
		dic0EventID = new Dictionary<string, DATA0>();
		foreach (DATA0 item in data0.items)
		{
			dic0.Add(item.ID.ToString(), item);
			dic0EventID.Add(item.eventid.ToString(), item);
		}
		dic1 = new Dictionary<string, DATA1>();
		passwordItemDic1 = new Dictionary<string, Dictionary<string, List<DATA1>>>();
		foreach (DATA1 item2 in data1.items)
		{
			if (item2.ID.ToString() == "10597")
			{
				item2.role = "#3100046";
				item2.sign = 3;
			}
			if (item2.passwordnumber >= 1 && item2.passwordnumber <= 6)
			{
				if (!passwordItemDic1.ContainsKey(item2.eventid.ToString()))
				{
					Dictionary<string, List<DATA1>> dictionary = new Dictionary<string, List<DATA1>>();
					List<DATA1> list = new List<DATA1>();
					list.Add(item2);
					dictionary.Add(item2.name, list);
					passwordItemDic1.Add(item2.eventid.ToString(), dictionary);
				}
				else
				{
					Dictionary<string, List<DATA1>> dictionary2 = passwordItemDic1[item2.eventid.ToString()];
					if (!dictionary2.ContainsKey(item2.name))
					{
						List<DATA1> list2 = new List<DATA1>();
						list2.Add(item2);
						dictionary2[item2.name] = list2;
					}
					else
					{
						dictionary2[item2.name].Add(item2);
					}
				}
			}
			dic1.Add(item2.ID.ToString(), item2);
		}
		dic2 = new Dictionary<string, DATA2>();
		foreach (DATA2 item3 in data2.items)
		{
			dic2.Add(item3.ID.ToString(), item3);
		}
		dic3 = new Dictionary<string, DATA3>();
		foreach (DATA3 item4 in data3.items)
		{
			dic3.Add(item4.ID.ToString(), item4);
		}
		dic11 = new Dictionary<string, DATA11>();
		foreach (DATA11 item5 in data11.items)
		{
			dic11.Add(item5.ID.ToString(), item5);
		}
		dic13 = new Dictionary<string, DATA13>();
		foreach (DATA13 item6 in data13.items)
		{
			dic13.Add(item6.ID.ToString(), item6);
		}
		dic14 = new Dictionary<string, DATA14>();
		foreach (DATA14 item7 in data14.items)
		{
			dic14.Add(item7.ID.ToString(), item7);
		}
		dic14_userid = new Dictionary<string, DATA14>();
		foreach (DATA14 item8 in data14.items)
		{
			dic14_userid.Add((item8.user.ToString() == "admin") ? "admin" : (item8.eventid + "_" + item8.user.ToString()), item8);
		}
		dic15 = new Dictionary<string, DATA15>();
		foreach (DATA15 item9 in data15.items)
		{
			dic15.Add(item9.ID.ToString(), item9);
		}
		dic16 = new Dictionary<string, DATA16>();
		foreach (DATA16 item10 in data16.items)
		{
			dic16.Add(item10.ID.ToString(), item10);
		}
		dic17 = new Dictionary<string, DATA17>();
		foreach (DATA17 item11 in data17.items)
		{
			dic17.Add(item11.ID.ToString(), item11);
		}
		dic20 = new Dictionary<string, DATA20>();
		foreach (DATA20 item12 in data20.items)
		{
			dic20.Add(item12.ID.ToString(), item12);
		}
		dic21 = new Dictionary<string, DATA21>();
		foreach (DATA21 item13 in data21.items)
		{
			dic21.Add(item13.ID.ToString(), item13);
		}
		dic22 = new Dictionary<string, DATA22>();
		foreach (DATA22 item14 in data22.items)
		{
			dic22.Add(item14.ID.ToString(), item14);
		}
		dic23 = new Dictionary<string, DATA23>();
		foreach (DATA23 item15 in data23.items)
		{
			dic23.Add(item15.ID.ToString(), item15);
		}
		dic24 = new Dictionary<string, DATA24>();
		foreach (DATA24 item16 in data24.items)
		{
			dic24.Add(item16.ID.ToString(), item16);
		}
		dic31 = new Dictionary<string, DATA31>();
		foreach (DATA31 item17 in data31.items)
		{
			dic31.Add(item17.ID.ToString(), item17);
		}
		dic33 = new Dictionary<string, DATA33>();
		foreach (DATA33 item18 in data33.items)
		{
			dic33.Add(item18.ID.ToString(), item18);
		}
		dic34 = new Dictionary<string, DATA34>();
		foreach (DATA34 item19 in data34.items)
		{
			dic34.Add(item19.ID.ToString(), item19);
		}
		dic35 = new Dictionary<string, DATA35>();
		foreach (DATA35 item20 in data35.items)
		{
			dic35.Add(item20.ID.ToString(), item20);
		}
		dic36 = new Dictionary<string, DATA36>();
		foreach (DATA36 item21 in data36.items)
		{
			dic36.Add(item21.ID.ToString(), item21);
		}
		dic37 = new Dictionary<string, DATA37>();
		foreach (DATA37 item22 in data37.items)
		{
			dic37.Add(item22.ID.ToString(), item22);
		}
		dic38 = new Dictionary<string, DATA38>();
		foreach (DATA38 item23 in data38.items)
		{
			dic38.Add(item23.ID.ToString(), item23);
		}
		dic39 = new Dictionary<string, DATA39>();
		foreach (DATA39 item24 in data39.items)
		{
			dic39.Add(item24.ID.ToString(), item24);
		}
		dic40 = new Dictionary<string, DATA40>();
		foreach (DATA40 item25 in data40.items)
		{
			dic40.Add(item25.id.ToString(), item25);
		}
		dic41 = new Dictionary<string, DATA41>();
		foreach (DATA41 item26 in data41.items)
		{
			dic41.Add(item26.ID.ToString(), item26);
		}
		dic42 = new Dictionary<string, DATA42>();
		foreach (DATA42 item27 in data42.items)
		{
			dic42.Add(item27.ID.ToString(), item27);
		}
		dic43 = new Dictionary<string, DATA43>();
		foreach (DATA43 item28 in data43.items)
		{
			dic43.Add(item28.ID.ToString(), item28);
		}
		dic44 = new Dictionary<string, DATA44>();
		foreach (DATA44 item29 in data44.items)
		{
			dic44.Add(item29.id.ToString(), item29);
		}
	}
}
