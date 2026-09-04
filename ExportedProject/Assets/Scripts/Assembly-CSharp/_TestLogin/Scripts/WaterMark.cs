using UnityEngine;

namespace _TestLogin.Scripts
{
	public class WaterMark : MonoBehaviour
	{
		private void Start()
		{
			GameManager component = GameObject.Find("GameManager").GetComponent<GameManager>();
			if (component != null)
			{
				base.gameObject.SetActive(!component.issteam);
			}
		}
	}
}
