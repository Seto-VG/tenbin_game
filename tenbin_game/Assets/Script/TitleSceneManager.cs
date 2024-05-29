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
    [SerializeField] private TextMeshProUGUI _enterText = default;
    [SerializeField] private new GameObject gameObject;
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    private Vector3 _initialTitleTextPosition = default;
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    private void Start()
    {
        StartFloatingTitleText();
        FadeAnimationEnterText(false);
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

    private void StartFloatingTitleText()
    {
        _initialTitleTextPosition = gameObject.transform.localPosition;
        gameObject.transform
            .DOLocalMoveY(_initialTitleTextPosition.y + 50.0f, 3.0f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void FadeAnimationEnterText(bool bIn)
    {
        _enterText
            .DOFade(bIn ? 1.0f : 0.0f, 1.0f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => FadeAnimationEnterText(!bIn));
    }
}
