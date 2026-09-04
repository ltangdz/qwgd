using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanVirusDialog : MonoBehaviour
	{
		public Text timeText;

		public Image warningImage;

		public ZhadanDialog zhadanDialog;

		public VirusFinishedLoading loadingDialog;

		private float _maxTime = 600f;

		private float _curTime;

		private bool _isFinished;

		private int _warningLevel;

		private void Start()
		{
			_curTime = _maxTime;
			StartCoroutine("Warning");
		}

		public void Finished()
		{
			_isFinished = true;
			Debug.Log("炸弹结束");
			GameManager component = GameObject.Find("GameManager").GetComponent<GameManager>();
			component.player.playerdata.titanStep = 3;
			component.saveManager.SavePlayerData(isshowlogo: true, isForce: true);
			Invoke("DelayFinished", 0.8f);
		}

		public void ShowLoading()
		{
			loadingDialog.gameObject.SetActive(value: true);
			_isFinished = true;
		}

		private IEnumerator Warning()
		{
			while (_isFinished)
			{
				float off = (_maxTime - _curTime) / _maxTime;
				float interval = (1f - off + 0.1f) * 2f;
				warningImage.DOFade(off / 2f, interval).SetEase(Ease.Linear);
				yield return new WaitForSeconds(interval);
				warningImage.DOFade(off / 2f - off / 2f / 3f, interval).SetEase(Ease.Linear);
				yield return new WaitForSeconds(interval);
			}
		}

		private void FixedUpdate()
		{
			if (timeText == null || _isFinished)
			{
				return;
			}
			_curTime -= Time.deltaTime;
			if (_curTime <= 0f)
			{
				zhadanDialog = GetComponentInChildren<ZhadanDialog>();
				if ((bool)zhadanDialog)
				{
					zhadanDialog.isGameOver = true;
				}
				_isFinished = true;
				Invoke("Fail", 1f);
				_curTime = 0f;
			}
			timeText.text = Mathf.FloorToInt(_curTime).ToString();
		}

		public void Fail()
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().player.playerdata.HackerDlc7["bomb"] = false;
			Object.Instantiate(Resources.Load<HackerFailDialog>(DLCNameUtil.Instance.GetFailDialogName()), base.transform.root);
		}

		private void DelayFinished()
		{
			GameManager component = GameObject.Find("GameManager").GetComponent<GameManager>();
			Dictionary<string, bool> hackerDlc = component.player.playerdata.HackerDlc7;
			if (hackerDlc["ddos"] && hackerDlc["bomb"])
			{
				component.UnlockAchievements("tophacker");
			}
			GetComponentInParent<TitanSecondStepDialog>().Finished(TitanSecondStep.VIRUS);
			Object.Destroy(base.gameObject);
		}
	}
}
