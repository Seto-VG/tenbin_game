using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : SingletonBehavior<FXManager>
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------

    [SerializeField] private List<Transform> _fireworkPoints = default;
    // ---------- プレハブ ----------
    // ---------- プロパティ ----------
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    // ---------- Public関数 ----------
    public void PlayFireworks()
    {
        foreach (Transform item in _fireworkPoints)
        {
            item.gameObject.SetActive(true);
        }
    }
    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------
}
