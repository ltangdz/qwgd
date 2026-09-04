using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class MissionItem : MonoBehaviour
{
	public string missionid;

	public Text txt_content;

	public Text txt_status;

	public Image img_status;

	public Image img_dot;

	public Sprite[] sprites;

	public Color[] colors;

	private GameManager gameManager;

	public DATA20 date20;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void SetInitContent(DATA20 date20)
	{
		this.date20 = date20;
		missionid = date20.ID.ToString();
		bool flag = (gameManager.player.playerdata.GetMissionItemStatus(missionid).Equals("1") ? true : false);
		txt_content.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue(date20.title) + ":" + (flag ? I18N.instance.getValue(date20.content) : I18N.instance.getValue("^mission_content10")));
		txt_content.color = (flag ? colors[1] : colors[0]);
		img_status.sprite = (flag ? sprites[1] : sprites[0]);
		img_dot.sprite = (flag ? sprites[3] : sprites[2]);
		txt_status.GetComponent<I18NText>().updateTranslation2(flag ? "Done" : "None");
	}

	public void CompeleteMission()
	{
		StartCoroutine(StartAnimation());
	}

	private IEnumerator StartAnimation()
	{
		bool flag = true;
		txt_content.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue(date20.title) + ":" + (flag ? I18N.instance.getValue(date20.content) : I18N.instance.getValue("^mission_content10")));
		yield return new WaitForSeconds(0.3f);
		txt_content.color = colors[1];
		img_dot.sprite = sprites[3];
		yield return new WaitForSeconds(0.3f);
		txt_content.color = colors[0];
		img_dot.sprite = sprites[2];
		yield return new WaitForSeconds(0.3f);
		txt_content.color = colors[1];
		img_dot.sprite = sprites[3];
		yield return new WaitForSeconds(0.3f);
		txt_content.color = colors[0];
		img_dot.sprite = sprites[2];
		yield return new WaitForSeconds(0.3f);
		txt_content.color = colors[1];
		img_dot.sprite = sprites[3];
		yield return new WaitForSeconds(0.2f);
		img_status.sprite = sprites[1];
		txt_status.GetComponent<I18NText>().updateTranslation2("Done");
		yield return new WaitForSeconds(0.3f);
		txt_status.color = colors[2];
		yield return new WaitForSeconds(0.2f);
		txt_status.color = colors[0];
		yield return new WaitForSeconds(0.2f);
		txt_status.color = colors[2];
	}

	private void Update()
	{
	}
}
