using System.Collections;
using System.IO;

public class FileComparer : IComparer
{
	int IComparer.Compare(object o1, object o2)
	{
		FileInfo obj = o1 as FileInfo;
		FileInfo fileInfo = o2 as FileInfo;
		return obj.CreationTime.CompareTo(fileInfo.CreationTime);
	}
}
