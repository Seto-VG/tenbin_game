using Unity.VisualScripting;
using UnityEngine;

public class WeightController : MonoBehaviour
{
    [SerializeField]
    private Camera _cam;
    private Vector3 _initialPosition; // 初期位置
    private Rigidbody _weightRb;
    [SerializeField]
    private bool _isFall = false;
    [SerializeField]
    private Collider _weightFallAreaR; // 判定したいコライダー
    [SerializeField]
    private float weight;
    AngleController angleController;

    // Start is called before the first frame update
    void Start()
    {
        _weightRb = gameObject.GetComponent<Rigidbody>();
        _initialPosition = this.transform.position;

        angleController = GameObject.Find("Circle_1").GetComponent<AngleController>();
        if (angleController != null)
        {
            // リストに自身を登録
            angleController.RegisterWeights(this.GameObject(), weight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsObjectInsideCollider(gameObject, _weightFallAreaR))
        {
            _weightRb.isKinematic = false;
        }
        else if (!_isFall)
        {
            _weightRb.isKinematic = true;
        }
    }

    // 錘をドラッグで動かせるように
    void WeightDrag()
    {
        Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objPos.z);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    // オブジェクトが完全にコライダー内に入っているかを判定
    private bool IsObjectInsideCollider(GameObject weight, Collider fallAreaCollider)
    {
        // オブジェクトの境界ボックスとコライダーの境界ボックスを取得する
        Bounds objectBounds = weight.GetComponent<Renderer>().bounds;
        Bounds colliderBounds = fallAreaCollider.bounds;

        // オブジェクトの全ての角がコライダー内にあるかを確認する
        return colliderBounds.Contains(objectBounds.min) &&
               colliderBounds.Contains(objectBounds.max) &&
               colliderBounds.Contains(new Vector3(objectBounds.min.x, objectBounds.min.y, objectBounds.max.z)) &&
               colliderBounds.Contains(new Vector3(objectBounds.min.x, objectBounds.max.y, objectBounds.min.z)) &&
               colliderBounds.Contains(new Vector3(objectBounds.min.x, objectBounds.max.y, objectBounds.max.z)) &&
               colliderBounds.Contains(new Vector3(objectBounds.max.x, objectBounds.min.y, objectBounds.min.z)) &&
               colliderBounds.Contains(new Vector3(objectBounds.max.x, objectBounds.min.y, objectBounds.max.z)) &&
               colliderBounds.Contains(new Vector3(objectBounds.max.x, objectBounds.max.y, objectBounds.min.z));
    }

    void OnMouseDrag()
    {
        WeightDrag(); // ドラッグで動かす
    }

    void OnMouseUp()
    {
        if (IsObjectInsideCollider(gameObject, _weightFallAreaR))
        {
            _isFall = true;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }
        else
        {
            this.transform.position = _initialPosition;
        }
    }

    private void OnEnable()
    {
        TriggerStayMonitor.OnTriggerStayNotCalled += HandleTriggerStayNotCalled;
    }

    private void OnDisable()
    {
        TriggerStayMonitor.OnTriggerStayNotCalled -= HandleTriggerStayNotCalled;
    }

    // イベントが発行されたときこの処理をする
    private void HandleTriggerStayNotCalled()
    {
        // 監視を終了 登録の解除申請 合計重量からこの重りの重量を削除
        angleController.RequestUnregisterWeights(gameObject);
        Debug.Log("OnTriggerStay is not being called!");
    }
}
