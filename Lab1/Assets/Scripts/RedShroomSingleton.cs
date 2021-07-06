using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShroomSingleton : Singleton<RedShroomSingleton>
{
	override public void Awake(){
		base.Awake();
		Debug.Log("awake called");
	}
}
