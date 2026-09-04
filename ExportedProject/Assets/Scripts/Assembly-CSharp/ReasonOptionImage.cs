using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReasonOptionImage : QuestionOptionAbstract
{
	public Image _unselectedImage;

	public Image _selectedImage;

	[Header("要有三个图片 0正常 1正确 2失败")]
	public List<Sprite> _selectedImages;

	[Header("要有三个图片 0正常 1正确 2失败")]
	public List<Sprite> _unSelectedImages;

	public Image _image;

	public Image _mask;

	private void Start()
	{
		UnSelectedUI();
	}

	protected override void SelectedUI()
	{
		NormalStatus(1f);
	}

	protected override void UnSelectedUI()
	{
		NormalStatus(0f);
	}

	protected override void InitUI()
	{
		_mask.gameObject.SetActive(value: false);
		_selectedImage.gameObject.SetActive(value: true);
		NormalStatus(0f);
		if (!string.IsNullOrEmpty(base.CurKey))
		{
			_image.sprite = Resources.Load<Sprite>(base.CurKey);
		}
	}

	protected override void SuccessUI()
	{
		_unselectedImage.sprite = _unSelectedImages[1];
		_selectedImage.sprite = _selectedImages[1];
	}

	protected override void FailUI()
	{
		_unselectedImage.sprite = _unSelectedImages[2];
		_selectedImage.sprite = _selectedImages[2];
		_mask.gameObject.SetActive(value: true);
	}

	protected override void resetUI()
	{
		_mask.gameObject.SetActive(value: false);
		_selectedImage.sprite = _selectedImages[0];
		_unselectedImage.sprite = _unSelectedImages[0];
	}

	private void NormalStatus(float alpha)
	{
		Color white = Color.white;
		white.a = alpha;
		_selectedImage.color = white;
		_selectedImage.sprite = _selectedImages[0];
		_unselectedImage.sprite = _unSelectedImages[0];
	}
}
