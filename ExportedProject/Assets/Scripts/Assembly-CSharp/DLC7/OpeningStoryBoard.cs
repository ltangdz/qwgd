using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DLC7
{
	public class OpeningStoryBoard : MonoBehaviour
	{
		private CameraFilterPack_Atmosphere_Rain_Pro _cameraFilterPackAtmosphereRainPro;

		private float _rainIntensity = 0.66f;

		public Text text;

		public AudioSource audioSource;

		public List<AudioClip> audios;

		[Header("分镜GameObject")]
		public List<GameObject> storyBoards;

		[Header("分镜一")]
		public Image sb1Window;

		public Image sb1Flickering;

		public Image sb1BlackImage;

		private float _sb1FlickeringTime = 0.5f;

		private Camera _mainCamera;

		[Header("分镜二")]
		public RectTransform _sb2WayPoint;

		public Image _sb2Cars;

		public Image _sb2light;

		public Image sb2BlackImage;

		[Header("分镜三")]
		public Image sb3BlackImage;

		public FrameAnimation2D sb3FrameAnimation;

		private int _sb3Times;

		[Header("分镜四")]
		public FrameAnimation2D sb4FrameAnimation;

		public Image sb4BlackImage;

		[Header("分镜五")]
		public Image sb5BlackImage;

		public Image sb5BG;

		public Image sb5Car1;

		public Image sb5Car2;

		public List<GameObject> sb5BirdGroups;

		public float sb5IntervalTime = 0.1f;

		[Header("分镜六")]
		public FrameAnimation2D sb6DoctorAnimation;

		public FrameAnimation2D sb6VanAnimation;

		public Image sb6BlackImage;

		[Header("分镜七")]
		public FrameAnimation2D sb7DoctorAnimation;

		public FrameAnimation2D sb7VanAnimation;

		[Header("分镜八")]
		public FrameAnimation2D sb8DoctorAnimation;

		[Header("分镜九")]
		public FrameAnimation2D sb9VanAnimation;

		public Transform sb9TreeLeft;

		public Transform sb9TreeCenter;

		public Transform sb9TreeRight;

		public Transform sb9Road;

		public Transform sb9Hill1;

		public Transform sb9Hill2;

		public Transform sb9FarTree;

		public Transform sb9Car;

		public Transform sb9LeftHillTree;

		public Image lightImage;

		public Image sb9BlackImage;

		private UnityAction _finishedCallback;

		private GameManager _gameManager;

		public GameManager GameManager
		{
			get
			{
				if (_gameManager == null)
				{
					_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
				}
				return _gameManager;
			}
		}

		private void Start()
		{
			GameManager.musicManager.Audiosource.Stop();
			Debug.Log("Opening Audiosource.Stop()");
			Invoke("PlaySound", 1.2f);
			Invoke("First", 1f);
			GameManager.player.playerdata.isShowNote = false;
		}

		public void SetFinishCallback(UnityAction callback)
		{
			_finishedCallback = callback;
		}

		public void SetCamera(Camera camera)
		{
			_mainCamera = camera;
		}

		private void First()
		{
			FrameAnimationEvent.Instance.frameFinished += FrameFinished;
			GameManager.musicManager.PlayMusicLoop(30);
			StartCoroutine(StoryBoard1Black());
			InvokeRepeating("SmokeFlickering", 0f, _sb1FlickeringTime);
		}

		private void PlaySound()
		{
			audioSource.PlayOneShot(audios[0]);
		}

		private IEnumerator StoryBoard1Black()
		{
			audioSource.PlayOneShot(audios[2]);
			yield return new WaitForSeconds(2f);
			if (_mainCamera != null)
			{
				_cameraFilterPackAtmosphereRainPro = _mainCamera.gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain_Pro>();
				_cameraFilterPackAtmosphereRainPro.DropOnOff = 0.163f;
				_cameraFilterPackAtmosphereRainPro.Distortion = 0f;
				_cameraFilterPackAtmosphereRainPro.Speed = 0.483f;
				_cameraFilterPackAtmosphereRainPro.Size = 1.15f;
				_cameraFilterPackAtmosphereRainPro.DirectionX = 0.133f;
				_cameraFilterPackAtmosphereRainPro.Intensity = _rainIntensity;
				_cameraFilterPackAtmosphereRainPro.Fade = 0f;
				DOTween.To(() => _cameraFilterPackAtmosphereRainPro.Fade, delegate(float x)
				{
					_cameraFilterPackAtmosphereRainPro.Fade = x;
				}, 0.369f, 3f).SetEase(Ease.Linear);
			}
			float actionTime1 = 3f;
			Color black = Color.black;
			black.a = 0.4f;
			sb1BlackImage.DOColor(black, actionTime1).SetEase(Ease.Linear);
			yield return new WaitForSeconds(actionTime1 / 2f);
			audioSource.PlayOneShot(audios[3]);
			yield return new WaitForSeconds(actionTime1 / 2f);
			sb1BlackImage.color = Color.white;
			yield return new WaitForSeconds(0.1f);
			float num = 0.3f;
			Color white = Color.white;
			white.a = 0f;
			sb1BlackImage.DOColor(white, num);
			yield return new WaitForSeconds(num);
			sb1Window.GetComponent<RectTransform>().DOAnchorPosY(-393f, 3f).SetEase(Ease.Linear);
			audioSource.PlayOneShot(audios[1]);
			yield return new WaitForSeconds(3.8f);
			sb1BlackImage.color = Color.black;
			sb1BlackImage.DOFade(1f, 0.2f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.2f);
			StoryBoard2();
		}

		private void SmokeFlickering()
		{
			RandomChangeAlpha(color: new Color(0.5372549f, 0.29803923f, 0.29803923f), image: sb1Flickering, range: new float[2] { 0.3f, 0.99f }, time: _sb1FlickeringTime);
		}

		private void StoryBoard2()
		{
			_cameraFilterPackAtmosphereRainPro.DropOnOff = 1f;
			storyBoards[1].SetActive(value: true);
			storyBoards[0].SetActive(value: false);
			sb2BlackImage.DOFade(0f, 0.5f).SetEase(Ease.Linear);
			_sb2Cars.GetComponent<RectTransform>().DOAnchorPos(_sb2WayPoint.anchoredPosition, 2f).SetEase(Ease.Linear)
				.OnComplete(StoryBoard3);
			InvokeRepeating("lightFlickering", 0f, 0.3f);
		}

		private void lightFlickering()
		{
			RandomChangeAlpha(_sb2light, Color.white, new float[2] { 0.4f, 0.6f }, 0.3f);
		}

		private void StoryBoard3()
		{
			storyBoards[2].SetActive(value: true);
			storyBoards[1].SetActive(value: false);
			sb3FrameAnimation.isFinishedHide = false;
			sb3FrameAnimation.Play();
		}

		private void StoryBoard4()
		{
			storyBoards[3].SetActive(value: true);
			storyBoards[2].SetActive(value: false);
			sb4FrameAnimation.isFinishedHide = false;
			sb4FrameAnimation.Play();
		}

		private void StoryBoard4Black()
		{
			DOTween.To(() => _cameraFilterPackAtmosphereRainPro.Fade, delegate(float x)
			{
				_cameraFilterPackAtmosphereRainPro.Fade = x;
			}, 0f, 0.5f).SetEase(Ease.Linear);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(sb4BlackImage.DOFade(1f, 0.8f).SetEase(Ease.Linear).OnComplete(delegate
			{
			}));
			sequence.Play().OnComplete(delegate
			{
				Debug.Log("场景4结束");
				StoryBoard5();
			});
		}

		private void StoryBoard5()
		{
			storyBoards[4].SetActive(value: true);
			storyBoards[3].SetActive(value: false);
			StartCoroutine("StoryBoard5Coroutine");
		}

		private IEnumerator StoryBoard5Coroutine()
		{
			sb5BlackImage.DOFade(0f, 0.5f).SetEase(Ease.Linear);
			Debug.Log("StoryBoard5Coroutine");
			Ease ease = Ease.Linear;
			RectTransform rectTransform = sb5Car1.GetComponent<RectTransform>();
			DOTween.To(() => _cameraFilterPackAtmosphereRainPro.Fade, delegate(float x)
			{
				_cameraFilterPackAtmosphereRainPro.Fade = x;
			}, 0.33f, 0.5f).SetEase(Ease.Linear);
			rectTransform.DORotate(new Vector3(0f, 0f, -30f), 0.5f).SetEase(ease);
			Vector3[] path = new Vector3[5]
			{
				new Vector3(-492f, 19f),
				new Vector3(-299f, -53f),
				new Vector3(-154f, -142f),
				new Vector3(0f, -276f),
				new Vector3(105f, -528f)
			};
			rectTransform.DOLocalPath(path, 1f, PathType.CatmullRom, PathMode.Sidescroller2D).SetEase(ease);
			yield return new WaitForSeconds(1.5f);
			sb5Car1.DOFade(0f, 0f);
			rectTransform.DORotate(Vector3.zero, 0f);
			sb5Car2.DOFade(1f, 0f);
			WaitForSeconds waitForSeconds = new WaitForSeconds(sb5IntervalTime);
			for (int i = 0; i < sb5BirdGroups.Count; i++)
			{
				yield return waitForSeconds;
				if (i > 0)
				{
					sb5BirdGroups[i - 1].SetActive(value: false);
				}
				sb5BirdGroups[i].SetActive(value: true);
			}
			yield return waitForSeconds;
			sb5BirdGroups[sb5BirdGroups.Count - 1].SetActive(value: false);
			yield return new WaitForSeconds(0.8f);
			sb5BlackImage.DOFade(1f, 0.2f).SetEase(Ease.Linear);
			DOTween.To(() => _cameraFilterPackAtmosphereRainPro.Fade, delegate(float x)
			{
				_cameraFilterPackAtmosphereRainPro.Fade = x;
			}, 0f, 0.2f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(3.8f);
			StoryBoard6();
		}

		private void StoryBoard6()
		{
			_cameraFilterPackAtmosphereRainPro.DropOnOff = 1f;
			storyBoards[4].SetActive(value: false);
			storyBoards[5].SetActive(value: true);
			StartCoroutine("StoryBoard6Coroutine");
		}

		private IEnumerator StoryBoard6Coroutine()
		{
			sb6BlackImage.DOFade(0f, 0.5f).SetEase(Ease.Linear);
			DOTween.To(() => _cameraFilterPackAtmosphereRainPro.Fade, delegate(float x)
			{
				_cameraFilterPackAtmosphereRainPro.Fade = x;
			}, 0.2f, 0.5f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.4f);
			sb6VanAnimation.Play();
			sb6VanAnimation.isFinishedHide = false;
			yield return new WaitForSeconds(2f);
			sb6DoctorAnimation.isFinishedHide = false;
			sb6DoctorAnimation.Play();
		}

		private void StoryBoard7()
		{
			float duration = GameManager.soundManager.PlayEventFinished("110008", 0);
			text.DOText(I18N.instance.getValue("^110008_common_82"), duration).SetEase(Ease.Linear);
			storyBoards[5].SetActive(value: false);
			storyBoards[6].SetActive(value: true);
			sb7DoctorAnimation.isFinishedHide = false;
			sb7DoctorAnimation.Play();
			sb7VanAnimation.gameObject.transform.DOLocalMoveX(58f, 3f).OnComplete(delegate
			{
				Invoke("StoryBoard8", 2f);
			}).SetEase(Ease.Linear);
			sb7VanAnimation.Play();
		}

		private void StoryBoard8()
		{
			storyBoards[6].SetActive(value: false);
			storyBoards[7].SetActive(value: true);
			sb8DoctorAnimation.isFinishedHide = false;
			sb8DoctorAnimation.Play();
		}

		private void StoryBoard9()
		{
			text.text = "";
			storyBoards[7].SetActive(value: false);
			storyBoards[8].SetActive(value: true);
			sb9TreeCenter.localPosition = new Vector2(sb9TreeCenter.localPosition.x + 180f, sb9TreeCenter.localPosition.y);
			StartCoroutine("StoryBoard9Coroutine");
		}

		private IEnumerator StoryBoard9Coroutine()
		{
			sb9VanAnimation.isFinishedHide = false;
			sb9VanAnimation.Play();
			float duration = 5f;
			sb9TreeLeft.DOScale(0.75f, duration);
			sb9TreeCenter.DOScale(0.75f, duration);
			sb9TreeRight.DOScale(1.25f, duration);
			Vector3 endValue = new Vector2(sb9TreeCenter.localPosition.x - 180f, sb9TreeCenter.localPosition.y);
			DOTween.To(() => sb9TreeCenter.localPosition, delegate(Vector3 x)
			{
				sb9TreeCenter.localPosition = x;
			}, endValue, duration);
			Vector3 endValue2 = new Vector2(sb9TreeLeft.localPosition.x - 40f, sb9TreeLeft.localPosition.y);
			DOTween.To(() => sb9TreeLeft.localPosition, delegate(Vector3 x)
			{
				sb9TreeLeft.localPosition = x;
			}, endValue2, duration);
			sb9Road.DOScale(1.25f, duration).SetEase(Ease.Linear);
			sb9Road.DORotate(new Vector3(0f, 0f, -9f), duration).SetEase(Ease.Linear);
			sb9Hill1.DOScale(1f, duration).SetEase(Ease.Linear);
			sb9Hill2.DOScale(1f, duration).SetEase(Ease.Linear);
			sb9FarTree.DOScale(1f, duration).SetEase(Ease.Linear);
			sb9Car.DOScale(1f, duration).SetEase(Ease.Linear);
			sb9LeftHillTree.DOScale(1f, duration).SetEase(Ease.Linear);
			RandomChangeAlpha(lightImage, Color.white, new float[2] { 0.5f, 1f }, 0.2f);
			RectTransform vanRT = sb9VanAnimation.GetComponent<RectTransform>();
			Vector3 endValue3 = new Vector2(vanRT.localPosition.x + 480f, vanRT.localPosition.y - 800f);
			DOTween.To(() => vanRT.localPosition, delegate(Vector3 x)
			{
				vanRT.localPosition = x;
			}, endValue3, duration).SetEase(Ease.Linear);
			yield return new WaitForSeconds(4f);
			DOTween.To(() => _cameraFilterPackAtmosphereRainPro.Fade, delegate(float x)
			{
				_cameraFilterPackAtmosphereRainPro.Fade = x;
			}, 0f, 1.5f).SetEase(Ease.Linear);
			sb9BlackImage.DOFade(1f, 4f).OnComplete(delegate
			{
				Invoke("Finish", 2f);
				Debug.Log("黑了");
			});
		}

		private void Finish()
		{
			Debug.Log("finish");
			_finishedCallback();
			Object.Destroy(base.gameObject);
		}

		private void RandomChangeAlpha(Image image, Color color, float[] range, float time)
		{
			float a = Random.Range(range[0], range[1]);
			color.a = a;
			image.DOColor(color, time).SetEase(Ease.Linear);
		}

		private void FrameFinished(string keyName, int frame, int maxCount)
		{
			if (keyName == "sb3" && frame == sb3FrameAnimation.frameSprites.Count - 1)
			{
				_sb3Times++;
				if (_sb3Times > 3)
				{
					StoryBoard4();
				}
			}
			if (keyName == "sb4" && frame == sb4FrameAnimation.frameSprites.Count - 2)
			{
				StoryBoard4Black();
			}
			if (keyName == "sb6_van" && frame == sb6VanAnimation.frameSprites.Count - 1)
			{
				Invoke("StoryBoard7", 1.5f);
			}
			if (keyName == "sb8_doctor" && frame == sb8DoctorAnimation.frameSprites.Count - 1)
			{
				Invoke("StoryBoard9", 1f);
			}
		}

		private void OnDestroy()
		{
			GameManager component = GameObject.Find("GameManager").GetComponent<GameManager>();
			component.player.playerdata.getMask = true;
			component.saveManager.SavePlayerData(isshowlogo: true, isForce: true);
			FrameAnimationEvent.Instance.frameFinished -= FrameFinished;
			Object.Destroy(_cameraFilterPackAtmosphereRainPro);
		}
	}
}
