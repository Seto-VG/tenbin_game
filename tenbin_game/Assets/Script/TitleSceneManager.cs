using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class TitleSceneManager : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {

    }

    public void LoadScene(int stageNum)
    {
        if(stageNum == 0)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
        else if(stageNum == 1)
        {
            SceneManager.LoadScene("TutorialScene");
        }
    }
}
