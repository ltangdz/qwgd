using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
	public float charsPerSecond = 0.2f;

	private string words;

	private string prewords;

	public bool isActive;

	private bool isDelete;

	private float timer;

	public Text myText;

	private int currentPos;

	public AudioClip[] audioClips;

	public bool issound;

	public GameManager gameManager;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		timer = 0f;
		charsPerSecond = Mathf.Min(0.2f, charsPerSecond);
		myText = GetComponent<Text>();
		words = myText.text;
		prewords = "";
		myText.text = "";
	}

	private void Update()
	{
		for (int i = 0; i < 2; i++)
		{
			OnStartWriter();
			OnStartDelete();
		}
	}

	public void StartSlowEffect(string content, float charsPerSecond, bool issound, string precontent = "")
	{
		this.issound = issound;
		this.charsPerSecond = charsPerSecond;
		currentPos = 0;
		prewords = precontent;
		words = content;
		isActive = true;
	}

	public void StartEffect(string content, string precontent = "")
	{
		currentPos = 0;
		prewords = precontent;
		words = content;
		isActive = true;
	}

	public void StartDeleteEffect()
	{
		if (!isActive)
		{
			isDelete = true;
		}
	}

	private void OnStartWriter()
	{
		if (!isActive)
		{
			return;
		}
		timer += Time.deltaTime;
		if (!(timer >= charsPerSecond))
		{
			return;
		}
		timer = 0f;
		currentPos++;
		if (words.Length > 0)
		{
			if (currentPos <= words.Length)
			{
				myText.GetComponent<I18NText>().updateTranslation5(prewords + words.Substring(0, currentPos));
			}
			if (issound && audioClips.Length >= 0)
			{
				int num = Random.Range(0, audioClips.Length - 1);
				gameManager.soundManager.PlayAudioClip(audioClips[num]);
			}
			if (currentPos >= words.Length)
			{
				OnFinish();
			}
		}
		else
		{
			OnFinish();
		}
	}

	private void OnStartDelete()
	{
		if (!isDelete)
		{
			return;
		}
		timer += Time.deltaTime;
		if (!(timer >= charsPerSecond))
		{
			return;
		}
		timer = 0f;
		currentPos++;
		if (words.Length > 0)
		{
			myText.GetComponent<I18NText>().updateTranslation5(words.Substring(0, words.Length - currentPos));
			if (currentPos >= words.Length)
			{
				OnDeleteFinish();
			}
		}
		else
		{
			OnDeleteFinish();
		}
	}

	private void OnFinish()
	{
		isActive = false;
		timer = 0f;
		currentPos = 0;
		words = prewords + words;
		myText.GetComponent<I18NText>().updateTranslation5(words);
		if (issound && audioClips.Length != 0)
		{
			StartCoroutine(StartLastAudioClip());
		}
	}

	private IEnumerator StartLastAudioClip()
	{
		yield return new WaitForSeconds(0.3f);
		gameManager.soundManager.PlayAudioClip(audioClips[audioClips.Length - 1]);
	}

	public void Stop()
	{
		isActive = false;
		timer = 0f;
		currentPos = 0;
		myText.text = "";
	}

	private void OnDeleteFinish()
	{
		isDelete = false;
		timer = 0f;
		currentPos = 0;
		words = "";
		myText.GetComponent<I18NText>().updateTranslation5(words);
	}
}
