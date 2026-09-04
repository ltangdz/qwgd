using UnityEngine;

public class MissionResultValueBox : MonoBehaviour
{
	public GameManager gameManager;

	public JumpNumber txt_hot;

	public JumpNumber txt_good;

	public JumpNumber txt_send;

	public float zhuanfa;

	public float dianzan;

	public float yuedu;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void Init(float time)
	{
		zhuanfa = Zhuanfa(shoujilv(), pingfen(1));
		dianzan = Dianzan(zhuanfa);
		yuedu = Yuedu(dianzan);
		txt_send.StartJump((int)zhuanfa, time);
		txt_good.StartJump((int)dianzan, time);
		txt_hot.StartJump((int)yuedu, time);
	}

	private float shoujilv()
	{
		string eventId = gameManager.player.GetEventId();
		float num = gameManager.dataManager.GetAllItems(eventId).Count;
		return (float)gameManager.player.playerdata.itemlist.Count / num * 100f;
	}

	private float pingfen(int clpType)
	{
		return 0f;
	}

	private float Zhuanfa(float shoujilv, float pingfen)
	{
		int num = Random.Range(95, 106);
		int num2 = int.Parse(gameManager.player.GetEventId()) - 110000;
		return (shoujilv * 0.4f + pingfen * 0.6f) * ((float)num + (float)num2 * 2f);
	}

	private float Dianzan(float zhuanfa)
	{
		int num = Random.Range(95, 106);
		return zhuanfa * (float)num / 100f * 5f;
	}

	private float Yuedu(float dianzan)
	{
		int num = Random.Range(95, 106);
		return dianzan * (float)num / 100f * 6f;
	}
}
