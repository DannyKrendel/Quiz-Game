using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CellUI : MonoBehaviour
{
    [SerializeField] private Cell _cell;
    [SerializeField] private Image _cellImage;
    [SerializeField] private ParticleSystem _particleSystem;

    public void OnCellDataChanged(CellData cellData)
    {
        _cellImage.sprite = cellData.Sprite;
        _cellImage.transform.eulerAngles = Vector3.back * cellData.RotationAngle;
    }

    public void OnCellInitialized(bool isStartOfGame)
    {
        if (isStartOfGame) PlayAppearAnimation();
    }

    public void OnCellClicked()
    {
        _cellImage.transform.DOKill(true);
    }

    public void OnCellAnswered(bool isCorrect)
    {
        if (isCorrect) 
            PlayCorrectAnswerAnimation();
        else 
            PlayWrongAnswerAnimation();
    }
    
    private void PlayAppearAnimation()
    {
        transform.DOPunchScale(Vector3.one * .2f, 1.2f, 5);
    }
    
    private void PlayCorrectAnswerAnimation()
    {
        _cell.CanClick = false;
        
        _particleSystem.Play();
        
        _cellImage.transform.DOPunchScale(Vector3.one * .2f, 1, 5)
            .OnComplete(() => _cell.CanClick = true);
    }
    
    private void PlayWrongAnswerAnimation()
    {
        _cell.CanClick = false;
        _cellImage.transform.DOShakePosition(.5f, new Vector3(15, 0), 10, 1).SetInverted(true)
            .OnComplete(() => _cell.CanClick = true);
    }
}
