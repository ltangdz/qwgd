using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
	public Button btn_open;

	public bool isopen;

	public GameObject content;

	public Sprite[] sprites;

	public Image img_arrow;

	private void Start()
	{
		btn_open.onClick.AddListener(delegate
		{
			StartCoroutine(OpenClosePanel(isopen));
		});
	}

	public void OpenPanel(bool isop)
	{
		StartCoroutine(OpenClosePanel(isop));
	}

	private IEnumerator OpenClosePanel(bool isop)
	{
		for (int i = 0; i < content.transform.childCount; i++)
		{
			content.transform.GetChild(i).GetComponent<LayoutElement>().ignoreLayout = !isop;
			content.transform.GetChild(i).gameObject.SetActive(isop);
			yield return new WaitForSeconds(0.05f);
		}
		img_arrow.sprite = ((!isop) ? sprites[0] : sprites[1]);
		GetComponent<VerticalLayoutGroup>().spacing = ((isopen && content.transform.childCount > 0) ? 22 : 0);
		isopen = !isop;
	}
}
