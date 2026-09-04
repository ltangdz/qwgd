public class Singleton<T> where T : new()
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (!object.Equals(instance, default(T)))
			{
				return instance;
			}
			return instance = new T();
		}
	}
}
