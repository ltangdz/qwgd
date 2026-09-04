using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class NoteItemTitle : MonoBehaviour
{
	public Text txt_title;

	public Transform contentPanel;

	public Text txt_count;

	public int count;

	public int allcount;

	public string hasid = "";

	private void Start()
	{
	}

	public void Init(string title, int allcount, string hid)
	{
		txt_title.GetComponent<I18NText>().updateTranslation2(title);
		this.allcount = allcount;
		hasid = hid;
		Add();
	}

	public void Add()
	{
		count++;
		txt_count.GetComponent<I18NText>().updateTranslation2(count + "/" + allcount);
	}

	private void Update()
	{
	}
}
