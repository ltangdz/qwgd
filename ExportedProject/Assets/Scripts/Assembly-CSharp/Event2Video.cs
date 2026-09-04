using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Event2Video : MonoBehaviour
{
	public GameObject mainCamera;

	public GameObject washRoom;

	public GameObject controlLight;

	public GameObject washGirl;

	public GameObject imgGirl;

	public GameObject watchScreen;

	public GameObject persen;

	public GameObject black;

	private GameManager gameManager;

	public void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		StartCoroutine(Run());
	}

	private IEnumerator Run()
	{
		mainCamera.GetComponent<CameraFilterPack_Distortion_FishEye>().enabled = true;
		black.GetComponent<Image>().DOFade(0f, 2f);
		yield return new WaitForSeconds(1f);
		gameManager.musicManager.Stop();
		yield return new WaitForSeconds(1f);
		gameManager.musicManager.PlayAnimationSound(1);
		washRoom.GetComponent<RectTransform>().DOLocalMoveX(703.5f, 4f);
		yield return new WaitForSeconds(5f);
		washRoom.GetComponent<RectTransform>().DOScale(new Vector3(2.5f, 2.5f, 2.5f), 0.5f);
		yield return new WaitForSeconds(1f);
		controlLight.GetComponent<Image>().DOFade(0.2f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		controlLight.GetComponent<Image>().DOFade(1f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		controlLight.GetComponent<Image>().DOFade(0.2f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		controlLight.GetComponent<Image>().DOFade(1f, 0.3f);
		yield return new WaitForSeconds(2f);
		StartCoroutine(ShowFloat());
		yield return new WaitForSeconds(2f);
		mainCamera.GetComponent<CameraFilterPack_Distortion_FishEye>().enabled = false;
		washRoom.SetActive(value: false);
		washGirl.SetActive(value: true);
		yield return new WaitForSeconds(1f);
		imgGirl.GetComponent<RectTransform>().DOLocalMoveY(249.95f, 3f);
		yield return new WaitForSeconds(5f);
		StartCoroutine(ShowFloat());
		yield return new WaitForSeconds(2f);
		washGirl.SetActive(value: false);
		watchScreen.SetActive(value: true);
		yield return new WaitForSeconds(4f);
		persen.SetActive(value: true);
		persen.GetComponent<RectTransform>().DOMoveX(0f, 0.3f);
	}

	private IEnumerator ShowFloat()
	{
		black.GetComponent<Image>().DOFade(1f, 2f);
		yield return new WaitForSeconds(2.5f);
		black.GetComponent<Image>().DOFade(0f, 2f);
	}
}
