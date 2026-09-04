using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameHelper : MonoBehaviour
{
	public KeyCode toggleKey = KeyCode.BackQuote;

	public bool openOnStart;

	public bool shakeToOpen = true;

	public float shakeAcceleration = 3f;

	public bool clampLogCount;

	public int maxLogCount = 1000;

	private readonly GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");

	private readonly GUIContent HideLabel = new GUIContent("Hide", "Hide the contents");

	private readonly GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");

	private bool isCollapsed;

	private bool isLogPanelVisible;

	private Vector2 scrollPosition;

	private Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
	{
		{
			LogType.Assert,
			Color.white
		},
		{
			LogType.Error,
			Color.red
		},
		{
			LogType.Exception,
			Color.red
		},
		{
			LogType.Log,
			Color.white
		},
		{
			LogType.Warning,
			Color.yellow
		}
	};

	private Dictionary<LogType, bool> logTypeFilters = new Dictionary<LogType, bool>
	{
		{
			LogType.Assert,
			true
		},
		{
			LogType.Error,
			true
		},
		{
			LogType.Exception,
			true
		},
		{
			LogType.Log,
			true
		},
		{
			LogType.Warning,
			true
		}
	};

	private List<LogInfo> logs = new List<LogInfo>();

	private ConcurrentQueue<LogInfo> queuedLogs = new ConcurrentQueue<LogInfo>();

	private const int margin = 50;

	private const string windowTitle = "GameHelper";

	private float windowHeight = 150f;

	private bool showAllBtn;

	private Rect windowRect = new Rect(50f, 50f, Screen.width - 100, Screen.height - 100);

	private void OnDisable()
	{
		Application.logMessageReceivedThreaded -= HandleLogThreaded;
	}

	private void OnEnable()
	{
		Application.logMessageReceivedThreaded += HandleLogThreaded;
	}

	private void Start()
	{
		isLogPanelVisible = openOnStart;
	}

	public void SetLogPanelStatus(bool show)
	{
		isLogPanelVisible = show;
	}

	private void OnGUI()
	{
		if (!isLogPanelVisible)
		{
			float width = 200f;
			float btnHeight = 35f;
			GUILayout.Window(0, new Rect(0f, 0f, width, windowHeight), delegate
			{
				if (showAllBtn && GUILayout.Button("Show", GUILayout.Width(width), GUILayout.Height(btnHeight)))
				{
					SetLogPanelStatus(show: true);
				}
				showAllBtn = GUILayout.Toggle(showAllBtn, "    Show All Btn");
				windowHeight = (showAllBtn ? 60f : 30f);
			}, "GameHelper");
		}
		else
		{
			windowRect = GUILayout.Window(1, windowRect, DrawWindow, "GameHelper");
		}
	}

	private void Update()
	{
		UpdateQueuedLogs();
		if (Input.GetKeyDown(toggleKey))
		{
			isLogPanelVisible = !isLogPanelVisible;
		}
		if (shakeToOpen && Input.acceleration.sqrMagnitude > shakeAcceleration)
		{
			isLogPanelVisible = !isLogPanelVisible;
		}
	}

	private void UpdateQueuedLogs()
	{
		LogInfo result;
		while (queuedLogs.TryDequeue(out result))
		{
			ShowLogItem(result);
		}
	}

	private void ShowLogItem(LogInfo log)
	{
		LogInfo logInfo = ((logs.Count == 0) ? null : logs.Last());
		if (log != null && log.EqualsTo(logInfo))
		{
			log.count = logInfo.count + 1;
			logs[logs.Count - 1] = log;
			return;
		}
		logs.Add(log);
		if (clampLogCount)
		{
			RemoveUnnecessaryLogs();
		}
	}

	private void DrawWindow(int windowID)
	{
		DrawLogList();
		DrawToolbar();
	}

	private void DrawLogList()
	{
		scrollPosition = GUILayout.BeginScrollView(scrollPosition);
		GUILayout.BeginVertical();
		foreach (LogInfo item in logs.Where((LogInfo log) => logTypeFilters[log.type]))
		{
			DrawLog(item);
		}
		GUILayout.EndVertical();
		Rect lastRect = GUILayoutUtility.GetLastRect();
		GUILayout.EndScrollView();
		Rect lastRect2 = GUILayoutUtility.GetLastRect();
		if (Event.current.type == EventType.Repaint && IsScrolledToBottom(lastRect, lastRect2))
		{
			ScrollToBottom();
		}
		GUI.contentColor = Color.white;
	}

	private void DrawLog(LogInfo log)
	{
		GUI.contentColor = logTypeColors[log.type];
		if (isCollapsed)
		{
			DrawCollapsedLog(log);
		}
		else
		{
			DrawExpandedLog(log);
		}
	}

	private void DrawCollapsedLog(LogInfo log)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label(log.GetFinalMessage());
		GUILayout.FlexibleSpace();
		GUILayout.Label(log.count.ToString(), GUI.skin.box);
		GUILayout.EndHorizontal();
	}

	private void DrawExpandedLog(LogInfo log)
	{
		for (int i = 0; i < log.count; i++)
		{
			GUILayout.Label(log.GetFinalMessage());
			GUILayout.Label(log.stackTrace);
		}
	}

	private void DrawToolbar()
	{
		GUILayout.BeginHorizontal();
		if (GUILayout.Button(clearLabel))
		{
			logs.Clear();
		}
		if (GUILayout.Button(HideLabel))
		{
			SetLogPanelStatus(show: false);
		}
		foreach (LogType value2 in Enum.GetValues(typeof(LogType)))
		{
			bool value = logTypeFilters[value2];
			string text = value2.ToString();
			logTypeFilters[value2] = GUILayout.Toggle(value, text, GUILayout.ExpandWidth(expand: false));
			GUILayout.Space(20f);
		}
		isCollapsed = GUILayout.Toggle(isCollapsed, collapseLabel, GUILayout.ExpandWidth(expand: false));
		GUILayout.EndHorizontal();
	}

	private bool IsScrolledToBottom(Rect innerScrollRect, Rect outerScrollRect)
	{
		float height = innerScrollRect.height;
		float num = outerScrollRect.height - (float)GUI.skin.box.padding.vertical;
		if (num > height)
		{
			return true;
		}
		return Mathf.Approximately(height, scrollPosition.y + num);
	}

	private void ScrollToBottom()
	{
		scrollPosition = new Vector2(0f, 2.1474836E+09f);
	}

	private void RemoveUnnecessaryLogs()
	{
		int num = logs.Count - maxLogCount;
		if (num > 0)
		{
			logs.RemoveRange(0, num);
		}
	}

	private void HandleLogThreaded(string message, string stackTrace, LogType type)
	{
		LogInfo item = new LogInfo
		{
			count = 1,
			message = message,
			stackTrace = stackTrace,
			type = type
		};
		queuedLogs.Enqueue(item);
	}
}
