using UnityEngine;

public class UIDepth : MonoBehaviour
{
	public int order;

	public bool isUI = true;

	private void Start()
	{
		if (isUI)
		{
			Canvas canvas = GetComponent<Canvas>();
			if (canvas == null)
			{
				canvas = base.gameObject.AddComponent<Canvas>();
			}
			canvas.overrideSorting = true;
			canvas.sortingOrder = order;
		}
		else
		{
			Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].sortingOrder = order;
			}
		}
	}
}
