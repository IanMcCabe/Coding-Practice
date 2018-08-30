using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PatternExperiments
{
	public abstract class AbstractFactory<T>
	{
		public abstract T Create();
	}
}

