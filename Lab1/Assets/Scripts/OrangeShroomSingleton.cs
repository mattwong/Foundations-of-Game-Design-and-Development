using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeShroomSingleton : Singleton<OrangeShroomSingleton>
{
	override public void Awake(){
		base.Awake();
		Debug.Log("awake called");
	}
}
