using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Fader : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeInDuration = 1;
    [SerializeField] private float _fadeOutDuration = 1;
    [SerializeField] private FadeInEndedEvent _fadeInEnded;
    [SerializeField] private FadeOutEndedEvent _fadeOutEnded;
    
    public void FadeIn()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, _fadeInDuration).From(0).OnComplete(_fadeInEnded.Invoke);
    }
    
    public void FadeOut()
    {
        _canvasGroup.DOFade(0, _fadeOutDuration).From(1)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                _fadeOutEnded.Invoke();
            });
    }
}

[Serializable]
public class FadeInEndedEvent : UnityEvent
{
}

[Serializable]
public class FadeOutEndedEvent : UnityEvent
{
}