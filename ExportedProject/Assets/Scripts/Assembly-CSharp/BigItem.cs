using System.Collections.Generic;
using DG.Tweening;
using DLC7.SignalLight;
using DLC7.Titan;
using DLC7.Titan.Voice;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BigItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	public Image img_bk;

	public Image img_frame;

	public Image img_content01;

	public Image img_content02;

	public Image img_icon;

	public Sprite[] sprites;

	public TotalPanel totalPanel;

	public int panelid;

	public TotalPanelDlc7 totalPanelDlc7;

	private List<Color> _lockColorList = new List<Color>
	{
		new Color(13f / 85f, 77f / 85f, 0.59607846f),
		new Color(0.8392157f, 1f / 3f, 24f / 85f)
	};

	private bool _isUnlock;

	private GameManager _gameManager;

	public Text lockText;

	private Transform _parentTransform;

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
		_parentTransform = GetComponentInParent<TiTanDlc7>().transform;
		if (GameManager.Is_Dlc7())
		{
			RefreshData();
		}
	}

	public void RefreshData()
	{
		bool isUnlock = false;
		List<int> list = DocumentLockList();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == panelid)
			{
				isUnlock = true;
			}
		}
		_isUnlock = isUnlock;
		if (panelid > 0)
		{
			lockText.text = I18N.instance.getValue(_isUnlock ? "^110008_game_107" : "^110008_game_106");
			lockText.color = _lockColorList[(!_isUnlock) ? 1 : 0];
		}
	}

	private List<int> DocumentLockList()
	{
		return GameManager.player.playerdata.TitanDocumentUnlock;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (GameManager.IsBasic())
		{
			totalPanel.ShowPanel(panelid);
		}
		else if (!_isUnlock)
		{
			if (DocumentLockList().Count == 0 && panelid > 0)
			{
				Object.Instantiate(Resources.Load<TitanDialog>(DLCNameUtil.Instance.GetTitanTipDialogName()), _parentTransform).InitData("^110008_game_101", null);
			}
			else
			{
				PlayDLC7Game();
			}
		}
		else
		{
			totalPanelDlc7.ShowPanel(panelid);
		}
	}

	public void PlayDLC7Game()
	{
		if (panelid == 0)
		{
			Object.Instantiate(Resources.Load<TitanLightController>("_DLC7/prefabs/TitanSignalGame"), _parentTransform);
			return;
		}
		GameObject gameObject = GetComponentInParent<TotalPanelDlc7>().gameObject;
		Object.Instantiate(Resources.Load<VoicePrintPanelDLC7>("_DLC7/prefabs/VoicePanel"), gameObject.transform.parent).InitData(panelid);
		gameObject.SetActive(value: false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		base.transform.DOKill();
		img_bk.sprite = sprites[3];
		img_frame.sprite = sprites[4];
		img_icon.sprite = sprites[7];
		if (img_content01 != null)
		{
			img_content01.sprite = sprites[5];
		}
		if (img_content02 != null)
		{
			img_content02.sprite = sprites[5];
		}
		base.transform.DOScale(new Vector3(1.01f, 1.01f, 1.01f), 0.2f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		base.transform.DOKill();
		img_bk.sprite = sprites[0];
		img_frame.sprite = sprites[1];
		img_icon.sprite = sprites[6];
		if (img_content01 != null)
		{
			img_content01.sprite = sprites[2];
		}
		if (img_content02 != null)
		{
			img_content02.sprite = sprites[2];
		}
		base.transform.DOScale(Vector3.one, 0.2f);
	}
}
