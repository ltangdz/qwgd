using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SurveillanceDot : MonoBehaviour
{
	public Animator ani_dot;

	public Image img_circle;

	public Image img_dot;

	public Text txt_pos;

	public Text txt_item;

	public Color[] colors;

	public Sprite[] sprites;

	public int status;

	public Animator ani_light0;

	public Animator ani_light1;

	public Button btn_search;

	public int pos;

	public SurveillanceDialog surveillanceDialog;

	public bool iscorrect;

	public string key = "";

	private bool isCanClick = true;

	private void Start()
	{
	}

	public void InitImage(int pos, string key, Vector2 pst)
	{
		this.key = key;
		this.pos = pos;
		img_dot.gameObject.SetActive(value: false);
		img_circle.gameObject.SetActive(value: true);
		img_circle.sprite = sprites[2];
		GetComponent<RectTransform>().localPosition = pst;
		ShowItemContent();
	}

	public void Init(int pos, bool iscorrect, string key, SurveillanceDialog surveillanceDialog, int status = 0)
	{
		this.key = key;
		this.iscorrect = iscorrect;
		this.surveillanceDialog = surveillanceDialog;
		this.pos = pos;
		txt_pos.text = pos.ToString();
		btn_search.onClick.AddListener(delegate
		{
			if (isCanClick)
			{
				isCanClick = false;
				if (status == 0 || status == 1)
				{
					this.surveillanceDialog.ShowSurSearchingDialog(this.pos, this.iscorrect);
				}
			}
			else
			{
				Object.Destroy(btn_search.gameObject);
			}
		});
		if (status == 3)
		{
			ChangeInitDot(isshowtext: true);
		}
	}

	public void StartLight()
	{
		ani_light0.gameObject.SetActive(value: true);
		ani_light1.gameObject.SetActive(value: true);
		StartCoroutine(LightAnimation());
	}

	private IEnumerator LightAnimation()
	{
		ani_light0.Play("ani_surveillancelightbreath");
		yield return new WaitForSeconds(0.8f);
		ani_light1.Play("ani_surveillancelightbreath");
	}

	public void StartBreath()
	{
		if (status == 0)
		{
			ani_dot.transform.parent.SetAsLastSibling();
			ani_dot.transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.2f);
		}
	}

	public void StopBreath()
	{
		if (status == 0)
		{
			ani_dot.transform.DOScale(Vector3.one, 0.2f);
		}
	}

	public void Click()
	{
		if (status == 0 && isCanClick)
		{
			ani_dot.transform.DOScale(Vector3.one, 0.2f);
			img_dot.sprite = sprites[1];
			txt_pos.color = Color.white;
			btn_search.gameObject.SetActive(value: true);
			btn_search.transform.DOScaleX(1f, 0.2f);
			if (surveillanceDialog != null)
			{
				surveillanceDialog.OtherDotCancleClick(pos);
			}
			base.transform.SetAsLastSibling();
			status = 1;
		}
	}

	public void CancelClick()
	{
		if (status == 1)
		{
			ani_dot.transform.DOScale(Vector3.one, 0.2f);
			img_dot.sprite = sprites[0];
			txt_pos.color = colors[0];
			btn_search.transform.DOScaleX(0f, 0.2f).OnComplete(OnComplete);
			status = 0;
		}
	}

	private void OnComplete()
	{
		Object.Destroy(btn_search.gameObject);
		isCanClick = true;
	}

	public void RemoveDot(bool ishide)
	{
		status = 2;
		GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(delegate
		{
			base.gameObject.SetActive(!ishide);
		});
	}

	public void ChangeDot()
	{
		surveillanceDialog.OtherDotRemoveDot(pos);
		img_dot.gameObject.SetActive(value: false);
		img_circle.gameObject.SetActive(value: true);
		if (surveillanceDialog.period == 5)
		{
			img_circle.sprite = sprites[4];
			ani_light0.GetComponent<Image>().sprite = sprites[5];
			ani_light1.GetComponent<Image>().sprite = sprites[5];
			txt_item.color = colors[2];
			btn_search.transform.DOScaleX(0f, 0.2f);
		}
		else
		{
			btn_search.transform.DOScaleX(0f, 0.2f);
		}
		status = 3;
		StartLight();
	}

	public void ShowItemContent()
	{
		txt_item.gameObject.SetActive(value: true);
		txt_item.GetComponent<I18NText>().updateTranslation2(key);
	}

	public void ChangeSureDot(bool isshowtext = false)
	{
		img_circle.sprite = sprites[2];
		ani_light0.gameObject.SetActive(value: false);
		ani_light1.gameObject.SetActive(value: false);
		if (isshowtext)
		{
			txt_item.GetComponent<I18NText>().updateTranslation2(key);
		}
	}

	public void ChangeInitDot(bool isshowtext)
	{
		ani_dot.gameObject.SetActive(value: false);
		img_circle.gameObject.SetActive(value: true);
		ChangeSureDot(isshowtext);
		if (pos == 5)
		{
			img_circle.sprite = sprites[4];
			txt_item.color = colors[2];
		}
	}
}
