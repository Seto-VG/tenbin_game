using UnityEngine;
using System.Collections;

public class TriggerStayMonitor : MonoBehaviour
{
    public string targetTag = "Weight"; // 監視対象のタグ
    public float triggerStayThreshold = 15f; // OnTriggerStayが呼び出されていないとみなすしきい値（秒）

    private float lastTriggerStayTime; // 最後にOnTriggerStayが呼び出された時間
    private bool isMonitoring = false; // 監視中かどうかのフラグ
    private bool stopMonitoring = false; // 監視を停止するかどうかのフラグ

    public delegate void TriggerStayEventHandler();
    public static event TriggerStayEventHandler OnTriggerStayNotCalled; // イベント

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

        if (!isMonitoring && other.CompareTag(targetTag))
        {
            // 監視開始
            isMonitoring = true;
            lastTriggerStayTime = Time.time;
            // 合計重量リストに入っていなかったら追加
            angleController.TryAddWeight(other.gameObject);

            StartCoroutine(MonitorTriggerStay());
        }
        else if (isMonitoring)
        {
            // OnTriggerStayが呼び出されたら最終呼び出し時間を更新
            lastTriggerStayTime = Time.time;
        }
    }

    private IEnumerator MonitorTriggerStay()
    {
        while (isMonitoring && !stopMonitoring)
        {
            yield return null;

            if (Time.time - lastTriggerStayTime > triggerStayThreshold)
            {
                // しきい値を超えてOnTriggerStayが呼び出されていない場合、イベントを発行し、以降の監視を停止
                OnTriggerStayNotCalled?.Invoke();
                stopMonitoring = true;
            }
        }
    }
}
