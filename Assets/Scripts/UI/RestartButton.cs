using DG.Tweening;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    private void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        transform.localScale = Vector3.one;
        transform.DOPunchScale(Vector3.one * .2f, 1, 5);
    }
    
    public void Hide()
    {
        transform.DOScale(0, 1);
    }
}
