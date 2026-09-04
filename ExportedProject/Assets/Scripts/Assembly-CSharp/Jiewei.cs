using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Jiewei : MonoBehaviour
{
	public Transform jieweibox;

	public List<GameObject> pagebox;

	private int step = 1;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.player.playerdata.completeHideGame)
		{
			gameManager.UnlockAchievements("vanplus");
		}
		else
		{
			gameManager.UnlockAchievements("badinternet");
		}
		StartCoroutine(ShowPage());
	}

	private IEnumerator ShowPage()
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < pagebox.Count; i++)
		{
			pagebox[i].SetActive(value: true);
			pagebox[i].GetComponent<RectTransform>().DOScale(new Vector3(1.09f, 1.09f, 1.09f), 12f);
			if (i > 0)
			{
				pagebox[i - 1].SetActive(value: false);
			}
			yield return new WaitForSeconds(5.7f);
			if (i < pagebox.Count - 1)
			{
				pagebox[i + 1].SetActive(value: true);
				gameManager.homeScene.glitch3.enabled = true;
				float a = 0f;
				DOTween.To(() => a, delegate(float x)
				{
					a = x;
				}, 0.9f, 0.5f).OnUpdate(delegate
				{
					gameManager.homeScene.glitch3._Glitch = a;
				});
				float a2 = 0f;
				gameManager.homeScene.distorted.enabled = true;
				DOTween.To(() => a2, delegate(float x)
				{
					a2 = x;
				}, 1f, 0.5f).OnUpdate(delegate
				{
					gameManager.homeScene.distorted.Distortion = a2;
				});
				yield return new WaitForSeconds(0.2f);
				float a4 = 0f;
				gameManager.homeScene.ditheroffset.enabled = true;
				DOTween.To(() => a4, delegate(float x)
				{
					a4 = x;
				}, 150f, 0.1f).OnUpdate(delegate
				{
					gameManager.homeScene.ditheroffset.Distance.x = a4;
				});
				if (step == 1)
				{
					jieweibox.DOLocalMoveX(-960f, 0.35f);
				}
				else
				{
					jieweibox.DOLocalMoveX(960f, 0.35f);
				}
				step *= -1;
				yield return new WaitForSeconds(0.15f);
				float a5 = 150f;
				DOTween.To(() => a5, delegate(float x)
				{
					a5 = x;
				}, 0f, 0.1f).OnUpdate(delegate
				{
					gameManager.homeScene.ditheroffset.Distance.x = a5;
				}).OnComplete(delegate
				{
					gameManager.homeScene.ditheroffset.enabled = false;
				});
				yield return new WaitForSeconds(0.1f);
				float a6 = 0.9f;
				DOTween.To(() => a6, delegate(float x)
				{
					a6 = x;
				}, 0f, 0.5f).OnUpdate(delegate
				{
					gameManager.homeScene.glitch3._Glitch = a6;
				}).OnComplete(delegate
				{
					gameManager.homeScene.glitch3.enabled = false;
				});
				float a7 = 1f;
				DOTween.To(() => a7, delegate(float x)
				{
					a7 = x;
				}, 0f, 0.5f).OnUpdate(delegate
				{
					gameManager.homeScene.distorted.Distortion = a7;
				}).OnComplete(delegate
				{
					gameManager.homeScene.distorted.enabled = false;
				});
			}
		}
		gameManager.ShowFloatBox();
		gameManager.musicManager.Stop();
		yield return new WaitForSeconds(2f);
		Object.Instantiate(Resources.Load<GameObject>("Dialog/endPanel"), gameManager.homeScene.middle);
		Object.Destroy(base.gameObject);
	}
}
