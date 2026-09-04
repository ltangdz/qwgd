using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AlubaTools
{
	public static bool ListEquals<T>(IEnumerable<T> one, IEnumerable<T> another)
	{
		if (one.Count() != another.Count())
		{
			return false;
		}
		return one.Except(another).Count() == 0;
	}

	public static List<T> Swap<T>(List<T> list, int index1, int index2)
	{
		T value = list[index1];
		list[index1] = list[index2];
		list[index2] = value;
		return list;
	}

	public static List<T> RandomList<T>(List<T> original)
	{
		List<T> newList = new List<T>();
		original.ForEach(delegate(T i)
		{
			newList.Add(i);
		});
		System.Random random = new System.Random();
		int num = 0;
		for (int num2 = 0; num2 < newList.Count; num2++)
		{
			num = random.Next(0, newList.Count - 1);
			if (num != num2)
			{
				T value = newList[num2];
				newList[num2] = newList[num];
				newList[num] = value;
			}
		}
		return newList;
	}

	public static bool IsRectTransformOverlap(RectTransform rect1, RectTransform rect2)
	{
		Vector3[] array = new Vector3[4];
		rect1.GetWorldCorners(array);
		array[2].x = Mathf.Abs(array[2].x - array[0].x);
		array[2].y = Mathf.Abs(array[2].y - array[0].y);
		Rect rect3 = new Rect(array[0].x, array[0].y, array[2].x, array[2].y);
		rect2.GetWorldCorners(array);
		array[2].x = Mathf.Abs(array[2].x - array[0].x);
		array[2].y = Mathf.Abs(array[2].y - array[0].y);
		Rect other = new Rect(array[0].x, array[0].y, array[2].x, array[2].y);
		return rect3.Overlaps(other);
	}

	public static string UserMd5(string str)
	{
		StringBuilder stringBuilder = new StringBuilder();
		byte[] array = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str));
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("X2"));
		}
		return stringBuilder.ToString();
	}

	public static Vector2 MouseToUgui(Vector3 position, Transform transform, string canvasName)
	{
		Vector2 vector = position;
		Vector2 vector2 = new Vector2(Screen.width, Screen.height);
		Canvas[] componentsInChildren = transform.root.GetComponentsInChildren<Canvas>();
		Canvas canvas = null;
		foreach (Canvas canvas2 in componentsInChildren)
		{
			if (canvas2.name.Equals(canvasName))
			{
				canvas = canvas2;
				break;
			}
		}
		if (canvas == null)
		{
			Debug.Log("未找到Canvas");
			return Vector2.zero;
		}
		return vector / vector2 * canvas.GetComponent<RectTransform>().sizeDelta;
	}

	public static float Angle(Vector2 from_, Vector2 to_)
	{
		float num = from_.x - to_.x;
		float num2 = from_.y - to_.y;
		float num3 = Mathf.Sqrt(Mathf.Pow(num, 2f) + Mathf.Pow(num2, 2f));
		float num4 = Mathf.Acos(num / num3);
		float num5 = 180f / ((float)Math.PI / num4);
		if (num2 < 0f)
		{
			num5 = 0f - num5;
		}
		else if (num2 == 0f && num < 0f)
		{
			num5 = 180f;
		}
		return num5;
	}

	public static float GetUnityDirection(Vector2 p1, Vector2 p2)
	{
		float num = Angle180To360(PointToAngle(p1, p2));
		float num2 = 90f;
		float result = 0f;
		for (int i = 0; i < 4; i++)
		{
			if (num >= (float)i * num2 - num2 * 0.5f && num < (float)i * num2 + num2 * 0.5f)
			{
				result = (float)i * num2;
				break;
			}
		}
		return result;
	}

	public static float Angle180To360(float angle)
	{
		if (angle >= 0f && angle <= 180f)
		{
			return angle;
		}
		return 360f + angle;
	}

	public static float PointToAngle(Vector2 p1, Vector2 p2)
	{
		float x = p2.x - p1.x;
		return Mathf.Atan2(p2.y - p1.y, x) * 180f / (float)Math.PI;
	}

	public static float CalculateLengthOfText(Text text, string message)
	{
		TextGenerationSettings generationSettings = text.GetGenerationSettings(Vector2.zero);
		generationSettings.scaleFactor = 1f;
		return text.cachedTextGeneratorForLayout.GetPreferredWidth(message, generationSettings);
	}
}
