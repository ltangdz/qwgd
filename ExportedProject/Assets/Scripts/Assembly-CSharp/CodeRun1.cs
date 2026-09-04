using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CodeRun1 : MonoBehaviour
{
	public GameObject content;

	public int _codeType;

	private CatchLoadingStep _step;

	private string[] codeList = new string[35]
	{
		"Load Avg: 4.09, 6.40, 5.46  CPU usage: 22.19% user, 12.17% sys, 65.63% idle", "SharedLibs: 281M resident, 44M data, 40M linkedit.", "MemRegions: 101336 total, 2117M resident, 121M private, 700M shared.", "PhysMem: 8113M used (1768M wired), ", "VM: 3034G vsize, 2305M framework vsize,.", "==> Processing curl-opnsl formula rename to curl", "==> Unlinking curl-opsl", "==> Moving curl-opsl versions to /usr/local/Cellar/curl", "==> Reliing crl", "Networks: packets: 10967237/14G in, 4179316/402M out.",
		"Disks: 5886434/93G read, 1915274/43G written.", "PID    COMMAND      %CPU TIME     #TH   #WQ  #PORT MEM    PURG   CMPRS  PGRP", "22037  photoanalysi 60.2 01:54.73 9/1   ", "145    WindowServer 19.7 28:3    6   ", "0      kernel_task  9.8  41179/4 0   ", "2817   iTerewd23       8.5  01:30.21 12    6    ", "22325  top          4.3  00:   26  5", "2307   gogo Chrom 4.0  05:21.35 15    2  ", "1787   vhvstorm     4.0  42:53.34 50    ", "2574   gogo Chrom 3.1  03:20.94 14    1    ",
		"684    Hitalk           2.5  12:0    65   1212  302   684", "172    coerwaudiod   2.0  00:19.88 6     1    369   504K  172", "833    uu       1.7  07:21.12K  94M-   833", "635    gamecontroll 1.7  01:27.08 4     3    65    1428K  0B     392K   635", "22296  mdworker_sha 1.7  00:00.13 3       0B     22296", "445    photolibrary 1.6  02:29.71 7     6    113   13M+   9252K  4144K  4", "==> Prodfcessing curl-opnsl formula rename to curl", "==> Unlinking curl-opsl", "==> Moving curl-opsl versions to /usr/local/Cellar/curl", "==> Reliing crl",
		"Warning: curl is outdated!", "To avoid broken installations, as soon as possible please run:", "  brew upgrade", "Or, if you're OK with a less reliable fix:", "  brw upgrade curl"
	};

	private string[][] codeList2 = new string[3][]
	{
		new string[17]
		{
			"$ which aluba", "/home/aluba/.aluba_studio/bin/aluba", "$ echo $PKG_CONFIG_PATH", "/home/aluba/.aluba_studio/aluba64/pkgconfig:/home/aluba/.aluba_studio/aluba/pkgconfig:/usr/local/aluba/pkgconfig:/usr/local/aluba64/pkgconfig:/usr/aluba64/pkgconfig:/usr/aluba/pkgconfig:/usr/aluba/x86_64-studio-gnu/pkgconfig:/usr/aluba64/pkgconfig:/usr/share/pkgconfig:", "$ aluba install jqd", "==> Downloading http://stedqan.ddfewq.io/jqd/download/source/jqd-1.3.tar.gz", "==> ./configure", "==> make", "/home/aluba/.aluba_studio/Cellar/jqd/x.3: 7 files, 256K, built in 10 seconds", "$ which jq",
			"/home/aluba/.aluba_studio/bin/jqd", "$ jqd --version", "jqd version x.3", "$ aluba search tmlx", "blahtetmlx       alubantmlx   alubatmlx2     tmlx-coreutils   tmlx2        tmlxrpc-c", "html-tmlx-utils  alubawbtmlx  alubatmlxsec1  tmlx-security-c  tmlxcatmgr   tmlxsh", "alubamtmlx         alubatmlx++  tinytmlx     tmlx-tooling-c   tmlxformat   tmlxstarlet"
		},
		new string[29]
		{
			"by_aluba run server", "", "> aluba_v-admin@x.4.3 dev /aluba/AlubaBY", "> aluba_v-cli-service serve", "", "INFO  Starting  server...", "10% building 2/3 modules 1 active .../aluba/AlubaBY/src/main", "10% building 4/5 modules 1 active .../aluba/AlubaBY/src/min", "15% building 6/12 modules 6 active .../aluba/AlubaBY/src/permission.", "20% building 32/46 modules 14 active .../aluba/AlubaBY/src/App.",
			"30% building 34/49 modules 15 active .../aluba/AlubaBY/src/settings.", "40% building 35/49 modules 14 active .../aluba/AlubaBY/src/settings.", "50% building 35/49 modules 14 active .../aluba/AlubaBY/src/settings.", "68% building 530/539 modules 9 active .../aluba/AlubaBY/src/user.j", "69% building 530/539 modules 9 active .../aluba/AlubaBY/src/user.j", "72% building 531/539 modules 8 active .../aluba/AlubaBY/src/user.j", "82% building 531/539 modules 8 active .../aluba/AlubaBY/src/user.j", "90% building 531/539 modules 8 active .../aluba/AlubaBY/src/user.j", "98% after emitting Plugin", "",
			"DONE  Compiled successfully in 1733ms  ", "", "", "  App running at:", "  - Local:   http://localhost:19527/", "", "", " Note that the development build is not optimized.", "  To create a production build, run by_aluba run server."
		},
		new string[16]
		{
			"", "", "               ___       __       __  __   ____     ___           ", "              /   |     / /      / / / /  / __ )   /   |          ", "             / /| |    / /      / / / /  / __  |  / /| |          ", "            / ___ |   / /___   / /_/ /  / /_/ /  / ___ |          ", "           /_/  |_|  /_____/  /_____/  /_____/  /_/  |_|          ", "", "", "22325  top          4.3  00:   26  5",
			"2307   gogo  4.0  05:21.35 15    2  ", "684    Hitalk           2.5  12:0    65   1212  302   684", "833    uu       1.7  07:21.12K  94M-   833", "", "", "============================> start server...."
		}
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
		bool isContinue = true;
		while (isContinue)
		{
			Text text = Object.Instantiate(Resources.Load<Text>("Dialog/code"), content.transform);
			if (_codeType == 0)
			{
				ColorUtility.TryParseHtmlString("#364f5c", out var color);
				text.color = color;
			}
			else
			{
				text.color = Color.white;
				text.fontSize = 16;
			}
			text.GetComponent<TypewriterEffect>().StartEffect(codeList[listIndex]);
			listIndex++;
			LineToBottom();
			int num = 21;
			if (_codeType == 1)
			{
				num = 11;
			}
			if (content.transform.childCount >= num)
			{
				Object.Destroy(content.transform.GetChild(0).gameObject);
			}
			if (listIndex >= codeList.Length - 1)
			{
				if (_codeType == 0)
				{
					listIndex = 0;
				}
				else
				{
					isContinue = false;
					CatchEvent.Instance.NoticeLoading(_step);
				}
			}
			yield return new WaitForSeconds((_codeType == 0) ? 0.3f : 0.15f);
		}
	}

	public void LineToBottom()
	{
		Canvas.ForceUpdateCanvases();
		GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}

	private void OnEnable()
	{
		CatchEvent.Instance.onNoticeLoading += NoticeLoading;
	}

	private void NoticeLoading(CatchLoadingStep obj)
	{
		bool flag = false;
		switch (obj)
		{
		case CatchLoadingStep.STEP1:
			_step = CatchLoadingStep.STEP1_FINISHED;
			flag = true;
			codeList = codeList2[0];
			break;
		case CatchLoadingStep.STEP2:
			_step = CatchLoadingStep.STEP2_FINISHED;
			flag = true;
			codeList = codeList2[1];
			break;
		case CatchLoadingStep.STEP3:
			_step = CatchLoadingStep.STEP3_FINISHED;
			flag = true;
			codeList = codeList2[2];
			break;
		}
		if (flag)
		{
			StartRun();
		}
	}

	private void OnDisable()
	{
		CatchEvent.Instance.onNoticeLoading -= NoticeLoading;
	}
}
