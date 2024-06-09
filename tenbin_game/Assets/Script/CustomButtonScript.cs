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
    [SerializeField] Button button;
    [SerializeField] CanvasGroup canvasGroup;
    public UnityEvent initEvent;

    private Vector3 originScale = Vector3.one;
    private bool isInside = false;
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------

    // Start is called before the first frame update
    void Start()
    {
        originScale = transform.localScale;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            Debug.Log("test");
            button.interactable = false;
        });
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
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(originScale * 0.75f, 0.1f).SetEase(Ease.InOutSine));
        seq.Append(transform.DOScale(originScale * 1.5f, 0.5f).SetEase(Ease.InOutSine));
        seq.Join(canvasGroup.DOFade(0.0f, 0.5f).SetEase(Ease.InOutSine));
        seq.OnComplete(() =>
        {
            initEvent.Invoke();
            button.gameObject.SetActive(false);
        });
        // throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isInside = true;
        // throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isInside = false;
        // throw new System.NotImplementedException();
    }

    // ---------- Public関数 ----------
    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------
    /*
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using DG.Tweening;
    using TMPro;

    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(CanvasGroup))]
    public class SimpleButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField]
        Image image;
        [SerializeField]
        CanvasGroup canvasGroup;
        [SerializeField]
        TextMeshProUGUI text;

        [SerializeField]
        bool isInteractable = true;

        [SerializeField]
        ColorSettings buttonColorSettings = new();
        [SerializeField]
        ColorSettings textColorSettings = new();
        [SerializeField]
        ButtonEvents events = new();

        ColorSet currentButtonColorSet;
        ColorSet currentTextColorSet;

        public bool IsInteractable
        {
            get { return isInteractable; }
            set
            {
                if (value == isInteractable)
                {
                    return;
                }

                SetButtonInteractable(value);
            }
        }

        public ButtonEvents Events { get { return events; } set { events = value; } }

        void Start()
        {
            InitButton();
        }

        protected virtual void InitButton()
        {
            SetButtonInteractable(isInteractable);
        }

        void SetButtonInteractable(bool value)
        {
            isInteractable = value;

            if (value)
            {
                ChangeButtonColor(buttonColorSettings.normalColor);
                ChangeTextColor(textColorSettings.normalColor);
            }
            else
            {
                ChangeButtonColor(buttonColorSettings.disabledColor);
                ChangeTextColor(textColorSettings.disabledColor);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isInteractable)
            {
                return;
            }

            ChangeButtonColor(buttonColorSettings.onPointerEnterColor);
            ChangeTextColor(textColorSettings.onPointerEnterColor);
            AdditionalOnPointerEnterProcess();

            events.onPointerEnter.Invoke();
        }

        protected virtual void AdditionalOnPointerEnterProcess()
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isInteractable)
            {
                return;
            }

            ChangeButtonColor(buttonColorSettings.normalColor);
            AdditionalOnPointerExitProcess();

            events.onPointerExit.Invoke();
        }

        protected virtual void AdditionalOnPointerExitProcess()
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable)
            {
                return;
            }

            AdditionalOnPointerClickProcess();

            events.onClick.Invoke();
        }

        protected virtual void AdditionalOnPointerClickProcess()
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isInteractable)
            {
                return;
            }

            ChangeButtonColor(buttonColorSettings.onPointerDownColor);
            AdditionalOnPointerDownProcess();

            events.onPointerDown.Invoke();
        }

        protected virtual void AdditionalOnPointerDownProcess()
        {

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable)
            {
                return;
            }

            ChangeButtonColor(buttonColorSettings.normalColor);
            AdditionalOnPointerUpProcess();

            events.onPointerUp.Invoke();
        }

        protected virtual void AdditionalOnPointerUpProcess()
        {

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

        void ChangeButtonColor(ColorSet colorSet)
        {
            if (currentButtonColorSet == colorSet)
            {
                return;
            }

            ChangeButtonColor(colorSet.color, colorSet.duration, colorSet.ease);

            currentButtonColorSet = colorSet;
        }

        public void ChangeTextColor(Color targetColor, float duration, Ease ease = Ease.Linear)
        {
            if (text == null || duration < 0)
            {
                return;
            }

            text.DOColor(targetColor, duration).SetLink(gameObject).SetUpdate(true).SetEase(ease);
        }

        void ChangeTextColor(ColorSet colorSet)
        {
            if (colorSet == currentTextColorSet)
            {
                return;
            }

            ChangeTextColor(colorSet.color, colorSet.duration, colorSet.ease);

            currentTextColorSet = colorSet;
        }

        [Serializable]
        sealed class ColorSettings
        {

            public ColorSet normalColor;
            public ColorSet disabledColor;
            public ColorSet onPointerEnterColor;
            public ColorSet onPointerDownColor;

        }

        [Serializable]
        sealed class ColorSet
        {

            public Color color = Color.white;
            [Min(0)]
            public float duration = 0;
            public Ease ease = Ease.Linear;

        }

    }

    [Serializable]
    public sealed class ButtonEvents
    {

        public UnityEvent onPointerEnter = new();
        public UnityEvent onPointerExit = new();
        public UnityEvent onClick = new();
        public UnityEvent onPointerDown = new();
        public UnityEvent onPointerUp = new();

    }
    */
}