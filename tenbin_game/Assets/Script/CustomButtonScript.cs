using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CustomButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------
    // ---------- プレハブ ----------
    // ---------- プロパティ ----------
    [SerializeField] Image image;
    [SerializeField] CanvasGroup canvasGroup;
    public UnityEvent Event;
    private bool isInside = false;
    private Vector3 originScale = Vector3.one;
    [SerializeField]
    RectTransform rectTransform;
    [SerializeField]
    float onPointerEnterScale = 1;
    [SerializeField]
    float duration = 0.1f;
    [SerializeField]
    Ease ease = Ease.OutQuad;
    public AudioClip seClip;
    
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------

    // Start is called before the first frame update
    void Start()
    {
        originScale = transform.localScale;
        AudioManager.instance.AddSEClip("ButtonSE", seClip);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isInside = true;
        transform
            .DOScale(originScale * 0.9f, 0.1f)
            .SetEase(Ease.InOutSine);
        // throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ボタンの範囲外の場合は処理を行わない
        if (!isInside)
        {
            transform
                .DOScale(originScale, 0.1f)
                .SetEase(Ease.InOutSine);
            return;
        }
        AudioManager.instance.PlaySE("ButtonSE");
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(originScale * 0.75f, 0.1f).SetEase(Ease.InOutSine));
        seq.Append(transform.DOScale(originScale * 1.5f, 0.5f).SetEase(Ease.InOutSine));
        seq.Join(canvasGroup.DOFade(0.0f, 0.5f).SetEase(Ease.InOutSine));
        seq.OnComplete(() =>
        {
            Event.Invoke();
            image.gameObject.SetActive(false);
        });
        // throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //isInside = true;
        AdditionalOnPointerEnterProcess();
        // throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isInside = false;
        AdditionalOnPointerExitProcess();
        // throw new System.NotImplementedException();
    }

    protected virtual void AdditionalOnPointerEnterProcess()
    {
        ChangeButtonScale(onPointerEnterScale);
    }
    protected virtual void AdditionalOnPointerExitProcess()
    {
        ChangeButtonScale(1);
    }

    public void ChangeButtonColor(Color targetColor, float duration, Ease ease = Ease.Linear)
    {
        if (duration < 0)
        {
            return;
        }

        image.DOColor(targetColor, duration).SetLink(gameObject).SetUpdate(true).SetEase(ease);
        canvasGroup.DOFade(targetColor.a, duration).SetLink(gameObject).SetUpdate(true).SetEase(ease);
    }

    void ChangeButtonScale(float scale)
    {
        rectTransform.DOScale(Vector3.one * scale, duration).SetLink(gameObject).SetEase(ease).SetUpdate(true);
    }
}