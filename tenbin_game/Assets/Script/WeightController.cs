using Unity.VisualScripting;
using UnityEngine;

public class WeightController : MonoBehaviour
{
    private Vector3 _initPos; // 初期位置
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
        _initPos = this.transform.position;

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
        if (GameManager.instance.IsFinishedStage() || !angleController.IsReady())
        { return; }

        // ドラッグで動かす
        Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objPos.z);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    void OnMouseUp()
    {
        if (IsObjectInsideCollider(gameObject, _weightFallAreaR))
        {
            _isFall = true;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            _weightRb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.position = _initPos;
        }
    }
}
