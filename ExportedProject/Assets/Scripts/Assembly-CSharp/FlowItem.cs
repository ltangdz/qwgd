using UnityEngine;

public class FlowItem : MonoBehaviour
{
	public int id;

	public int type;

	public string content;

	public string arrowid;

	public int belongid;

	public CardTurnOver cardTurnOver;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A) && cardTurnOver != null)
		{
			cardTurnOver.StartBack();
		}
		if (Input.GetKeyDown(KeyCode.Z) && cardTurnOver != null)
		{
			cardTurnOver.StartFront();
		}
	}
}
