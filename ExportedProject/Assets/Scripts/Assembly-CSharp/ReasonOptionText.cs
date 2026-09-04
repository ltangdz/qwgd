using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ReasonOptionText : QuestionOptionAbstract
{
	public Image _unselectedImage;

	public Image _selectedImage;

	[Header("要有三个图片 0正常 1正确 2失败")]
	public List<Sprite> _selectedImages;

	[Header("要有三个图片 0正常 1正确 2失败")]
	public List<Sprite> _unSelectedImages;

	public Text _nameText;

	private Color _color;

	private void NormalStatus(float alpha)
	{
		Color color = _selectedImage.color;
		color.a = alpha;
		_selectedImage.color = color;
		_selectedImage.sprite = _selectedImages[0];
		_unselectedImage.sprite = _unSelectedImages[0];
		_nameText.DOColor(_color, 0.3f);
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
		_selectedImage.gameObject.SetActive(value: true);
		_color = _nameText.color;
		UnSelectedUI();
		_nameText.text = I18N.instance.getValue(base.CurKey);
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
	}

	protected override void resetUI()
	{
		_selectedImage.sprite = _selectedImages[0];
		_nameText.color = _color;
	}
}
