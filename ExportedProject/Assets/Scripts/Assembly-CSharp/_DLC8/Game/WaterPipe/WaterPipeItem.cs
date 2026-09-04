using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.WaterPipe
{
	public class WaterPipeItem : MonoBehaviour
	{
		public List<WaterPipeCollider> colliders;

		public List<Sprite> bgSprites;

		public Image bgImage;

		private Button _clickBtn;

		private WaterPipeManager _manager;

		private Image _frameImage;

		private List<WaterPipeCollider> _enterColliders;

		public List<Image> lineImages;

		private bool _isSuccess;

		public bool isGreenColor;

		private int _index;

		private Color[] _colors = new Color[2]
		{
			Color.white,
			new Color(0.18431373f, 73f / 85f, 0.7607843f, 1f)
		};

		public WaterPipeManager Manager
		{
			get
			{
				return _manager;
			}
			set
			{
				_manager = value;
			}
		}

		private void Awake()
		{
			_enterColliders = new List<WaterPipeCollider>();
			_clickBtn = GetComponent<Button>();
			_frameImage = GetComponent<Image>();
			_clickBtn.onClick.AddListener(ClickRotate);
		}

		public void Show(int index)
		{
			_index = index;
			Rotate();
		}

		private void Rotate()
		{
			if (_index > 3 || _index < 0)
			{
				_index = 0;
			}
			for (int i = 0; i < colliders.Count; i++)
			{
				colliders[i].gameObject.SetActive(value: false);
			}
			base.transform.DOLocalRotate(new Vector3(0f, 0f, _index * 90), 0f, RotateMode.FastBeyond360);
			for (int j = 0; j < colliders.Count; j++)
			{
				colliders[j].gameObject.SetActive(value: true);
			}
		}

		private void ClickRotate()
		{
			if (!_isSuccess)
			{
				_index++;
				Rotate();
				Invoke("RefreshColor", 0.05f);
			}
		}

		private void RefreshColor()
		{
			_manager.RefreshColor();
		}

		public void Enter(WaterPipeCollider waterPipeCollider)
		{
			if (!_enterColliders.Contains(waterPipeCollider))
			{
				_enterColliders.Add(waterPipeCollider);
				if (_enterColliders.Count == colliders.Count && _enterColliders.Count > 0)
				{
					_manager.FinishItem(this);
				}
			}
		}

		public void SetColor(bool isGreen)
		{
			isGreenColor = isGreen;
			if (isGreen)
			{
				for (int i = 0; i < lineImages.Count; i++)
				{
					lineImages[i].color = _colors[1];
				}
				bgImage.sprite = bgSprites[2];
			}
			else
			{
				for (int j = 0; j < lineImages.Count; j++)
				{
					lineImages[j].color = _colors[0];
				}
				bgImage.sprite = bgSprites[0];
			}
		}

		public void Exit(WaterPipeCollider waterPipeCollider)
		{
			_enterColliders.Remove(waterPipeCollider);
			if (_enterColliders.Count != colliders.Count)
			{
				_manager.RemoveItem(this);
			}
		}

		public void Success()
		{
			_isSuccess = true;
		}
	}
}
