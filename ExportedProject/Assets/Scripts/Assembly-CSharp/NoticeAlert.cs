using UnityEngine;
using UnityEngine.UI;

public class NoticeAlert : MonoBehaviour
{
	public Button sureBtn;

	public void InitInfo()
	{
		GetComponent<Animator>().Play("ani_showalert");
	}

	private void Start()
	{
		sureBtn.onClick.AddListener(delegate
		{
			GetComponent<Animator>().Play("ani_hidealert");
			Invoke("CloseAlert", 0.8f);
		});
	}

	private void CloseAlert()
	{
		Object.Destroy(base.gameObject);
	}
}
