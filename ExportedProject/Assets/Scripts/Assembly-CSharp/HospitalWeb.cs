using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HospitalWeb : MonoBehaviour
{
	public InputField searchInput;

	public GameObject noResult;

	public Button searchBtn;

	public GameObject webIndex;

	public GameObject webSearch;

	public GameObject searchContent;

	public Button bakIndex;

	private void Start()
	{
		searchBtn.onClick.AddListener(delegate
		{
			SearchResult();
		});
		bakIndex.onClick.AddListener(BakIndex);
	}

	private void BakIndex()
	{
		webIndex.SetActive(value: true);
		webSearch.SetActive(value: false);
		for (int i = 0; i < searchContent.transform.childCount; i++)
		{
			Object.Destroy(searchContent.transform.GetChild(i).gameObject);
		}
	}

	private void SearchResult()
	{
		string text = searchInput.text;
		if (text.Trim() != "")
		{
			if (text.ToLower() == "john hanilton")
			{
				Object.Instantiate(Resources.Load<GameObject>("Browser/hospital_searchform02"), searchContent.transform);
				webIndex.SetActive(value: false);
				webSearch.SetActive(value: true);
			}
			else
			{
				StartCoroutine(ShowWarning(noResult));
			}
		}
		else
		{
			StartCoroutine(ShowWarning(noResult));
		}
		searchInput.text = "";
	}

	private IEnumerator ShowWarning(GameObject obj)
	{
		obj.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		obj.SetActive(value: false);
	}
}
