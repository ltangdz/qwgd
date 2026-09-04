using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Event6Video : MonoBehaviour
{
	public Text _text;

	public Image _bg;

	public GameObject blackFloat;

	public GameObject whiteFloat;

	private AudioClip[] sounds;

	private int index;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.PlayMusicLoop(3);
		string[] array = new string[9]
		{
			I18N.instance.getValue("^53FECA71-A93A-1BC8-EDB8-A6223C524DFD"),
			I18N.instance.getValue("^EC5FAA9D-9512-7B84-1864-00B08A6AF8B8"),
			I18N.instance.getValue("^304E320D-7879-D89D-946D-27A92B41F747"),
			I18N.instance.getValue("^61F861D1-FB22-008B-2271-6D061F761DD8"),
			I18N.instance.getValue("^20B16A63-ECFC-5AB8-F10A-313B09FEAF72"),
			I18N.instance.getValue("^E6BDBCBD-8606-6207-A61B-FA4ED2506957"),
			I18N.instance.getValue("^E9AE1BC3-920B-FF3F-D447-C8672AEA135B"),
			I18N.instance.getValue("^7C4371C6-F02F-8B4F-0572-B5D5B0B0EC21"),
			I18N.instance.getValue("^8377D9EB-9F77-C5AB-5637-CAA569E76604")
		};
		bool flag = I18N.instance.gameLang == LanguageCode.EN;
		Sequence sequence = DOTween.Sequence();
		sequence.Append(_text.DOText(array[0], 0f));
		sequence.AppendInterval(2f);
		sequence.Append(_text.DOFade(0f, 1f));
		sounds = new AudioClip[8];
		for (int i = 0; i < sounds.Length; i++)
		{
			sequence.Append(_text.DOFade(1f, 0f));
			string path = "_DLC/sound/player I_" + (i + 1);
			if (flag)
			{
				path = "_DLC/sound/Player I_" + (i + 1) + "_E";
			}
			AudioClip audioClip = (AudioClip)Resources.Load(path);
			float length = audioClip.length;
			sounds[i] = audioClip;
			int i2 = i;
			sequence.Append(_text.DOText(array[i + 1], 0f).OnComplete(delegate
			{
				gameManager.soundManager.audiosource.clip = sounds[i2];
				gameManager.soundManager.audiosource.loop = false;
				gameManager.soundManager.audiosource.Play();
			}));
			sequence.AppendInterval(length);
			sequence.Append(_text.DOFade(0f, 1f));
		}
		sequence.Append(blackFloat.GetComponent<Image>().DOFade(1f, 3f));
		sequence.Play();
	}

	private void FixedUpdate()
	{
		_bg.transform.position = new Vector3(_bg.transform.position.x - 5f * Time.deltaTime, _bg.transform.position.y, _bg.transform.position.z);
	}
}
