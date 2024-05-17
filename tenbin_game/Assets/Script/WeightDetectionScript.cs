using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerStayMonitor : MonoBehaviour
{
    public string targetTag = "Weight"; // 監視対象のタグ
    private float triggerStayThreshold = 0.3f; // OnTriggerStayが呼び出されていないとみなすしきい値（秒）

    private Dictionary<GameObject, float> monitoredObjects = new Dictionary<GameObject, float>(); // 監視中のオブジェクトと最終呼び出し時間
    private bool stopMonitoring = false; // 監視を停止するかどうかのフラグ

    // public delegate void TriggerStayEventHandler();
    // public static event TriggerStayEventHandler OnTriggerStayNotCalled; // イベント

    AngleController angleController;
    void Start()
    {
        angleController = GameObject.Find("Circle_1").GetComponent<AngleController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (stopMonitoring)
        {
            return; // 監視停止フラグが立っている場合は何もしない
        }

        if (other.CompareTag(targetTag))
        {
            // 監視中のオブジェクトリストに追加
            if (!monitoredObjects.ContainsKey(other.gameObject))
            {
                monitoredObjects.Add(other.gameObject, Time.time);
                Debug.Log("TryAddWeight");
                angleController.TryAddWeight(other.gameObject);
            }
            else
            {
                // OnTriggerStayが呼び出されたら最終呼び出し時間を更新
                monitoredObjects[other.gameObject] = Time.time;
            }
            StartCoroutine(MonitorTriggerStay(other.gameObject));
        }
    }

    private IEnumerator MonitorTriggerStay(GameObject obj)
    {
        while (monitoredObjects.ContainsKey(obj) && !stopMonitoring)
        {
            yield return null;
            
            // if (!monitoredObjects.ContainsKey(obj))
            // continue;

            if (Time.time - monitoredObjects[obj] > triggerStayThreshold)
            {
                // しきい値を超えてOnTriggerStayが呼び出されていない場合、イベントを発行し、以降の監視を停止
                // OnTriggerStayNotCalled?.Invoke();
                stopMonitoring = true;
                Debug.Log(stopMonitoring);
                angleController.RequestUnregisterWeights(obj);
                monitoredObjects.Remove(obj);
            }
        }
    }
}
