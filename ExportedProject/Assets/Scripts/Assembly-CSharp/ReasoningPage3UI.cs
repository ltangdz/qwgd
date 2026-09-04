using System.Collections;
using DLC7.Reasoning;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningPage3UI : MonoBehaviour
{
	public UIEraserTexture uIEraserTexture;

	public Button nextBtn;

	private bool hideEraser;

	public ContentSizeFitter contentSizeFitter1;

	public IEnumerator ResetContentSizeFitter(ContentSizeFitter contentSizeFitter)
	{
		contentSizeFitter.enabled = false;
		yield return new WaitForEndOfFrame();
		contentSizeFitter.enabled = true;
	}

	private void Start()
	{
		nextBtn.onClick.AddListener(delegate
		{
		});
		StartCoroutine(ResetContentSizeFitter(contentSizeFitter1));
	}

	public void Update()
	{
		if (uIEraserTexture.CanSee() && !hideEraser)
		{
			hideEraser = true;
			uIEraserTexture.Hide(delegate
			{
				DLC7.Reasoning.ReasoningManager.Instance.NoticeResult("4016");
				nextBtn.gameObject.SetActive(value: false);
			});
		}
	}
}
