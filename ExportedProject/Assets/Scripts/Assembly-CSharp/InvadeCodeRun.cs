using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeCodeRun : MonoBehaviour
{
	public GameObject serverBox;

	public GameObject succBox;

	public GameObject whileTrue;

	public Text serverName;

	public Image logo;

	public Text ruqinTxt;

	public Transform runContent;

	public ScrollRect scroll;

	public Text invadeTip;

	private string serverID;

	private GameManager gameManager;

	private InvadeDialog parObj;

	private string[] codeList = new string[70]
	{
		"int ablkcipher_walk_phys(struct ablkcipher_request *req,", "struct ablkcipher_walk *walk)", "{", "walk-&gt;blocksize = crypto_tfm_alg_blocksize(req-&gt;base.tfm);", "return ablkcipher_walk_first(req, walk);", "}", "EXPORT_SYMBOL_GPL(ablkcipher_walk_phys);", "static int setkey_unaligned(struct crypto_ablkcipher * tfm, const u8* key,", "                                unsigned int keylen)", "{",
		"        struct ablkcipher_alg * cipher = crypto_ablkcipher_alg(tfm);", "unsigned long alignmask = crypto_ablkcipher_alignmask(tfm);", "int ret;", "u8* buffer, * alignbuffer;", "unsigned long absize;", "absize = keylen + alignmask;", "        buffer = kmalloc(absize, GFP_ATOMIC);", "        if (!buffer)", "                return -ENOMEM;", "        alignbuffer = (u8*) ALIGN((unsigned long)buffer, alignmask + 1);",
		"        memcpy(alignbuffer, key, keylen);", "ret = cipher-&gt;setkey(tfm, alignbuffer, keylen);", "memset(alignbuffer, 0, keylen);", "kfree(buffer);", "        return ret;", "}", "static int setkey(struct crypto_ablkcipher * tfm, const u8* key,", "                    unsigned int keylen)", "{", "        struct ablkcipher_alg * cipher = crypto_ablkcipher_alg(tfm);",
		"unsigned long alignmask = crypto_ablkcipher_alignmask(tfm);", "        if (keylen &lt; cipher-&gt;min_keysize | keylen &gt; cipher-&gt;max_keysize) {", "                crypto_ablkcipher_set_flags(tfm, CRYPTO_TFM_RES_BAD_KEY_LEN);", "                return -EINVAL;", "        }", "        if ((unsigned long)key &amp; alignmask)", "                return setkey_unaligned(tfm, key, keylen);", "        return cipher-&gt;setkey(tfm, key, keylen);", "}", "static unsigned int crypto_ablkcipher_ctxsize(struct crypto_alg * alg, u32 type,",
		"                                                u32 mask)", "{", "        return alg-&gt;cra_ctxsize;", "}", "int skcipher_null_givencrypt(struct skcipher_givcrypt_request * req)", "{", "        return crypto_ablkcipher_encrypt(&amp; req-&gt;creq);", "}", "int skcipher_null_givdecrypt(struct skcipher_givcrypt_request * req)", "{",
		"        return crypto_ablkcipher_decrypt(&amp; req-&gt;creq);", "}", "static int crypto_init_ablkcipher_ops(struct crypto_tfm * tfm, u32 type,", "                                        u32 mask)", "{", "        struct ablkcipher_alg * alg = &amp; tfm-&gt;__crt_alg-&gt;cra_ablkcipher;", "        struct ablkcipher_tfm * crt = &amp; tfm-&gt;crt_ablkcipher;", "        if (alg-&gt;ivsize &gt; PAGE_SIZE / 8)", "                return -EINVAL;", "        crt-&gt;setkey = setkey;",
		"        crt-&gt;encrypt = alg-&gt;encrypt;", "        crt-&gt;decrypt = alg-&gt;decrypt;", "        if (!alg-&gt;ivsize) {", "                crt-&gt;givencrypt = skcipher_null_givencrypt;", "                crt-&gt;givdecrypt = skcipher_null_givdecrypt;", "        }", "        crt-&gt;base = __crypto_ablkcipher_cast(tfm);", "crt-&gt;ivsize = alg-&gt;ivsize;", "        return 0;", "}"
	};

	private bool complete;

	private int codeLength;

	public void Init(string id, GameManager gm, InvadeDialog par = null)
	{
		serverID = id;
		gameManager = gm;
		parObj = par;
		Debug.Log("用户ID" + id);
		string sqlname = gameManager.dataManager.dic33[id].sqlname;
		string text = gameManager.dataManager.dic33[id].logo;
		Debug.Log("服务器名称：" + sqlname + "----id:" + id + "----serverLogo:" + text);
		serverName.GetComponent<I18NText>().updateTranslation2(sqlname);
		logo.sprite = Resources.Load<Sprite>("touxiang/" + text);
		logo.GetComponent<RectTransform>().sizeDelta = new Vector2(48f, 48f);
		StartCoroutine(Loading());
	}

	private IEnumerator Loading()
	{
		string a = "";
		while (!complete)
		{
			a += ".";
			a = ((a.Length > 3) ? "" : (a + "."));
			ruqinTxt.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^invade_label22") + a);
			yield return new WaitForSeconds(0.4f);
		}
	}

	public void Complete(bool success = true)
	{
		StopAllCoroutines();
		complete = true;
		if (success)
		{
			ruqinTxt.GetComponent<I18NText>().updateTranslation2("^invade_label23");
		}
		StartCoroutine(CodeRun(99999f, "success"));
	}

	public void Run(float codeNum, string lastLabel)
	{
		StartCoroutine(CodeRun(codeNum, lastLabel));
	}

	private IEnumerator CodeRun(float codeNum, string lastLabel)
	{
		for (int i = 0; (float)i <= codeNum; i++)
		{
			if (runContent.childCount > 35)
			{
				Object.Destroy(runContent.GetChild(0).gameObject);
			}
			GameObject label = Object.Instantiate(Resources.Load<GameObject>("txt_coderun"), runContent);
			LineToBottom(scroll);
			label.GetComponent<Text>().DOText(codeList[codeLength], 0.2f);
			codeLength++;
			if (codeLength >= codeList.Length)
			{
				codeLength = 0;
			}
			yield return new WaitForSeconds(0.2f);
			if ((float)i == codeNum)
			{
				yield return new WaitForSeconds(0.5f);
				Object.Instantiate(Resources.Load<GameObject>("txt_coderun"), runContent);
				LineToBottom(scroll);
				label.GetComponent<Text>().DOText(lastLabel, 0.2f);
			}
		}
	}

	public void LineToBottom(ScrollRect scrollRect)
	{
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}

	public void TaskOver()
	{
		StopAllCoroutines();
	}

	public void HideServerBox()
	{
		serverBox.SetActive(value: false);
		succBox.SetActive(value: true);
	}

	public void ShowWhileTrue(string tip = "")
	{
		serverBox.SetActive(value: false);
		whileTrue.SetActive(value: true);
		if (tip != "")
		{
			invadeTip.GetComponent<I18NText>().updateTranslation2(tip);
		}
	}
}
