using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : SingletonBehavior<GameManager>
{
    bool _isInitialized = false;
    bool _isFinishedStage = false;
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
    public bool IsFinishedStage() { return _isFinishedStage; }

    // ステージクリア
    public void OnCompleteStage(string whichComplete)
    {
        _isFinishedStage = true;
        // FXManager.instance.PlayFireworks(); // TODO ゲームクリア後のエフェクト再生
        // Excellentだった場合
        if (whichComplete == "Excellent")
        {

        }
        // Greatだった場合
        if (whichComplete == "Great")
        {
            
        }

        DOVirtual.DelayedCall(3.0f, () =>
        {
            if (StageManager.instance.IsLastStage())
            {
                // 最後のステージクリア後セレクト画面へ
                SceneManager.LoadScene("StageSelectScene");
                return;
            }
            else
            {
                _nextSceneFlag = true;
            }
        });
    }

    // ゲームオーバー
    public void OnGameOver()
    {
        _isFinishedStage = true;
    }

    // 初期化
    private void Initialize()
    {
        if (_isInitialized) { return; }
        StageManager.instance.CreateCurrentStage();
        _isInitialized = true;
    }
}
