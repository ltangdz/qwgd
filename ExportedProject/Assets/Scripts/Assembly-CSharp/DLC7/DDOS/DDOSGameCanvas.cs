using UnityEngine;

namespace DLC7.DDOS
{
	public class DDOSGameCanvas : MonoBehaviour
	{
		public Canvas gameCanvas;

		public Canvas bgCanvas;

		public Canvas titanCanvas;

		private void Start()
		{
			GameManager component = GameObject.Find("GameManager").GetComponent<GameManager>();
			if (component.player.playerdata.dlc7Invades[2] == 2)
			{
				ShowTitan();
				return;
			}
			DDOSGameController dDOSGameController = Object.Instantiate(Resources.Load<DDOSGameController>("_DLC7/prefabs/DDOS/DDOSGame"), base.transform);
			component.musicManager.PlayMusicLoop(24);
			int[] dlc7Invades = component.player.playerdata.dlc7Invades;
			for (int i = 0; i < 3; i++)
			{
				int num = dlc7Invades[i];
				if (num > 0 && num < 2)
				{
					dDOSGameController.InitData(i + 1);
					break;
				}
			}
		}

		public void ShowTitan()
		{
			gameCanvas.gameObject.SetActive(value: false);
			bgCanvas.gameObject.SetActive(value: false);
			titanCanvas.gameObject.SetActive(value: true);
		}
	}
}
