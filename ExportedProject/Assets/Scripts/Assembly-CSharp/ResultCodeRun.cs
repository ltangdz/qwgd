using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResultCodeRun : MonoBehaviour
{
	public ScrollRect scroll;

	public Transform runContent;

	public Color color = Color.white;

	public int fontSize = 8;

	public float num = 20f;

	public bool isDefault = true;

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

	private string[][] codeList2 = new string[2][]
	{
		new string[35]
		{
			"Load Avg: 4.09, 6.40, 5.46  CPU usage: 22.19% user, 12.17% sys, 65.63% idle", "SharedLibs: 281M resident, 44M data, 40M linkedit.", "MemRegions: 101336 total, 2117M resident, 121M private, 700M shared.", "PhysMem: 8113M used (1768M wired), ", "VM: 3034G vsize, 2305M framework vsize,.", "==> Processing curl-opnsl formula rename to curl", "==> Unlinking curl-opsl", "==> Moving curl-opsl versions to /usr/local/Cellar/curl", "==> Reliing crl", "Networks: packets: 10967237/14G in, 4179316/402M out.",
			"Disks: 5886434/93G read, 1915274/43G written.", "PID    COMMAND      %CPU TIME     #TH   #WQ  #PORT MEM    PURG   CMPRS  PGRP", "22037  photoanalysi 60.2 01:54.73 9/1   ", "145    WindowServer 19.7 28:3    6   ", "0      kernel_task  9.8  41179/4 0   ", "2817   iTerewd23       8.5  01:30.21 12    6    ", "22325  top          4.3  00:   26  5", "2307   gogo Chrom 4.0  05:21.35 15    2  ", "1787   vhvstorm     4.0  42:53.34 50    ", "2574   gogo Chrom 3.1  03:20.94 14    1    ",
			"684    Hitalk           2.5  12:0    65   1212  302   684", "172    coerwaudiod   2.0  00:19.88 6     1    369   504K  172", "833    uu       1.7  07:21.12K  94M-   833", "635    gamecontroll 1.7  01:27.08 4     3    65    1428K  0B     392K   635", "22296  mdworker_sha 1.7  00:00.13 3       0B     22296", "445    photolibrary 1.6  02:29.71 7     6    113   13M+   9252K  4144K  4", "==> Prodfcessing curl-opnsl formula rename to curl", "==> Unlinking curl-opsl", "==> Moving curl-opsl versions to /usr/local/Cellar/curl", "==> Reliing crl",
			"Warning: curl is outdated!", "To avoid broken installations, as soon as possible please run:", "  brew upgrade", "Or, if you're OK with a less reliable fix:", "  brw upgrade curl"
		},
		new string[46]
		{
			"$ which aluba", "/home/aluba/.aluba_studio/bin/aluba", "$ echo $PKG_CONFIG_PATH", "/home/aluba/.aluba_studio/aluba64/pkgconfig:/home/aluba/.aluba_studio/aluba/pkgconfig:/usr/local/aluba/pkgconfig:/usr/local/aluba64/pkgconfig:/usr/aluba64/pkgconfig:/usr/aluba/pkgconfig:/usr/aluba/x86_64-studio-gnu/pkgconfig:/usr/aluba64/pkgconfig:/usr/share/pkgconfig:", "$ aluba install jqd", "==> Downloading http://stedqan.ddfewq.io/jqd/download/source/jqd-1.3.tar.gz", "==> ./configure", "==> make", "/home/aluba/.aluba_studio/Cellar/jqd/x.3: 7 files, 256K, built in 10 seconds", "$ which jq",
			"/home/aluba/.aluba_studio/bin/jqd", "$ jqd --version", "jqd version x.3", "$ aluba search tmlx", "blahtetmlx       alubantmlx   alubatmlx2     tmlx-coreutils   tmlx2        tmlxrpc-c", "html-tmlx-utils  alubawbtmlx  alubatmlxsec1  tmlx-security-c  tmlxcatmgr   tmlxsh", "alubamtmlx         alubatmlx++  tinytmlx     tmlx-tooling-c   tmlxformat   tmlxstarlet", "by_aluba run server", "", "> aluba_v-admin@x.4.3 dev /aluba/AlubaBY",
			"> aluba_v-cli-service serve", "", "INFO  Starting  server...", "10% building 2/3 modules 1 active .../aluba/AlubaBY/src/main", "10% building 4/5 modules 1 active .../aluba/AlubaBY/src/min", "15% building 6/12 modules 6 active .../aluba/AlubaBY/src/permission.", "20% building 32/46 modules 14 active .../aluba/AlubaBY/src/App.", "30% building 34/49 modules 15 active .../aluba/AlubaBY/src/settings.", "40% building 35/49 modules 14 active .../aluba/AlubaBY/src/settings.", "50% building 35/49 modules 14 active .../aluba/AlubaBY/src/settings.",
			"68% building 530/539 modules 9 active .../aluba/AlubaBY/src/user.j", "69% building 530/539 modules 9 active .../aluba/AlubaBY/src/user.j", "72% building 531/539 modules 8 active .../aluba/AlubaBY/src/user.j", "82% building 531/539 modules 8 active .../aluba/AlubaBY/src/user.j", "90% building 531/539 modules 8 active .../aluba/AlubaBY/src/user.j", "98% after emitting Plugin", "", "DONE  Compiled successfully in 1733ms  ", "", "",
			"  App running at:", "  - Local:   http://localhost:19527/", "", "", " Note that the development build is not optimized.", "  To create a production build, run by_aluba run server."
		}
	};

	private int codeLength;

	private void Start()
	{
		if (!isDefault)
		{
			codeList = codeList2[Random.Range(0, 2)];
		}
		StartCoroutine(CodeRun());
	}

	private IEnumerator CodeRun()
	{
		GameObject load = Resources.Load<GameObject>("txt_coderun");
		codeLength = Random.Range(0, codeList.Length);
		while (true)
		{
			GameObject obj = Object.Instantiate(load, runContent);
			Text component = obj.GetComponent<Text>();
			component.color = color;
			component.fontSize = fontSize;
			if ((float)runContent.childCount >= num)
			{
				Object.Destroy(runContent.GetChild(0).gameObject);
			}
			LineToBottom(scroll);
			obj.GetComponent<TypewriterEffect>().StartEffect(codeList[codeLength]);
			codeLength++;
			if (codeLength >= codeList.Length)
			{
				codeLength = 0;
			}
			yield return new WaitForSeconds(0.3f);
		}
	}

	public void LineToBottom(ScrollRect scrollRect)
	{
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}
}
