using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehavior<UIManager>
{
    [SerializeField] private GameObject _excellentText;
    [SerializeField] private GameObject _greatText;
    [SerializeField] private GameObject _failedText;
    [SerializeField] private GameObject _nextStageText;
    [SerializeField] private GameObject _returnSelectStageText;
    // Start is called before the first frame update
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

    public void ActiveReturnSelectStageText()
    {
        _returnSelectStageText.SetActive(true);
    }
}
