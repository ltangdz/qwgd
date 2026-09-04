using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicTurnOver9 : MonoBehaviour
{
	public List<Animator> items;

	public int vol;

	public int row;

	public GameObject picGroup;

	public string picname;

	public string picblurname;

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		Sprite[] array = Resources.LoadAll<Sprite>(picname);
		Sprite[] array2 = Resources.LoadAll<Sprite>(picblurname);
		for (int i = 0; i < row * vol; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("picitem"), picGroup.transform);
			gameObject.transform.Find("tile").GetComponent<Image>().sprite = array[i];
			gameObject.transform.Find("tile").GetComponent<Image>().SetNativeSize();
			gameObject.transform.Find("tile_blur").GetComponent<Image>().sprite = array2[i];
			gameObject.transform.Find("tile_blur").GetComponent<Image>().SetNativeSize();
			gameObject.name = "item" + i;
			items.Add(gameObject.GetComponent<Animator>());
		}
	}

	public void StartShowPic()
	{
		InvokeRepeating("StartTurnOver3", 0.02f, 0.001f);
	}

	private void StartTurnOver3()
	{
		if (items.Count <= 0)
		{
			CancelInvoke();
			return;
		}
		int index = Random.Range(0, items.Count);
		items[index].Play("ani_pictile");
		items.RemoveAt(index);
	}
}
