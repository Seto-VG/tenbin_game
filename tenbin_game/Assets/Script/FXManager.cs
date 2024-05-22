using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : SingletonBehavior<FXManager>
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------

    [SerializeField] private GameObject confetti;
    // ---------- プレハブ ----------
    // ---------- プロパティ ----------
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    // ---------- Public関数 ----------
    public void PlayConfetti()
    {
        confetti.SetActive(true);
    }
    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------
}
