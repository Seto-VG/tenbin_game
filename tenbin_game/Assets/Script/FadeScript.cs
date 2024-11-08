using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    private int m_sceneNum;
    private FadeController m_fadeController;
    [SerializeField]
    private Color m_color;
    private StageSelect stageSelect;

    private void Start()
    {

        m_fadeController = FadeController.Instance;
        m_fadeController.SetSortingOrder(1);
        m_fadeController.FadeIn(1.0f, m_color);

        FadeController.Instance.FadeIn(1.0f);

    }

    private void Update()
    {
        // if (Input.anyKeyDown && m_fadeController.IsFinish) {
        //     m_fadeController.FadeOut(1.0f, m_color, GotoScene);
        // }
    }

    public void FadeOut(int num)
    {
        m_sceneNum = num;
        if (m_fadeController.IsFinish)
        {
            m_fadeController.FadeOut(1.0f, m_color, GotoScene);
        }
    }

    public void FadeOut()
    {
        if (m_fadeController.IsFinish)
        {
            m_fadeController.FadeOut(1.0f, m_color, stageSelect.LoadStage);
        }
    }

    private void GotoScene()
    {

        if (m_sceneNum == -1)
        {
            SceneManager.LoadScene("TitleScene");
        }
        else if (m_sceneNum == 0)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}
