using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerStayMonitor : MonoBehaviour
{
    public string targetTag = "Weight"; // 監視対象のタグ
    private float triggerStayThreshold = 0.3f; // OnTriggerStayが呼び出されていないとみなすしきい値（秒）

    private Dictionary<GameObject, float> monitoredObjects = new Dictionary<GameObject, float>(); // 監視中のオブジェクトと最終呼び出し時間
    private bool stopMonitoring = false; // 監視を停止するかどうかのフラグ

    AngleController angleController;

    void Start()
    {
        angleController = GameObject.Find("Circle_1").GetComponent<AngleController>();
        StartCoroutine(MonitorTriggerStay());
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
                angleController.TryAddWeight(other.gameObject);
            }
            else
            {
                // OnTriggerStayが呼び出されたら最終呼び出し時間を更新
                monitoredObjects[other.gameObject] = Time.time;
            }
        }
    }

    private IEnumerator MonitorTriggerStay()
    {
        while (true)
        {
            if (stopMonitoring)
            {
                yield break; // コルーチンを終了
            }

            List<GameObject> objectsToRemove = new List<GameObject>();

            foreach (var obj in monitoredObjects)
            {
                if (Time.time - obj.Value > triggerStayThreshold)
                {
                    angleController.RequestUnregisterWeights(obj.Key);
                    objectsToRemove.Add(obj.Key);
                }
            }

            foreach (var obj in objectsToRemove)
            {
                monitoredObjects.Remove(obj);
            }

            yield return null; // 次のフレームまで待つ
        }
    }
}

