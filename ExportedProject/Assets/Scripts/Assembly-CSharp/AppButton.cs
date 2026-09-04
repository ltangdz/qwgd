using System.Collections.Generic;
using UnityEngine;

public class AppButton : MonoBehaviour
{
	[Tooltip("0 不需要破解就可以打开，1 需要破解才能打开——输入密码，2 需要破解才能打开——图案密码，3 无法打开, 4 需要破解才能打开——数字密码输入, 5:需要破解才能打开——图片回答 6更改数字密码")]
	public int type;

	[Tooltip("0：相册 1：日记 2：imeet 3：出行天下 4：保罗万象 5：gps 6：电话 7：联系人 8：浏览器 9：短信 10：日历 11：设置 12：groomusic 13：电话录音 14：金色天堂")]
	public int btnType;

	public bool isChecked;

	public bool isUnlock;

	[Header("破解锁需要的密码")]
	public string password;

	[Header("数字密码错误提示")]
	public string passwordTip;

	[Header("设置信息")]
	public List<string> names;

	public List<string> prefabs;

	public List<int> readTypes;

	[Header("链接wifi后数据是否已更新")]
	public bool refresh;

	public bool isNeedWifiRefresh;

	[Header("破解提示")]
	public string titlekey = "";

	[Header("加密内容")]
	public string lockContent = "";
}
