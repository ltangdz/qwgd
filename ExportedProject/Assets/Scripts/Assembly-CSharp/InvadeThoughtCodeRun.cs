using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InvadeThoughtCodeRun : MonoBehaviour
{
	public Text txt_title;

	public Transform content;

	public int codeIndex;

	public ScrollRect scroll;

	private string[] code01 = new string[12]
	{
		"<color=#5195ab>struct crypto_tfm *tfm = req-&gt;base.tfm;</color>", "<color=#dcdc97>unsigned int alignmask, bsize, n;</color>", "<color=#dcdc97>void *src, *dst;</color>", "<color=#5b6e8d>int err;</color>", "<color=#5b6e8d>alignmask = crypto_tfm_alg_alignmask(tfm);</color>", "<color=#5b6e8d>n = walk-&gt;total;</color>", "<color=#dcdc97>if (unlikely(n &lt; crypto_tfm_alg_blocksize(tfm))) {</color>", "<color=#dcdc97> req-&gt;base.flags = CRYPTO_TFM_RES_BAD_BLOCK_LEN;</color>", "<color=#dcdc97>return ablkcipher_walk_done(req, walk, -EINVAL);</color>", "<color=#dcdc97>}</color>",
		"<color=#a9bac3>walk-&gt;flags &amp;= ~ABLKCIPHER_WALK_SLOW;</color>", "<color=#a9bac3>return ablkcipher_next_fast(req, walk);</color>"
	};

	private string[] code02 = new string[7] { "<color=#db3232>struct crypto_tfm *tfm = req-&gt;base.tfm;</color>", "walk-&gt;blocksize = crypto_tfm_alg_blocksize(req-&gt;base.tfm);", "crypto_ablkcipher_alignmask(tfm);", "memset(alignbuffer, 0, keylen);", "unsigned long alignmask = crypto_ablkcipher_alignmask(tfm);", "crt-&gt;encrypt = alg-&gt;encrypt;", "<color=#db3232>send mail()=>alg-&gt;cra_ctxsize</color>" };

	private string[] code = new string[0];

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (codeIndex == 0)
		{
			code = code01;
		}
		else
		{
			code = code02;
		}
		StartCoroutine(CodeRun());
	}

	private IEnumerator CodeRun()
	{
		base.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
		yield return new WaitForSeconds(0.3f);
		string value = txt_title.text.ToString();
		GameManager.SetTextWithEllipsis(txt_title, value);
		for (int i = 0; i < code.Length; i++)
		{
			Object.Instantiate(Resources.Load<Text>("Dialog/txt_invadeCodeRun"), content).DOText(code[i], 0.3f);
			LineToBottom(scroll);
			yield return new WaitForSeconds(0.3f);
		}
	}

	public void Hide()
	{
		base.transform.DOScale(new Vector3(0f, 0f, 0f), 0.3f);
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
