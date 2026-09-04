using UnityEngine;

public class Account : MonoBehaviour
{
	private string mailAddress;

	private string mailName;

	public string MailAddress => mailAddress;

	public string MailName => mailName;

	public void Reset(string mail, string name)
	{
		mailAddress = mail;
		mailName = name;
	}
}
