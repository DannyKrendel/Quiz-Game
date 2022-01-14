using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CellDataChangedEvent _cellDataChanged;
    [SerializeField] private CellInitializedEvent _cellInitialized;
    [SerializeField] private CellAnsweredEvent _cellAnswered;

    public CellClickedEvent Clicked;

    private CellData _cellData;
    public CellData CellData
    {
        get => _cellData;
        private set
        {
            _cellData = value;
            _cellDataChanged.Invoke(value);
        }
    }

    public int X { get; private set; }
    public int Y { get; private set; }
    public bool CanClick { get; set; } = true;

    public void Initialize(CellData cellData, int x, int y, bool isStartOfGame)
    {
        CellData = cellData;
        X = x;
        Y = y;

        _cellInitialized.Invoke(isStartOfGame);
    }

    public void ReplaceData(CellData cellData)
    {
        CellData = cellData;
    }

    public void OnAnswer(bool isCorrect)
    {
        _cellAnswered.Invoke(isCorrect);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanClick) return;
        
        Clicked.Invoke(this);
    }
}


[Serializable]
public class CellDataChangedEvent : UnityEvent<CellData>
{
}

[Serializable]
public class CellInitializedEvent : UnityEvent<bool>
{
}

[Serializable]
public class CellAnsweredEvent : UnityEvent<bool>
{
}

[Serializable]
public class CellClickedEvent : UnityEvent<Cell>
{
}