using System;
using UnityEngine;

public sealed class DLCNameUtil
{
	private static readonly Lazy<DLCNameUtil> lazy = new Lazy<DLCNameUtil>(() => new DLCNameUtil());

	private GameManager _gameManager;

	public static DLCNameUtil Instance => lazy.Value;

	public GameManager GameManager
	{
		get
		{
			return _gameManager;
		}
		set
		{
			_gameManager = value;
		}
	}

	private DLCNameUtil()
	{
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public string GetPrefabPathDLC(GameTypeEnum gameType)
	{
		if (gameType == GameTypeEnum.DLC7)
		{
			return "_DLC7/Prefabs/";
		}
		return "";
	}

	public string GetTitanTipDialogName()
	{
		return "_DLC7/Prefabs/TitanDialog";
	}

	public string GetFailDialogName()
	{
		return "_DLC7/Prefabs/FailDialog";
	}

	public string GetNoteTabName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/notetab2";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/notetab_dlc7";
		}
		return "notetab";
	}

	public string GetNoteItemName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/noteitem_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/noteitem_dlc7";
		}
		return "noteitem";
	}

	public string GetNotePanelName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/notepanel_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/notepanel_dlc7";
		}
		return "notepanel";
	}

	public string GetNoteItemToolName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/noteitemtool_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/noteitemtool_dlc7";
		}
		return "noteitemtool";
	}

	public string GetPasswordDialogName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/passwordDialog_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/passwordDialog_dlc7";
		}
		return "Dialog/passwordDialog";
	}

	public string GetPasswordDialog2Name()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/passwordDialog2_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/passwordDialog2_dlc7";
		}
		return "Dialog/passwordDialog2";
	}

	public string GetSqlDialogName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/sqlDialog_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/sqlDialog_dlc7";
		}
		return "Dialog/sqlDialog";
	}

	public string GetPhoneDialogName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/phoneDialog_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/phoneDialog_dlc7";
		}
		return "Dialog/phoneDialog";
	}

	public string GetPhoneListName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/phone_list_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/phone_list_dlc7";
		}
		return "phone_list";
	}

	public string GetPhoneItemName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/phone_item_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/phone_item_dlc7";
		}
		return "phone_item";
	}

	public string GetWeizhuangName()
	{
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/weizhuang_dlc7";
		}
		return "Dialog/weizhuang";
	}

	public string GetWeizhuangItemName()
	{
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/weizhuang_item_dlc7";
		}
		return "weizhuang_item";
	}

	public string GetWeizhuangChoiceName()
	{
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/weizhuang_choicebox_dlc7";
		}
		return "weizhuang_choicebox";
	}

	public string GetCamListName()
	{
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/cam_list_dlc7";
		}
		return "cam_list";
	}

	public string GetPhoneItemBakName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/phone_itembak_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/phone_itembak_dlc7";
		}
		return "phone_itembak";
	}

	public string GetFishDialogName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/fishDialog_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/fishDialog_dlc7";
		}
		return "Dialog/fishDialog1";
	}

	public string GetFishItemName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/fishitem_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/fishitem_dlc7";
		}
		return "fishitem";
	}

	public string GetNoteDragItemName()
	{
		_gameManager.IsAllDlc();
		return "_DLC/Prefabs/HomeTools/notedrag";
	}

	public string GetInvadeDialogName()
	{
		if (_gameManager.Is_Dlc6())
		{
			return "_DLC/Prefabs/HomeTools/invadeDialog_dlc";
		}
		if (_gameManager.Is_Dlc7())
		{
			return "_DLC/Prefabs/HomeTools/invadeDialog_dlc7";
		}
		return "Dialog/invadeDialog";
	}

	public string GetFishPhoneInvadeDialogName()
	{
		if (_gameManager.IsAllDlc())
		{
			return "_DLC/Prefabs/HomeTools/FishPhoneInvadeDialog_dlc";
		}
		return "Dialog/FishPhoneInvadeDialog";
	}

	public string GetInvadephoneitemName()
	{
		if (_gameManager.IsAllDlc())
		{
			return "_DLC/Prefabs/HomeTools/invadephoneitem_dlc";
		}
		return "invadephoneitem";
	}

	public string GetInvadephoneitem0Name()
	{
		if (_gameManager.IsAllDlc())
		{
			return "_DLC/Prefabs/HomeTools/invadephoneitem0_dlc";
		}
		return "invadephoneitem0";
	}

	public string GetInvadephoneitem1Name()
	{
		if (_gameManager.IsAllDlc())
		{
			return "_DLC/Prefabs/HomeTools/invadephoneitem1_dlc";
		}
		return "invadephoneitem1";
	}

	public string GetInvadephoneitem2Name()
	{
		if (_gameManager.IsAllDlc())
		{
			return "_DLC/Prefabs/HomeTools/invadephoneitem2_dlc";
		}
		return "invadephoneitem2";
	}

	public string GetGoalitemName()
	{
		if (_gameManager.IsAllDlc())
		{
			return "_DLC/Prefabs/HomeTools/goalitem_dlc";
		}
		return "goalitem2";
	}

	public string GetBrowserSearchName()
	{
		if (_gameManager.IsAllDlc())
		{
			return "Browser/browser_search_dlc";
		}
		return "Browser/browser_search";
	}

	public string GetWebNewsName()
	{
		if (_gameManager.IsAllDlc())
		{
			return "Browser/web_news_dlc";
		}
		return "Browser/web_news";
	}

	public string GetBrowserSocialName()
	{
		if (_gameManager.IsAllDlc())
		{
			return "Browser/browser_social_dlc";
		}
		return "Browser/browser_social";
	}

	public string GetFingerCodeDialog()
	{
		if (_gameManager.IsAllDlc())
		{
			return "Dialog/fingercodeDialog_dlc";
		}
		return "Dialog/fingercodeDialog";
	}

	public string getInvadeMuma()
	{
		if (_gameManager.IsAllDlc())
		{
			return "InvadeMuma_dlc";
		}
		return "InvadeMuma";
	}

	public string[] GetWord()
	{
		return new string[324]
		{
			"毛泽东", "周恩来", "刘少奇", "朱德", "彭德怀", "林彪", "刘伯承", "陈毅", "贺龙", "聂荣臻",
			"徐向前", "罗荣桓", "叶剑英", "李大钊", "陈独秀", "孙中山", "孙文", "孙逸仙", "邓小平", "陈云",
			"江泽民", "李鹏", "朱镕基", "李瑞环", "尉健行", "李岚清", "胡锦涛", "罗干", "温家宝", "吴邦国",
			"曾庆红", "贾庆林", "黄菊", "吴官正", "李长春", "吴仪", "回良玉", "曾培炎", "周永康", "曹刚川",
			"唐家璇", "华建敏", "陈至立", "陈良宇", "张德江", "张立昌", "张德江", "张高丽", "王岐山", "刘云山",
			"俞正声", "王乐泉", "刘云山", "王刚", "王兆国", "刘淇", "贺国强", "郭伯雄", "胡耀邦", "王乐泉",
			"王兆国", "周永康", "李登辉", "连战", "陈水扁", "宋楚瑜", "吕秀莲", "郁慕明", "蒋介石", "蒋中正",
			"蒋经国", "马英九", "习近平", "李克强", "吴帮国", "无帮国", "无邦国", "无帮过", "瘟家宝", "假庆林",
			"甲庆林", "假青林", "离长春", "习远平", "袭近平", "李磕墙", "贺过墙", "和锅枪", "粥永康", "轴永康",
			"肘永康", "周健康", "粥健康", "周小康", "李肇星务", "国务委员", "国务院", "中央委员", "发改委", "国家发展和改革委员会",
			"发展和改革委员会", "薄熙来", "温家饱", "温假饱", "胡惊涛", "习仲勋", "华国锋", "徐才厚", "王立军", "彭丽媛",
			"令计划", "本拉登", "奥马尔", "柴玲", "达赖喇嘛", "江青", "张春桥", "姚文元", "王洪文", "东条英机",
			"希特勒", "墨索里尼", "冈村秀树", "冈村宁次", "高丽朴", "赵紫阳", "王丹", "沃尔开西", "李洪志", "李大师",
			"赖昌星", "马加爵", "班禅", "额尔德尼", "山本五十六", "阿扁", "阿扁万岁", "热那亚", "热比娅", "尖阁列岛",
			"实际神", "东方闪电", "全能神", "安倍晋三", "金正恩", "释迦牟尼", "阿弥陀佛", "多维", "河殇", "摩门教",
			"穆罕默德", "圣战", "耶和华", "耶稣", "伊斯兰", "真主安拉", "白莲教", "天主教", "基督教", "东正教",
			"大法", "法轮", "法轮功", "瘸腿帮", "真理教", "真善忍", "转法轮", "自焚", "走向圆满", "黄大仙",
			"跳大神", "神汉", "神婆", "真理教", "大卫教", "阎王", "黑白无常", "牛头马面", "藏独", "高丽棒子",
			"疆独", "蒙古鞑子", "台独", "台独分子", "台联", "台湾民国", "西藏独立", "新疆独立", "南蛮", "老毛子",
			"回民吃猪肉", "回民都是猪", "恐怖组织", "买毒品", "卖毒品", "钓鱼岛", "钓鱼岛不属于中国", "突尼斯", "新闻出版总署", "新闻出版署",
			"处女", "房事", "押大", "押小", "坐台", "猥亵", "猥琐", "肉欲", "肉体", "排泄",
			"卵子", "西藏314事件", "新疆75事件", "新疆国", "党中央", "新闻管制", "一边一国", "两国论", "分裂中国", "革命",
			"茉莉花", "突尼斯", "国内动态详情", "回回", "六四", "六四运动", "美国之音", "密宗", "民国", "民进党",
			"民运", "民主", "民主潮", "摩门教", "纳粹", "南华早报", "南蛮", "明慧网", "起义", "亲民党",
			"瘸腿帮", "人民报", "法轮功", "法轮大法", "打倒共产党", "台独万岁", "圣战", "示威", "台独", "台独分子",
			"台联", "台湾民国", "台湾岛国", "台湾国", "台湾独立", "太子党", "天安门事件", "屠杀", "小泉", "新党",
			"新疆独立", "新疆分裂", "新疆国", "疆独", "西藏独立", "西藏分裂", "西藏国", "藏独", "藏青会", "藏妇会",
			"学潮", "学运", "一党专政", "一中一台", "两个中国", "一贯道", "游行", "造反", "真善忍", "镇压",
			"政变", "政治", "政治反对派", "政治犯", "中共", "共产党", "反党", "反共", "政府", "民主党",
			"中国之春", "转法轮", "自焚", "共党", "共匪", "苏家屯", "基地组织", "塔利班", "东亚病夫", "支那",
			"高治联", "高自联", "专政", "专制", "世界维吾尔大会", "核工业基地", "核武器", "铀", "原子弹", "氢弹",
			"导弹", "核潜艇", "大参考", "小参考", "国内动态清样", "全能教", "新疆恐怖", "台湾政府", "新疆势力", "新疆恐怖势力",
			"新疆万岁", "新疆", "占中", "占领中环"
		};
	}
}
