using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7
{
	public class DlcContentSizeFitter : MonoBehaviour
	{
		private ContentSizeFitter contentSizeFitter;

		private ContentSizeFitter[] componentsInParent;

		private void Start()
		{
			contentSizeFitter = GetComponent<ContentSizeFitter>();
			componentsInParent = GetComponentsInParent<ContentSizeFitter>();
			StartCoroutine(SizeFitter(contentSizeFitter, componentsInParent));
		}

		public void Reset()
		{
			StartCoroutine(SizeFitter(contentSizeFitter, componentsInParent));
		}

		private IEnumerator SizeFitter(ContentSizeFitter cur, ContentSizeFitter[] componentsInParent)
		{
			yield return new WaitForEndOfFrame();
			if (cur != null)
			{
				cur.enabled = false;
			}
			for (int i = 0; i < componentsInParent.Length; i++)
			{
				componentsInParent[i].enabled = false;
			}
			yield return new WaitForEndOfFrame();
			if (cur != null)
			{
				cur.enabled = true;
			}
			for (int j = 0; j < componentsInParent.Length; j++)
			{
				componentsInParent[j].enabled = true;
			}
		}
	}
}
