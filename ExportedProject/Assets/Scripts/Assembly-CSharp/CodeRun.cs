using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CodeRun : MonoBehaviour
{
	public GameObject content;

	private string[] codeList = new string[36]
	{
		"using System.Collections;using System.Collections;using System.Collections;", "using System.Collections.Generic;using System.Collections.Generic", "using UnityEngine;using System.Collections.Generic;", "using UnityEngine.UI;using System.Collections.Generic;", "using DG.Tweening;using UnityEngine.UI", "using Honeti;", "public class BrowserDialog : CustomDialog", "{", "    public Transform contentPanel;", "    public GameObject homepanel;",
		"    public GameObject searchPanel;", "    public GameObject currentPanel;", "    public Image webLoadLine;public Text textHttp;public Text textHttp;public Text textHttp;", "    public Button btn_favourite;public Text textHttp;public Text textHttp;public Text textHttp;", "    public GameObject favouritePanel;", "    public float maxWidth;", "    public GameObject loadScene;public Text textHttp;", "    public Text textHttp;public Text textHttp;", "    private bool hasAdmin = false;", "   void Start()",
		"    {", "        textHttp.text = \"www.gogo.com\";textHttp.text = \"www.gogo.com\";textHttp.text = \"www.gogo.com\";", "        homepanel.GetComponent<Link>().webLink = \"www.gogo.com\";textHttp.text = \"www.gogo.com\";", "        gameManager.browserDialog = this;", "        btn_favourite.onClick.AddListener(delegate ()", "        textHttp.text = \"www.gogo.com/favorites\";", "        OpenPanel(\"favouritePanel\", \"www.gogo.com/favorites\");", "    }", "    public void FirstBrowserShow()", "    homepanel.GetComponent<HomeBrowser>().coursePanel.gameObject.SetActive(true);",
		"    homepanel.GetComponent<HomeBrowser>().coursePanel.ShowCourse();", "    public void FirstBrowserShow2()", "    {", "        searchPanel.GetComponent<SearchBrowser>().coursePanel.gameObject.SetActive(true);", "        searchPanel.GetComponent<SearchBrowser>().coursePanel.ShowCourse();", "    }"
	};

	public void StartRun()
	{
		StartCoroutine(CodeStartRun());
	}

	public void StopRun()
	{
		StopAllCoroutines();
	}

	private IEnumerator CodeStartRun()
	{
		int listIndex = 0;
		while (true)
		{
			Object.Instantiate(Resources.Load<Text>("Dialog/code"), content.transform).GetComponent<TypewriterEffect>().StartEffect(codeList[listIndex]);
			listIndex++;
			LineToBottom();
			if (content.transform.childCount >= 20)
			{
				Object.Destroy(content.transform.GetChild(0).gameObject);
			}
			if (listIndex >= codeList.Length - 1)
			{
				listIndex = 0;
			}
			yield return new WaitForSeconds(0.3f);
		}
	}

	public void LineToBottom()
	{
		Canvas.ForceUpdateCanvases();
		GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}
}
