using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect: MonoBehaviour 
{
    [SerializeField]
    private int _stageNumber = 0;
    public GameObject scoreUI;
    public int score_num = 0; // スコア変数
    void Start()
    {
        //TODO そのステージに対応したデータをロードしてクリア状況に応じたスコアを表示
        // スコアのロード   ステージ１のスコアの場合のキー "Stage1"
        score_num = PlayerPrefs.GetInt ("score" + Convert.ToString(_stageNumber), 0);
        Debug.Log("score" + Convert.ToString(_stageNumber));
        if (score_num == 1)
        {
            scoreUI.SetActive(true);
        }
    }
    public void LoadStage()
    {
        _stageNumber -= 1; 
        GameData.stageId = _stageNumber;
        SceneManager.LoadScene("InGameScene");
    }
}
