using UnityEngine;
using UnityEngine.UI;

public class GridFitter : MonoBehaviour
{
    [SerializeField] private int _rows;
    [SerializeField] private int _cols;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;

    private void OnValidate()
    {
        _gridLayoutGroup.cellSize = new Vector2(
            (_rectTransform.sizeDelta.x - _gridLayoutGroup.padding.left - _gridLayoutGroup.padding.right - _gridLayoutGroup.spacing.x * (_cols - 1)) / _cols, 
            (_rectTransform.sizeDelta.y - _gridLayoutGroup.padding.bottom - _gridLayoutGroup.padding.top - _gridLayoutGroup.spacing.y * (_rows - 1)) / _rows);

    }
}
