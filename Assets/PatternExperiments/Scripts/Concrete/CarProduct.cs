using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PatternExperiments;

public class CarProduct : AbstractProduct<CarProduct>
{	
	public override CarProduct Create()
	{
		return this;
	}
}
