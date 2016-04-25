using System;
using Library.Core;
using System.Collections.Generic;

namespace Library
{
	public interface IClient<T, V> 
		where T : Model 
		where V : Models
	{
		V List(Dictionary<String, String> parameters);
		T View(Dictionary<String, String> parameters);
		T Create (T data);
		T Update(T data);
	}
}