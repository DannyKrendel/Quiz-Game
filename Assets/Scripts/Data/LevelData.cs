using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _rows;
    [SerializeField] private int _cols;

    public string Name => _name;
    public int Rows => _rows;
    public int Cols => _cols;
}
