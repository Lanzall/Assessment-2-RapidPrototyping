using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class TweenX
{
    /// <summary>
    /// Kills a tweener that may be active
    /// </summary>
    /// <param name="_tweener">The tweener to kill</param>
    public static void KillTweener(Tweener _tweener)
    {
        if (_tweener != null)
            _tweener.Kill();
    }

    public static void TweenTextScale(Transform _textPanel, float _to, float _duration)
    {
        _textPanel.DOScaleX(_to, _duration).SetEase(Ease.InOutSine);
    }
    public static void TweenTextScale(Transform _textPanel, float _to, float _duration, Ease _ease)
    {
        _textPanel.DOScaleX(_to, _duration).SetEase(_ease);
    }

    /// <summary>
    /// Tweens an images fill value Optional On success action
    /// </summary>
    public static void TweenFill(Image _image, float _duration, Ease _ease = Ease.InOutSine, float _toValue = 1)
    {
        _image.DOFillAmount(_toValue, _duration).SetUpdate(true).SetEase(_ease);
    }
    public static void TweenFill(Image _image, float _duration, float _toValue = 1, Action _onComplete = null)
    {
        _image.DOFillAmount(_toValue, _duration).SetUpdate(true).OnComplete(() => _onComplete.Invoke());
    }
    public static void TweenFill(Image _image, float _duration, Ease _ease = Ease.InOutSine, float _toValue = 1, Action _onComplete = null)
    {
        _image.DOFillAmount(_toValue, _duration).SetEase(_ease).SetUpdate(true).OnComplete(()=>_onComplete.Invoke());
    }

    /// <summary>
    /// Tweens an images colour
    /// </summary>
    /// <param name="_image"></param>
    /// <param name="_toColor"></param>
    /// <param name="_duration"></param>
    /// <param name="_ease"></param>
    public static void TweenColor(Image _image, Color _toColor, float _duration = 0.4f, Ease _ease = Ease.InOutSine)
    {
        _image.DOColor(_toColor, _duration).SetEase(_ease);
    }
    /// <summary>
    /// Tweens an images colour
    /// </summary>
    /// <param name="_text"></param>
    /// <param name="_toColor"></param>
    /// <param name="_duration"></param>
    /// <param name="_ease"></param>
    public static void TweenColor(TMPro.TMP_Text _text, Color _toColor, float _duration = 0.4f, Ease _ease = Ease.InOutSine)
    {
        _text.DOColor(_toColor, _duration).SetEase(_ease);
    }
}
