using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanDoor : MonoBehaviour
{
	[Tooltip("门的类型，-1：无，0：关闭，1：打开")]
	public int type;

	public List<Sprite> doorSprite;

	public void ChangeType()
	{
		type = ((type + 1 <= 1) ? 1 : 0);
		GetComponent<Image>().sprite = doorSprite[type];
	}
}
