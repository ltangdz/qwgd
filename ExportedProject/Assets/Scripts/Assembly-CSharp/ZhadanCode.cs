using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanCode : MonoBehaviour
{
	public Transform content;

	public ScrollRect scroll;

	public GameObject bk;

	private string[] code = new string[45]
	{
		"EXPORT_SYMBOL_GPL(ablkcipher_walk_phys);", "static int setkey_unaligned(struct crypto_ablkcipher * tfm, const u8* key,", "                                unsigned int keylen)", "        struct ablkcipher_alg * cipher = crypto_ablkcipher_alg(tfm);", "unsigned long alignmask = crypto_ablkcipher_alignmask(tfm);", "int ret;", "u8* buffer, * alignbuffer;", "unsigned long absize;", "absize = keylen + alignmask;", "        buffer = kmalloc(absize, GFP_ATOMIC);",
		"        if (!buffer)", "                return -ENOMEM;", "        alignbuffer = (u8*) ALIGN((unsigned long)buffer, alignmask + 1);", "        memcpy(alignbuffer, key, keylen);", "ret = cipher-&gt;setkey(tfm, alignbuffer, keylen);", "memset(alignbuffer, 0, keylen);", "kfree(buffer);", "        return ret;", "}", "static int setkey(struct crypto_ablkcipher * tfm, const u8* key,",
		"                    unsigned int keylen)", "{", "        struct ablkcipher_alg * cipher = crypto_ablkcipher_alg(tfm);", "unsigned long alignmask = crypto_ablkcipher_alignmask(tfm);", "        if (keylen &lt; cipher-&gt;min_keysize | keylen &gt; cipher-&gt;max_keysize) {", "                crypto_ablkcipher_set_flags(tfm, CRYPTO_TFM_RES_BAD_KEY_LEN);", "                return -EINVAL;", "        }", "        if ((unsigned long)key &amp; alignmask)", "                return setkey_unaligned(tfm, key, keylen);",
		"        return cipher-&gt;setkey(tfm, key, keylen);", "}", "        struct ablkcipher_alg * alg = &amp; tfm-&gt;__crt_alg-&gt;cra_ablkcipher;", "        struct ablkcipher_tfm * crt = &amp; tfm-&gt;crt_ablkcipher;", "        if (alg-&gt;ivsize &gt; PAGE_SIZE / 8)", "                return -EINVAL;", "        crt-&gt;setkey = setkey;", "        crt-&gt;encrypt = alg-&gt;encrypt;", "        crt-&gt;decrypt = alg-&gt;decrypt;", "        if (!alg-&gt;ivsize) {",
		"                crt-&gt;givencrypt = skcipher_null_givencrypt;", "                crt-&gt;givdecrypt = skcipher_null_givdecrypt;", "        }", "        crt-&gt;base = __crypto_ablkcipher_cast(tfm);", "crt-&gt;ivsize = alg-&gt;ivsize;"
	};

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		StartCoroutine(CodeRun());
		gameManager.homeScene.zhadanInvoke.StopInterval();
	}

	private IEnumerator CodeRun()
	{
		bk.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
		yield return new WaitForSeconds(0.3f);
		for (int i = 0; i < code.Length; i++)
		{
			if (content.childCount >= 18)
			{
				Object.Destroy(content.GetChild(0).gameObject);
			}
			Text text = Object.Instantiate(Resources.Load<Text>("Dialog/txt_invadeCodeRun"), content);
			text.fontSize = 16;
			text.DOText(code[i], 0.1f);
			LineToBottom(scroll);
			yield return new WaitForSeconds(0.1f);
		}
		gameManager.homeScene.zhadanInvoke.PojieSuccess();
		gameManager.homeScene.newZhadanDialog.ZhadanSuccess("3300009", isEMP: true);
		gameManager.homeScene.ShowVideoTip("3700065");
		Hide();
	}

	public void Hide()
	{
		bk.transform.DOScale(new Vector3(0f, 0f, 0f), 0.3f);
		Invoke("Des", 0.3f);
	}

	private void Des()
	{
		Object.Destroy(base.gameObject);
	}

	public void LineToBottom(ScrollRect scrollRect)
	{
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}
}
