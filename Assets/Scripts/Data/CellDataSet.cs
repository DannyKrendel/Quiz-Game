using UnityEngine;

[CreateAssetMenu(fileName = "New Cell Data Set", menuName = "Cell Data Set")]
public class CellDataSet : ScriptableObject
{
    [SerializeField] private CellData[] _cellDataArray;

    public CellData[] CellDataArray => _cellDataArray;
}
