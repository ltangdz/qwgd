using UnityEngine;

public class PanelSize : MonoBehaviour
{
	public Canvas canvas;

	public RectTransform bk_title;

	private void Start()
	{
	}

	private void Update()
	{
		base.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(base.gameObject.GetComponent<RectTransform>().rect.width, canvas.GetComponent<RectTransform>().rect.height - 47f);
	}
}
