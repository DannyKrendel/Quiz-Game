using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelData[] _levels;
    [SerializeField] private CellDataSet[] _itemDataSets;
    [SerializeField] private AnswerPickedEvent _answerPicked;
    [SerializeField] private LevelChangedEvent _levelChanged;
    [SerializeField] private GameRestartingEvent _gameRestarting;
    [SerializeField] private GameEndedEvent _gameEnded;

    private int _currentLevelIndex;
    private CellData[] _currentCells;
    private HashSet<string> _previousAnswerIds;
    private string _answerId;
    
    private void Start()
    {
        _previousAnswerIds = new HashSet<string>();
        ChangeLevel(0);
    }

    public void CheckAnswer(Cell cell)
    {
        var isCorrectAnswer = cell.CellData.Id == _answerId;
        
        cell.OnAnswer(isCorrectAnswer);
        
        if (isCorrectAnswer)
        {
            StartCoroutine(GoToNextLevelCoroutine());
        }
    }

    public void RestartGame()
    {
        _currentLevelIndex = 0;
        _currentCells = null;
        _previousAnswerIds = new HashSet<string>();
        _answerId = null;
        _gameRestarting.Invoke();
        
        ChangeLevel(0);
    }

    private IEnumerator GoToNextLevelCoroutine()
    {
        yield return new WaitForSeconds(1);
        GoToNextLevel();
    }

    private void GoToNextLevel()
    {
        ChangeLevel(_currentLevelIndex + 1);
    }

    private void ChangeLevel(int index)
    {
        if (index < 0)
        {
            Debug.LogError("Level id was out of bounds!");
            return;
        }

        if (index >= _levels.Length)
        {
            _gameEnded.Invoke();
            return;
        }

        var levelData = _levels[index];
        _currentLevelIndex = index;
        
        var currentCellDataSet = _itemDataSets.RandomIndex();
        _currentCells = currentCellDataSet.CellDataArray.PickUnique(levelData.Rows * levelData.Cols);

        PickAnswer();
        
        _levelChanged.Invoke(levelData, _currentCells);
    }

    private void PickAnswer()
    {
        if (!string.IsNullOrEmpty(_answerId))
            _previousAnswerIds.Add(_answerId);
        
        var availableAnswers = _currentCells.Where(c => !_previousAnswerIds.Contains(c.Id)).ToArray();

        if (availableAnswers.Length == 0)
        {
            Debug.LogError("There were no available answers!");
            return;
        }
        
        _answerId = availableAnswers.RandomIndex().Id;
        _answerPicked.Invoke(_answerId);
    }
}

[Serializable]
public class AnswerPickedEvent : UnityEvent<string>
{
}

[Serializable]
public class LevelChangedEvent : UnityEvent<LevelData, CellData[]>
{
}

[Serializable]
public class GameRestartingEvent : UnityEvent
{
}

[Serializable]
public class GameEndedEvent : UnityEvent
{
}