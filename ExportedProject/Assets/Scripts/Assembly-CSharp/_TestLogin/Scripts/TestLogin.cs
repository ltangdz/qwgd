using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace _TestLogin.Scripts
{
	public class TestLogin : MonoBehaviour
	{
		[Header("是否测试包 0否 1是")]
		public bool isTest;

		[Header("项目的APPID，联系管理员")]
		public string appId;

		[Header("测试码，联系管理员")]
		public string testCode;

		[Header("页面组件")]
		public Text nameText;

		public InputField pwdInputField;

		public Button loginButton;

		public Image messageLayer;

		public Text messageText;

		public GameManager gameManager;

		private void Awake()
		{
			loginButton.onClick.AddListener(Login);
		}

		private void Start()
		{
			if (GameObject.Find("GameManager").GetComponent<GameManager>().issteam)
			{
				Object.Destroy(base.gameObject);
			}
		}

		private void Login()
		{
			_ = GameObject.Find("GameManager").GetComponent<GameManager>() != null;
			messageLayer.gameObject.SetActive(value: true);
			messageText.text = "网络请求中，请稍后";
			StartCoroutine(PostRequest("https://login.alubastudio.com/index.php/login"));
		}

		private IEnumerator PostRequest(string url)
		{
			string text = nameText.text;
			string text2 = pwdInputField.text;
			WWWForm wWWForm = new WWWForm();
			wWWForm.AddField("username", text);
			wWWForm.AddField("password", text2);
			wWWForm.AddField("uid", appId);
			Debug.Log(text + ":" + text2);
			using (UnityWebRequest webRequest = UnityWebRequest.Post(url, wWWForm))
			{
				messageLayer.gameObject.SetActive(value: false);
				yield return webRequest.SendWebRequest();
				if (!string.IsNullOrEmpty(webRequest.error))
				{
					messageText.text = webRequest.error;
					yield return new WaitForSeconds(3f);
					messageLayer.gameObject.SetActive(value: false);
					Debug.LogError(webRequest.error);
					yield break;
				}
				ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(webRequest.downloadHandler.text);
				if (responseData.code == "0")
				{
					if (responseData.test_code == testCode)
					{
						messageText.text = "登陆成功";
						messageLayer.gameObject.SetActive(value: true);
						yield return new WaitForSeconds(1.5f);
						gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
						gameManager.BeginGame();
						Object.Destroy(base.gameObject);
					}
					else
					{
						messageText.text = "测试码错误，请联系管理员";
						messageLayer.gameObject.SetActive(value: true);
						yield return new WaitForSeconds(1.5f);
						messageLayer.gameObject.SetActive(value: false);
					}
				}
				else
				{
					messageText.text = responseData.message;
					messageLayer.gameObject.SetActive(value: true);
					yield return new WaitForSeconds(3f);
					messageLayer.gameObject.SetActive(value: false);
				}
				Debug.Log(webRequest.downloadHandler.text);
			}
		}
	}
}
