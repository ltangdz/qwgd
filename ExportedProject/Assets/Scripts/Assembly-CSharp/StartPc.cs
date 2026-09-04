using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartPc : MonoBehaviour
{
	[SerializeField]
	private Button videotip;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.soundManager.StopLoop();
		gameManager.soundManager.PlaySound(1);
		videotip.onClick.AddListener(delegate
		{
			Object.Instantiate(Resources.Load("Dialog/Hacker/hackervideoDialog06") as GameObject, base.transform.parent).GetComponent<HackerVideoDialog06>().startpc = base.gameObject;
			videotip.gameObject.SetActive(value: false);
			gameManager.soundManager.Stop();
		});
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
		Sequence sequence = DOTween.Sequence();
		sequence.Append(videotip.transform.DOScale(Vector3.one, 1f));
		sequence.Append(videotip.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f));
		sequence.Play().SetLoops(-1);
		gameManager.soundManager.PlaySoundLoop(2);
	}
}
