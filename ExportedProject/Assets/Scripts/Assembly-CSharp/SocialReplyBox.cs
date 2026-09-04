using Honeti;
using UnityEngine;

public class SocialReplyBox : MonoBehaviour
{
	public string user;

	public string reply;

	public string content;

	private void Start()
	{
		string key = "<color=#4267B2>" + I18N.instance.getValue(user) + "</color>" + I18N.instance.getValue(reply) + "<color=#4267B2>" + I18N.instance.getValue("^txt_reply") + "</color>" + I18N.instance.getValue(content);
		GetComponent<I18NText>().updateTranslation2(key);
	}
}
