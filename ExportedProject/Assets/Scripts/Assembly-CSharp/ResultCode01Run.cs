using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCode01Run : MonoBehaviour
{
	public List<Text> code;

	private string[] codeList = new string[91]
	{
		"struct ablkcipher_buffer", "{", "struct list_head        entry;", "struct scatter_walk        dst;", "unsigned int len;", "void* data;", "};", "enum {", "ABLKCIPHER_WALK_SLOW = 1 & lt;&lt; 0,", "};",
		"static inline void ablkcipher_buffer_write(struct ablkcipher_buffer *p)", "{", "scatterwalk_copychunks(p-&gt; data, &amp;p-&gt;dst, p-&gt;len, 1);", "}", "void __ablkcipher_walk_complete(struct ablkcipher_walk * walk)", "{", "struct ablkcipher_buffer * p, * tmp;", "list_for_each_entry_safe(p, tmp, &amp; walk-&gt;buffers, entry) {", "ablkcipher_buffer_write(p);", "list_del(&amp; p-&gt;entry);",
		"kfree(p);", "}", "}", "EXPORT_SYMBOL_GPL(__ablkcipher_walk_complete);", "static inline void ablkcipher_queue_write(struct ablkcipher_walk * walk,", "struct ablkcipher_buffer * p)", "{", "        p-&gt;dst = walk-&gt;out;", "list_add_tail(&amp; p-&gt;entry, &amp;walk-&gt;buffers);", "}",
		"static inline u8 * ablkcipher_get_spot(u8* start, unsigned int len)", "{", "u8* end_page = (u8*)(((unsigned long)(start + len - 1)) &amp; PAGE_MASK);", "return max(start, end_page);", "}", "static inline unsigned int ablkcipher_done_slow(struct ablkcipher_walk * walk,", "unsigned int bsize)", "{", "unsigned int n = bsize;", "for (;;) {",
		"unsigned int len_this_page = scatterwalk_pagelen(&amp; walk-&gt;out);", "if (len_this_page &gt; n)", "len_this_page = n;", "scatterwalk_advance(&amp; walk-&gt;out, n);", "if (n == len_this_page)", "break;", "n -= len_this_page;", "scatterwalk_start(&amp; walk-&gt;out, scatterwalk_sg_next(walk-&gt;out.sg));", "}", "return bsize;",
		"}", "static inline unsigned int ablkcipher_done_fast(struct ablkcipher_walk * walk,", "unsigned int n)", "{", "scatterwalk_advance(&amp; walk-&gt;in, n);", "scatterwalk_advance(&amp; walk-&gt;out, n);", "return n;", "}", "static int ablkcipher_walk_next(struct ablkcipher_request * req,", "struct ablkcipher_walk * walk);",
		"int ablkcipher_walk_done(struct ablkcipher_request * req,", "struct ablkcipher_walk * walk, int err)", "{", "struct crypto_tfm * tfm = req - &gt;base.tfm;", "unsigned int nbytes = 0;", "if (likely(err &gt;= 0)) {", "unsigned int n = walk - &gt; nbytes - err;", "if (likely(!(walk-&gt;flags &amp; ABLKCIPHER_WALK_SLOW)))", "n = ablkcipher_done_fast(walk, n);", "else if (WARN_ON(err)) {",
		"err = -EINVAL;", "goto err;", "} else", "n = ablkcipher_done_slow(walk, n);", "nbytes = walk-&gt;total - n;", "err = 0;", "}", "scatterwalk_done(&amp; walk-&gt;in, 0, nbytes);", "scatterwalk_done(&amp; walk-&gt;out, 1, nbytes);", "err:",
		"walk-&gt;total = nbytes;", "walk-&gt;nbytes = nbytes;", "if (nbytes) {", "crypto_yield(req-&gt;base.flags);", "return ablkcipher_walk_next(req, walk);", "}", "if (walk-&gt;iv != req-&gt;info)", "memcpy(req-&gt; info, walk-&gt;iv, tfm-&gt;crt_ablkcipher.ivsize);", "kfree(walk-&gt; iv_buffer);", "return err;",
		"}"
	};

	private void Start()
	{
		int num = 0;
		float num2 = 0f;
		for (int i = 0; i < code.Count; i++)
		{
			int num3 = Random.Range(1, 20);
			float num4 = (float)Random.Range(4, 8) * 0.1f;
			num = ((num == num3) ? (num3 + Random.Range(1, 3)) : num3);
			num2 = ((num2 == num4) ? (num4 + 0.1f) : num4);
			StartCoroutine(Run(code[i], num, num2));
		}
	}

	private IEnumerator Run(Text txt, int i, float time)
	{
		int a = i;
		while (true)
		{
			if (a >= codeList.Length)
			{
				a = 0;
			}
			txt.GetComponent<TypewriterEffect>().StartEffect(codeList[a]);
			a++;
			if (a >= codeList.Length)
			{
				a = 0;
			}
			yield return new WaitForSeconds(time);
			txt.text = "";
		}
	}
}
