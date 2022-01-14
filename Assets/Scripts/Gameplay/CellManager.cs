using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    [SerializeField] private RectTransform _gridTransform;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private CellClickedEvent _cellClicked;

    private List<Cell> _currentCells;
    private bool _isStartOfGame = true;

    private void Awake()
    {
        _currentCells = new List<Cell>();
    }

    public void SpawnCells(LevelData levelData, CellData[] cellDataArray)
    {
        for (int i = 0, k = 0; i < levelData.Rows; i++)
        {
            for (int j = 0; j < levelData.Cols; j++, k++)
            {
                var foundCell = _currentCells.FirstOrDefault(c => c.X == j && c.Y == i);
                if (foundCell != null)
                {
                    foundCell.ReplaceData(cellDataArray[k]);
                    continue;
                }
                var cell = SpawnCell(j, i, cellDataArray[k]);
                _currentCells.Add(cell);
            }
        }

        _isStartOfGame = false;
    }

    public void ResetCells()
    {
        RemoveCells();
        _isStartOfGame = true;
    }

    private void RemoveCells()
    {
        foreach (var cell in _currentCells)
        {
            Destroy(cell.gameObject);
        }
        _currentCells.Clear();
    }
    
    private Cell SpawnCell(int x, int y, CellData cellData)
    {
        var cell = Instantiate(_cellPrefab, _gridTransform).GetComponent<Cell>();
        cell.Initialize(cellData, x, y, _isStartOfGame);
        cell.Clicked.AddListener(OnCellClicked);
        return cell;
    }

    private void OnCellClicked(Cell cell)
    {
        _cellClicked.Invoke(cell);
    }
}
