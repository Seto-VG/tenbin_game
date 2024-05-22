using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect: MonoBehaviour 
{
    [SerializeField]
    private int _stageNumber = 0;

    public void LoadStage()
    {
        _stageNumber -= 1; 
        GameData.stageId = _stageNumber;
        SceneManager.LoadScene("InGameScene");
    }
}
