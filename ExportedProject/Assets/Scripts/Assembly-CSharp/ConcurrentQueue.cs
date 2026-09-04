using System.Collections.Generic;

public class ConcurrentQueue<T>
{
	private readonly object queueLock = new object();

	private readonly Queue<T> queue = new Queue<T>();

	public void Enqueue(T item)
	{
		lock (queueLock)
		{
			queue.Enqueue(item);
		}
	}

	public bool TryDequeue(out T result)
	{
		lock (queueLock)
		{
			if (queue.Count == 0)
			{
				result = default(T);
				return false;
			}
			result = queue.Dequeue();
			return true;
		}
	}
}
