using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
	private string[] targetInfoArr;

	public GameObject infoBox;

	private void Start()
	{
		targetInfoArr = new string[1] { "TUK-35468" };
		Text original = Resources.Load("Text", typeof(Text)) as Text;
		for (int i = 0; i < targetInfoArr.Length; i++)
		{
			Object.Instantiate(original, infoBox.transform).GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^target_name") + targetInfoArr[i]);
		}
	}
}
