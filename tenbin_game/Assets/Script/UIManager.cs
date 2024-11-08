using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : SingletonBehavior<UIManager>
{
    public bool isInGame;
    private int stageId = GameData.stageId;
    public GameObject titleLogo;
    public CanvasGroup highlightDraggableArea;
    [SerializeField] private GameObject _excellentText;
    [SerializeField] private GameObject _greatText;
    [SerializeField] private GameObject _failedText;
    [SerializeField] private GameObject _nextStageButton;
    [SerializeField] private GameObject _returnSelectStageButton;
    [SerializeField] private GameObject _returnButton;
    [SerializeField] private GameObject _finishButton;
    [SerializeField] private List<GameObject> _stageNumTextList = new List<GameObject>();
    private GameObject _stageNumberInstance;
    [SerializeField] private RectTransform canvasTransform;

    private void Start()
    {
        if (!isInGame)
        {
            StartFloatingObj(titleLogo);
        }
        else
        {
            // stageIdに対応したステージ数のUIをインスタンス化する
            _stageNumberInstance = Instantiate(_stageNumTextList[stageId], canvasTransform);
            FadeObj(highlightDraggableArea,true);
        }
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
        _nextStageButton.SetActive(true);
    }

    public void OffNextStageText()
    {
        _nextStageButton.SetActive(false);
    }

    public void ActiveReturnSelectStageText()
    {
        _returnSelectStageButton.SetActive(true);
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

    public void ActiveFinishButton(bool flag)
    {
        _finishButton.SetActive(flag);
    }

    private void StartFloatingObj(GameObject gameObject)
    {
        Vector3 initPosition = gameObject.transform.localPosition;
        gameObject.transform
            .DOLocalMoveY(initPosition.y + 50.0f, 3.0f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void FadeAnimText(TextMeshProUGUI text, bool bIn)
    {
        text
            .DOFade(bIn ? 1.0f : 0.0f, 1.0f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => FadeAnimText(text, !bIn));
    }

    public void FadeObj(CanvasGroup canvasGroup, bool bIn)
    {
        canvasGroup
            .DOFade(bIn ? 1.0f : 0.0f, 1.0f)
            .SetEase(Ease.InSine);
    }
}
