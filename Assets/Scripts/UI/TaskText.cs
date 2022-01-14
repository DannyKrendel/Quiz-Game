using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text), typeof(CanvasGroup))]
public class TaskText : MonoBehaviour
{
    private TMP_Text _text;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.DOFade(1, .7f).From(0);
    }

    public void SetTask(string taskName)
    {
        _text.text = $"Find {taskName}";
    }
}
