using System;
using UnityEngine;

[Serializable]
public struct CellData
{
    [SerializeField] private string _id;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _rotationAngle;
    
    public string Id => _id;
    public Sprite Sprite => _sprite;

    public float RotationAngle => _rotationAngle;
}
