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
    bool _isCompleatLastStage = false;

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
        else if(Input.GetMouseButtonDown(0) && _isCompleatLastStage)
        {
            // 最後のステージクリア後セレクト画面へ
            SceneManager.LoadScene("StageSelectScene");
            return;
        }
    }

    // 初期化判定
    public bool IsInitialized() { return _isInitialized; }

    // ステージクリア判定
    public bool IsFinishedStage() { return _isFinishedStage; }

    // ステージクリア
    public void OnFinishedStage(string whichComplete)
    {
        _isFinishedStage = true;
        // FXManager.instance.PlayFireworks(); // TODO ゲームクリア後のエフェクト再生
        // Excellentだった場合
        if (whichComplete == "Excellent")
        {
            UIManager.instance.ActiveExcellentText();
            FXManager.instance.PlayConfetti();
        }
        // Greatだった場合
        if (whichComplete == "Great")
        {
            UIManager.instance.ActiveGreatText();
            FXManager.instance.PlayConfetti();
        }
        // Failedだった場合
        if (whichComplete == "Failed")
        {
            UIManager.instance.ActiveFailedText();
        }

        DOVirtual.DelayedCall(2.0f, () =>
        {
            if (StageManager.instance.IsLastStage())
            {
                _isCompleatLastStage = true;
                UIManager.instance.ActiveReturnSelectStageText();
            }
            else
            {
                _nextSceneFlag = true;
                UIManager.instance.ActiveNextStageText();
            }
        });
    }

    // 初期化
    private void Initialize()
    {
        if (_isInitialized) { return; }
        StageManager.instance.CreateCurrentStage();
        _isInitialized = true;
        _nextSceneFlag = false;
    }
}
