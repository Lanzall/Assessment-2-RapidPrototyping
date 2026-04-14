using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public static class FadeX
{
    public static void FadeIn(CanvasGroup _canvasGroup, float _tweenTime, Ease _ease, bool _interactable = true, Action _onSuccess = null)
    {
        _canvasGroup.gameObject.SetActive(true);
        if (_canvasGroup.alpha == 1) return;

        _canvasGroup.DOFade(1, _tweenTime).SetUpdate(true).OnComplete(() => _onSuccess.Invoke());
        _canvasGroup.interactable = _interactable;
        _canvasGroup.blocksRaycasts = _interactable;
    }
    public static void FadeIn(CanvasGroup _canvasGroup, float _tweenTime = 0.5f, bool _interactable = true, Action _onSuccess = null)
    {
        _canvasGroup.gameObject.SetActive(true);
        if (_canvasGroup.alpha == 1) return;

        _canvasGroup.DOFade(1, _tweenTime).SetUpdate(true).OnComplete(() => _onSuccess.Invoke());
        _canvasGroup.interactable = _interactable;
        _canvasGroup.blocksRaycasts = _interactable;
    }

    public static void FadeOut(CanvasGroup _canvasGroup, float _tweenTime, Ease _ease, bool _interactable = true, Action _onSuccess = null)
    {
        if (_canvasGroup.alpha == 0) return;

        _canvasGroup.DOFade(0, _tweenTime).SetUpdate(true).OnComplete(() => _onSuccess.Invoke());
        _canvasGroup.interactable = _interactable;
        _canvasGroup.blocksRaycasts = _interactable;
    }
    public static void FadeOut(CanvasGroup _canvasGroup, float _tweenTime = 0.5f, bool _interactable = false, Action _onSuccess = null)
    {
        if (_canvasGroup.alpha == 0) return;

        _canvasGroup.DOFade(0, _tweenTime).SetUpdate(true).OnComplete(()=> _onSuccess.Invoke());
        _canvasGroup.interactable = _interactable;
        _canvasGroup.blocksRaycasts = _interactable;
    }
    public static void FadeOut(Image _image, float _tweenTime = 0.5f, Action _onSuccess = null)
    {
        if (_image.color.a == 0) return;

        _image.DOFade(0, _tweenTime).SetUpdate(true).OnComplete(()=> _onSuccess.Invoke());
    }

    public static void FadeTo(CanvasGroup _canvasGroup, float _toValue = 0.5f, float _tweenTime = 0.5f, bool _interactable = false, Action _onSuccess = null)
    {
        if (_canvasGroup.alpha == _toValue) return;

        _canvasGroup.gameObject.SetActive(true);

        _canvasGroup.DOFade(_toValue, _tweenTime).SetUpdate(true).OnComplete(() => _onSuccess.Invoke());
        _canvasGroup.interactable = _interactable;
        _canvasGroup.blocksRaycasts = _interactable;
    }

    /// <summary>
    /// Makes a panel instantly opaque and optionally sets interactable (true by default)
    /// </summary>
    /// <param name="_canvasGroup">The canvas group to fade</param>
    /// <param name="_interactable">Are we interactable?</param>
    public static void InstantOpaque(CanvasGroup _canvasGroup, bool _interactable = true)
    {
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = _interactable;
        _canvasGroup.blocksRaycasts = _interactable;
    }

    /// <summary>
    /// Makes a panel instantly transparent and optionally sets interactable (false by default)
    /// </summary>
    /// <param name="_canvasGroup">The canvas group to fade</param>
    /// <param name="_interactable">Are we interactable?</param>
    public static void InstantTransparent(CanvasGroup _canvasGroup, bool _interactable = false)
    {
        _canvasGroup.gameObject.SetActive(false);
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = _interactable;
        _canvasGroup.blocksRaycasts = _interactable;
    }
    public static void InstantAlphaValue(CanvasGroup _canvasGroup, float _alpha = 0.1f, bool _interactable = false)
    {
        _canvasGroup.alpha = _alpha;
        _canvasGroup.interactable = _interactable;
        _canvasGroup.blocksRaycasts = _interactable;
    }
}
