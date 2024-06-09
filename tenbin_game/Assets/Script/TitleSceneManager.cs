using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class TitleSceneManager : MonoBehaviour
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------
    // ---------- プレハブ ----------
    // ---------- プロパティ ----------
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    private void Start()
    {

    }

    private void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
    // ---------- Public関数 ----------
    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------
}
