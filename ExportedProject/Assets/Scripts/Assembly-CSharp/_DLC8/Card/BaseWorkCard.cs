using System.IO;
using SFB;
using UnityEngine;

namespace _DLC8.Card
{
	public class BaseWorkCard : MonoBehaviour
	{
		public Camera camera;

		private RectTransform _rt;

		public RectTransform RT
		{
			get
			{
				if (_rt == null)
				{
					_rt = base.transform.GetComponent<RectTransform>();
				}
				return _rt;
			}
		}

		public void SaveImage(string name)
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			StandaloneFileBrowser.SaveFilePanelAsync("Title", "", name, "jpg", delegate(string path)
			{
				if (!string.IsNullOrEmpty(path))
				{
					File.WriteAllBytes(path, CaptureCamera(camera, rectTransform.rect));
				}
			});
		}

		private byte[] CaptureCamera(Camera camera, Rect rect)
		{
			rect = new Rect(0f, 0f, rect.width, rect.height);
			Debug.LogError(rect);
			RenderTexture renderTexture = (camera.targetTexture = new RenderTexture((int)rect.width, (int)rect.height, 0));
			camera.Render();
			RenderTexture.active = renderTexture;
			Texture2D texture2D = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, mipChain: false);
			texture2D.ReadPixels(rect, 0, 0);
			texture2D.Apply();
			camera.targetTexture = null;
			RenderTexture.active = null;
			Object.Destroy(renderTexture);
			return texture2D.EncodeToPNG();
		}
	}
}
