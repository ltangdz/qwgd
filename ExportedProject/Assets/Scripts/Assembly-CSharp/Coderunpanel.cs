using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Coderunpanel : MonoBehaviour
{
	[SerializeField]
	private ScrollRect scroll;

	[SerializeField]
	private Transform runContent;

	[SerializeField]
	private Text ruqinTxt;

	[SerializeField]
	private bool complete;

	private int codeLength;

	private string[] codeList = new string[97]
	{
		"admin@wks05:~$ grep root etc/crypto", "grep: /etc/crypto: Permission Denied", "admin@wks05:~$", "admin@wks05:~$ grep root etc/crypto", "pico ablkcipher.c", "static const char* skcipher_default_geniv __read_mostly;", "struct ablkcipher_buffer", "{", "struct list_head        entry;", "struct scatter_walk        dst;",
		"unsigned int len;", "void* data;", "};", "enum {", "ABLKCIPHER_WALK_SLOW = 1 & lt;&lt; 0,", "};", "static inline void ablkcipher_buffer_write(struct ablkcipher_buffer *p)", "{", "scatterwalk_copychunks(p-&gt; data, &amp;p-&gt;dst, p-&gt;len, 1);", "}",
		"void __ablkcipher_walk_complete(struct ablkcipher_walk * walk)", "{", "struct ablkcipher_buffer * p, * tmp;", "list_for_each_entry_safe(p, tmp, &amp; walk-&gt;buffers, entry) {", "ablkcipher_buffer_write(p);", "list_del(&amp; p-&gt;entry);", "kfree(p);", "}", "}", "EXPORT_SYMBOL_GPL(__ablkcipher_walk_complete);",
		"static inline void ablkcipher_queue_write(struct ablkcipher_walk * walk,", "struct ablkcipher_buffer * p)", "{", "        p-&gt;dst = walk-&gt;out;", "list_add_tail(&amp; p-&gt;entry, &amp;walk-&gt;buffers);", "}", "static inline u8 * ablkcipher_get_spot(u8* start, unsigned int len)", "{", "u8* end_page = (u8*)(((unsigned long)(start + len - 1)) &amp; PAGE_MASK);", "return max(start, end_page);",
		"}", "static inline unsigned int ablkcipher_done_slow(struct ablkcipher_walk * walk,", "unsigned int bsize)", "{", "unsigned int n = bsize;", "for (;;) {", "unsigned int len_this_page = scatterwalk_pagelen(&amp; walk-&gt;out);", "if (len_this_page &gt; n)", "len_this_page = n;", "scatterwalk_advance(&amp; walk-&gt;out, n);",
		"if (n == len_this_page)", "break;", "n -= len_this_page;", "scatterwalk_start(&amp; walk-&gt;out, scatterwalk_sg_next(walk-&gt;out.sg));", "}", "return bsize;", "}", "static inline unsigned int ablkcipher_done_fast(struct ablkcipher_walk * walk,", "unsigned int n)", "{",
		"scatterwalk_advance(&amp; walk-&gt;in, n);", "scatterwalk_advance(&amp; walk-&gt;out, n);", "return n;", "}", "static int ablkcipher_walk_next(struct ablkcipher_request * req,", "struct ablkcipher_walk * walk);", "int ablkcipher_walk_done(struct ablkcipher_request * req,", "struct ablkcipher_walk * walk, int err)", "{", "struct crypto_tfm * tfm = req - &gt;base.tfm;",
		"unsigned int nbytes = 0;", "if (likely(err &gt;= 0)) {", "unsigned int n = walk - &gt; nbytes - err;", "if (likely(!(walk-&gt;flags &amp; ABLKCIPHER_WALK_SLOW)))", "n = ablkcipher_done_fast(walk, n);", "else if (WARN_ON(err)) {", "err = -EINVAL;", "goto err;", "} else", "n = ablkcipher_done_slow(walk, n);",
		"nbytes = walk-&gt;total - n;", "err = 0;", "}", "scatterwalk_done(&amp; walk-&gt;in, 0, nbytes);", "scatterwalk_done(&amp; walk-&gt;out, 1, nbytes);", "err:", "walk-&gt;total = nbytes;", "walk-&gt;nbytes = nbytes;", "if (nbytes) {", "crypto_yield(req-&gt;base.flags);",
		"return ablkcipher_walk_next(req, walk);", "}", "if (walk-&gt;iv != req-&gt;info)", "memcpy(req-&gt; info, walk-&gt;iv, tfm-&gt;crt_ablkcipher.ivsize);", "kfree(walk-&gt; iv_buffer);", "return err;", "}"
	};

	private Coroutine run;

	private void Start()
	{
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
		run = StartCoroutine(CodeRun(99999f, "success"));
	}

	public void Run(float codeNum, string lastLabel)
	{
		if (run != null)
		{
			StopCoroutine(run);
		}
		run = StartCoroutine(CodeRun(codeNum, lastLabel));
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
}
