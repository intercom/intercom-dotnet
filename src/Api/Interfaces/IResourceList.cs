using System;

namespace Library
{
	public interface IResourceList<T>
		where T : Models
	{
		T List(T data);
	}
}