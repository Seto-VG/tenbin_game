using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteTest : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            GameManager.instance.OnCompleteStage();
        }
    }
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------
    // ---------- プレハブ ----------
    // ---------- プロパティ ----------
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    // ---------- Public関数 ----------
    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weight"))
        {
            Debug.Log("次のステージへ");
            GameManager.instance.OnCompleteStage();
        }
    }
}
