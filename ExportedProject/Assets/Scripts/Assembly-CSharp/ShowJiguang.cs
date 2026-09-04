using UnityEngine;

public class ShowJiguang : MonoBehaviour
{
	public void OpenDoor()
	{
		base.transform.GetComponent<Animator>().SetBool("OpenDoor", value: true);
	}
}
