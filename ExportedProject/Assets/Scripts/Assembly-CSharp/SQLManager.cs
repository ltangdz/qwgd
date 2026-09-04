using System.Collections.Generic;
using Honeti;
using UnityEngine;
using tnt_deploy;

public class SQLManager : MonoBehaviour
{
	public GameManager gameManager;

	public List<string[]> SelectWherePersonTable(string name, string otherinfor)
	{
		Debug.LogError("数据库:" + name + ":" + otherinfor);
		List<DATA44> allSqlItem = gameManager.dataManager.GetAllSqlItem(gameManager.player.GetEventId());
		for (int i = 0; i < allSqlItem.Count; i++)
		{
			Debug.LogError("name:" + I18N.instance.getValue(allSqlItem[i].name0).ToLower());
			if (!name.ToLower().Equals(I18N.instance.getValue(allSqlItem[i].name0).ToLower()))
			{
				continue;
			}
			string text = allSqlItem[i].hitalkid;
			string text2 = allSqlItem[i].addnum;
			string text3 = allSqlItem[i].tel;
			if (text.EndsWith(".0"))
			{
				text = text.Substring(0, text.Length - 2);
			}
			if (text3.EndsWith(".0"))
			{
				text3 = text3.Substring(0, text3.Length - 2);
			}
			if (text2.EndsWith(".0"))
			{
				text2 = text2.Substring(0, text2.Length - 2);
			}
			if (otherinfor.Equals(text2) || otherinfor.ToLower().Equals(allSqlItem[i].email0.ToLower()) || otherinfor.Equals(text) || otherinfor.Equals(allSqlItem[i].birth_en) || otherinfor.Equals(allSqlItem[i].birth) || otherinfor.Equals(text3))
			{
				List<string[]> list = new List<string[]>();
				string[] array = new string[13]
				{
					I18N.instance.getValue(allSqlItem[i].name0),
					(I18N.instance.gameLang == LanguageCode.EN) ? allSqlItem[i].birth_en : allSqlItem[i].birth,
					allSqlItem[i].gender,
					allSqlItem[i].tel,
					allSqlItem[i].address,
					allSqlItem[i].email0,
					allSqlItem[i].idnumber0,
					allSqlItem[i].hitalkid,
					allSqlItem[i].itemid,
					allSqlItem[i].marriage,
					allSqlItem[i].fingerPrint,
					allSqlItem[i].crime,
					null
				};
				if (allSqlItem[i].position == null)
				{
					array[12] = "null";
				}
				else
				{
					string position = allSqlItem[i].position;
					Debug.Log("position:" + position);
					array[12] = (position.StartsWith("^") ? I18N.instance.getValue(position) : position);
				}
				list.Add(array);
				return list;
			}
		}
		return new List<string[]>();
	}

	public List<string> SelectWherePersonTable(string id)
	{
		if (gameManager.dataManager.dic44.ContainsKey(id))
		{
			return new List<string>
			{
				I18N.instance.getValue(gameManager.dataManager.dic44[id].name0),
				gameManager.dataManager.dic44[id].gender,
				(I18N.instance.gameLang == LanguageCode.EN) ? gameManager.dataManager.dic44[id].birth_en : gameManager.dataManager.dic44[id].birth,
				gameManager.dataManager.dic44[id].idnumber0,
				gameManager.dataManager.dic44[id].@event
			};
		}
		return null;
	}

	public List<string> SelectWherePersonTable2(string id)
	{
		if (gameManager.dataManager.dic44.ContainsKey(id))
		{
			return new List<string>
			{
				I18N.instance.getValue(gameManager.dataManager.dic44[id].name0),
				gameManager.dataManager.dic44[id].gender,
				(I18N.instance.gameLang == LanguageCode.EN) ? gameManager.dataManager.dic44[id].birth_en : gameManager.dataManager.dic44[id].birth,
				gameManager.dataManager.dic44[id].idnumber0,
				gameManager.dataManager.dic44[id].address,
				gameManager.dataManager.dic44[id].hitalkid,
				gameManager.dataManager.dic44[id].tel,
				gameManager.dataManager.dic44[id].email0,
				gameManager.dataManager.dic44[id].position,
				gameManager.dataManager.dic44[id].@event
			};
		}
		return null;
	}

	private string Check(string oricontent)
	{
		if (oricontent.Equals("null"))
		{
			return I18N.instance.getValue("^houtai21");
		}
		return oricontent;
	}

	public List<string[]> SelectWherePersonBoatInfo(string name)
	{
		return null;
	}
}
