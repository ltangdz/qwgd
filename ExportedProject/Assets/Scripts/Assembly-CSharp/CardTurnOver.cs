using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CardTurnOver : MonoBehaviour
{
	public GameObject mFront;

	public GameObject mBack;

	public CardState mCardState;

	public float mTime = 0.3f;

	private bool isActive;

	public void Init()
	{
		if (mCardState == CardState.Front)
		{
			mFront.transform.eulerAngles = Vector3.zero;
			mBack.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		}
		else
		{
			mFront.transform.eulerAngles = new Vector3(90f, 0f, 0f);
			mBack.transform.eulerAngles = Vector3.zero;
		}
	}

	private void Start()
	{
		Init();
	}

	public void StartBack()
	{
		if (!isActive)
		{
			StartCoroutine(ToBack());
		}
	}

	public void StartFront()
	{
		if (!isActive)
		{
			StartCoroutine(ToFront());
		}
	}

	private IEnumerator ToBack()
	{
		isActive = true;
		mFront.transform.DORotate(new Vector3(90f, 0f, 0f), mTime);
		for (float i = mTime; i >= 0f; i -= Time.deltaTime)
		{
			yield return 0;
		}
		mBack.transform.DORotate(new Vector3(0f, 0f, 0f), mTime);
		isActive = false;
	}

	private IEnumerator ToFront()
	{
		isActive = true;
		mBack.transform.DORotate(new Vector3(90f, 0f, 0f), mTime);
		for (float i = mTime; i >= 0f; i -= Time.deltaTime)
		{
			yield return 0;
		}
		mFront.transform.DORotate(new Vector3(0f, 0f, 0f), mTime);
		isActive = false;
	}
}
