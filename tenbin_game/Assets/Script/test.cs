using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //[SerializeField]
	//List<GameObject> _weightList = new List<GameObject>(); // 検知している重りのリスト
    void OnTriggerEnter(Collider other)
	{
		//_weightList.Add(other.gameObject);
        if(other.CompareTag("Weight"))
        {
            Debug.Log("当たった");
        }
	}
}
