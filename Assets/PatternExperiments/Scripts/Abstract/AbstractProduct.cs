using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PatternExperiments
{
	public abstract class AbstractProduct<T>
	{
		public abstract T Create();
	}
}