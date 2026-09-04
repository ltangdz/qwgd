using System.Collections.Generic;

public class ListComparer<T> : IEqualityComparer<T>
{
	public delegate bool EqualsComparer<F>(F x, F y);

	public EqualsComparer<T> equalsComparer;

	public ListComparer(EqualsComparer<T> _euqlsComparer)
	{
		equalsComparer = _euqlsComparer;
	}

	public bool Equals(T x, T y)
	{
		if (equalsComparer != null)
		{
			return equalsComparer(x, y);
		}
		return false;
	}

	public int GetHashCode(T obj)
	{
		return obj.ToString().GetHashCode();
	}
}
