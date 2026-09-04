using UnityEngine;

namespace Aluba
{
	public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance;

		public static T GetInstance()
		{
			if (instance == null)
			{
				instance = new GameObject
				{
					name = typeof(T).ToString()
				}.AddComponent<T>();
			}
			return instance;
		}
	}
}
