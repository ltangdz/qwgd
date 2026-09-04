using DG.Tweening;
using UnityEngine;

public class UIScale : MonoBehaviour
{
	public void Scale(GameObject go, float scaleTime, Vector2 pivot, Vector3 startScale, Vector3 targetScale)
	{
		if (!(null == go))
		{
			go.GetComponent<RectTransform>().pivot = pivot;
			go.transform.DOKill();
			go.transform.localScale = startScale;
			go.transform.DOScale(targetScale, 0.8f);
		}
	}
}
