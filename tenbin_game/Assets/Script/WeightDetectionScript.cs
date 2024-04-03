using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeightDetectionScript : MonoBehaviour
{
    [SerializeField]
    AngleController angleController;

    void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Weight"))
        {
            angleController.EnterAreaObj(other.gameObject);
            Debug.Log("当たった");
        }
	}
}
