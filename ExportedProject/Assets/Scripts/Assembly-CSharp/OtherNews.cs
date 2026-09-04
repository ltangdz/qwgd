using System.Collections.Generic;
using UnityEngine;

public class OtherNews : MonoBehaviour
{
	public List<GameObject> news;

	public void PlayMusic(int i)
	{
		news[i].GetComponent<BeginingNewsWindow>().PlayMusic();
	}
}
