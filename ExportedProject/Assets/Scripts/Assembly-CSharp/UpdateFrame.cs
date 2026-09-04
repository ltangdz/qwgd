using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class UpdateFrame : MonoBehaviour
{
	private const float fpsMeasurePeriod = 0.5f;

	private int m_FpsAccumulator;

	private float m_FpsNextPeriod;

	private int m_CurrentFps;

	private const string display = "{0} FPS";

	public Text m_Text;

	public int FPS;

	private void Awake()
	{
		m_FpsNextPeriod = Time.realtimeSinceStartup + 0.5f;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Update()
	{
		m_FpsAccumulator++;
		if (Time.realtimeSinceStartup > m_FpsNextPeriod)
		{
			m_CurrentFps = (int)((float)m_FpsAccumulator / 0.5f);
			m_FpsAccumulator = 0;
			m_FpsNextPeriod += 0.5f;
			m_Text.GetComponent<I18NText>().updateTranslation2($"{m_CurrentFps} FPS");
		}
	}

	private void OnApplicationPause()
	{
	}

	private void OnApplicationFocus()
	{
	}
}
