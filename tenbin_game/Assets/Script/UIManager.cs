using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UIManager : SingletonBehavior<UIManager>
{
    private int stageId = GameData.stageId;
    [SerializeField] private GameObject _excellentText;
    [SerializeField] private GameObject _greatText;
    [SerializeField] private GameObject _failedText;
    [SerializeField] private GameObject _nextStageText;
    [SerializeField] private GameObject _returnSelectStageText;
    [SerializeField] private GameObject _returnButton;
    [SerializeField] private List<GameObject> _stageNumTextList = new List<GameObject>();
    private GameObject _stageNumberInstance;
    [SerializeField] private RectTransform canvasTransform;

    private void Start()
    {
        // stageIdに対応したステージ数のUIをインスタンス化する
        _stageNumberInstance = Instantiate(_stageNumTextList[stageId], canvasTransform);
    }

    public void ActiveExcellentText()
    {
        _excellentText.SetActive(true);
    }

    public void ActiveGreatText()
    {
        _greatText.SetActive(true);
    }

    public void ActiveFailedText()
    {
        _failedText.SetActive(true);
    }

    public void ActiveNextStageText()
    {
        _nextStageText.SetActive(true);
    }

    public void OffNextStageText()
    {
        _nextStageText.SetActive(false);
    }

    public void ActiveReturnSelectStageText()
    {
        _returnSelectStageText.SetActive(true);
    }
    
    public void ActiveReturnButton()
    {
        _returnButton.SetActive(true);
    }

    public void ActiveStageNum()
    {
        _stageNumTextList[stageId].SetActive(true);
    }

    public void OffStageNum()
    {
        _stageNumberInstance.SetActive(false);
    }
}
