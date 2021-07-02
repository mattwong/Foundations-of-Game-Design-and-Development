using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        CentralManager.centralManagerInstance.increaseScore();
        Destroy(this.gameObject);
    }
}
