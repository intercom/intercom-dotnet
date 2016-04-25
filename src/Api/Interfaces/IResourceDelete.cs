using System;

namespace Library
{
	public interface IResourceDelete<T>
		where T : Model
	{
		T Delete(T data);
	}
}