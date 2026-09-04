using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class TrackIpItem : MonoBehaviour
{
	public Text txt_ip;

	public Color graycolor;

	public Image img_redline;

	private void Start()
	{
	}

	public void SetContent(string ip)
	{
		txt_ip.GetComponent<I18NText>().updateTranslation2(ip);
	}

	public void MoveOut()
	{
		txt_ip.color = graycolor;
		img_redline.DOFillAmount(1f, 1f);
	}

	private void Update()
	{
	}
}
