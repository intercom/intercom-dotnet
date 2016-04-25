using System;

namespace Library
{
	public interface ICreate<T> 
		where T : Model
	{
		T Create (T data);
	}
}