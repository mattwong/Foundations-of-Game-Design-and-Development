using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : Singleton<DontDestroy>
{
	override public void Awake(){
		base.Awake();
		Debug.Log("awake called");
	}
}
