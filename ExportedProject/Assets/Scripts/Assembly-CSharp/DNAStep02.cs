using UnityEngine;
using UnityEngine.UI;

public class DNAStep02 : MonoBehaviour
{
	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private GameObject step03;

	[SerializeField]
	private Button btn_start;

	private void Start()
	{
		btn_start.onClick.AddListener(delegate
		{
			step03.gameObject.SetActive(value: true);
			step02.gameObject.SetActive(value: false);
		});
	}
}
