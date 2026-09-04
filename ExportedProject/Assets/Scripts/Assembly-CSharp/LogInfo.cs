using UnityEngine;

public class LogInfo
{
	public int count;

	public string message;

	public string stackTrace;

	public LogType type;

	private const int MaxMessageLength = 16382;

	public bool EqualsTo(LogInfo log)
	{
		if (log == null)
		{
			return false;
		}
		if (message == log.message && stackTrace == log.stackTrace)
		{
			return type == log.type;
		}
		return false;
	}

	public string GetFinalMessage()
	{
		if (string.IsNullOrEmpty(message))
		{
			return "";
		}
		if (message.Length > 16382)
		{
			return message.Substring(0, 16382);
		}
		return message;
	}
}
