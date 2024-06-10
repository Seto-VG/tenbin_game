using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BGColorChange : MonoBehaviour
{
    public new Camera camera;  // 背景色を変更するカメラ
    public Color completeColor;  // ゲームクリア時の色
    public Color failedColor;  // ゲームオーバー時の色
    public float duration;  // アニメーションの時間
    void Start()
    {

    }

    void Update()
    {
        if (!GameManager.instance.IsFinishedStage()) {return;}

        // クリアしたときGreatなら
        if ("Excellent" == AngleController.instance.WhichCompletionStatus())
        {
            camera.DOColor(completeColor, duration);
        }
        // クリアしたときFailedなら
        else if ("Failed" == AngleController.instance.WhichCompletionStatus())
        {
            camera.DOColor(failedColor, duration);
        }
    }
}
