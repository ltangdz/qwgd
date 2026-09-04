using UnityEngine;

public static class SingleHelper
{
	public static T2 CreateBehaviourSingleton<T2>(ref T2 t) where T2 : MonoBehaviour
	{
		if (t == null || t.gameObject == null)
		{
			t = (T2)Object.FindObjectOfType(typeof(T2));
			if (Object.FindObjectsOfType(typeof(T2)).Length > 1)
			{
				Debug.LogError("[Singleton] Something went really wrong  - there should never be more than 1 singleton! Reopenning the scene might fix it.\n");
				Object.DontDestroyOnLoad(t.gameObject);
				return t;
			}
			if (t == null)
			{
				GameObject gameObject = new GameObject();
				t = gameObject.AddComponent<T2>();
				gameObject.name = "(singleton) " + typeof(T2).ToString();
				Object.DontDestroyOnLoad(t.gameObject);
				Debug.Log(string.Concat("[Singleton] An instance of ", typeof(T2), " is needed in the scene, so '", gameObject, "' was created with DontDestroyOnLoad.\n"));
			}
			else
			{
				Debug.Log("[Singleton] Using instance already created: " + t.gameObject.name + "\n");
				Object.DontDestroyOnLoad(t.gameObject);
			}
		}
		return t;
	}
}
