using UnityEngine;

public class GameMiddle : MonoBehaviour
{
	public GameDialog gameDialog;

	private void Start()
	{
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log("开始接触");
		Debug.Log(collider.name);
		string s = (collider.name = collider.gameObject.GetComponent<GameTextItem>().Stop());
		gameDialog.Add(s);
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		Debug.Log("接触结束");
		Debug.Log(collider.name);
		gameDialog.RemoveS(collider.name, isdestroy: false);
		collider.name = "over";
		collider.gameObject.GetComponent<GameTextItem>().Restart();
	}
}
