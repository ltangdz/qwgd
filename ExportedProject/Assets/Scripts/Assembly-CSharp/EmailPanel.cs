using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailPanel : MonoBehaviour
{
	public GameObject listpanel;

	public Button btn_singleemailback;

	public TotalPanel totalPanel;

	public GameObject currentemailpanel;

	public List<GameObject> maillist = new List<GameObject>();

	private void Start()
	{
		btn_singleemailback.onClick.AddListener(delegate
		{
			if (listpanel.activeSelf)
			{
				totalPanel.gameObject.SetActive(value: true);
				base.gameObject.SetActive(value: false);
			}
			else
			{
				listpanel.SetActive(value: true);
				if (currentemailpanel != null)
				{
					currentemailpanel.SetActive(value: false);
				}
			}
		});
	}

	public void ShowSingleEmail(int emailid)
	{
		if (currentemailpanel != null)
		{
			currentemailpanel.SetActive(value: false);
		}
		maillist[emailid].SetActive(value: true);
		currentemailpanel = maillist[emailid];
		listpanel.SetActive(value: false);
	}
}
