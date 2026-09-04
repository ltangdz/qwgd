using UnityEngine;
using UnityEngine.UI;

public class PersonPanel : MonoBehaviour
{
	public Singleperson singleperson;

	public GameObject listpanel;

	public Button btn_singlepersonback;

	public TotalPanel totalPanel;

	private void Start()
	{
		btn_singlepersonback.onClick.AddListener(delegate
		{
			if (listpanel.activeSelf)
			{
				totalPanel.gameObject.SetActive(value: true);
				base.gameObject.SetActive(value: false);
			}
			else
			{
				listpanel.SetActive(value: true);
				singleperson.gameObject.SetActive(value: false);
			}
		});
	}

	public void ShowSinglePerson(int dbid)
	{
		singleperson.gameObject.SetActive(value: true);
		singleperson.Init(dbid);
		listpanel.SetActive(value: false);
	}
}
