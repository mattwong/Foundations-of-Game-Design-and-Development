using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoolSingleton : Singleton<SpawnPoolSingleton>
{
	override public void Awake(){
		base.Awake();
		Debug.Log("awake called");
	}
}
