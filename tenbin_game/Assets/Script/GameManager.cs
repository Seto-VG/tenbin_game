using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : SingletonBehavior<GameManager>
{
    bool _isInitialized = false;
    bool _isCompleteStage = false;
    bool _nextSceneFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && _nextSceneFlag)
        {
            // 次のステージへ
            StageManager.instance.SetNextStage();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public bool IsInitialized() { return _isInitialized; }

    // ステージクリア判定
    public bool IsCompleteStage() { return _isCompleteStage; }

    // ステージクリア
    public void OnCompleteStage()
    {
        if (_isCompleteStage) { return; }
        _isCompleteStage = true;

        // FXManager.instance.PlayFireworks(); // TODO ゲームクリア後のエフェクト再生

        DOVirtual.DelayedCall(3.0f, () =>
        {
            if (StageManager.instance.IsLastStage())
            {
                // クリア画面へ遷移
                SceneManager.LoadScene("StageSelectScene");
                return;
            }
            else
            {
                _nextSceneFlag = true;
            }
        });
    }

    // 初期化
    private void Initialize()
    {
        if (_isInitialized) { return; }
        StageManager.instance.CreateCurrentStage();
        _isInitialized = true;
    }
}
