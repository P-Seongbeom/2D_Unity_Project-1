using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private string _itemName;
    public string ItemName { get { return _itemName; } }

    [SerializeField]
    private float _score;
    public float Score { get { return _score; } }

    [SerializeField]
    private float _spawnFrequency;
    public float SpawnFrequency { get { return _spawnFrequency; } }

    [SerializeField]
    private int _maxCount;
    public int MaxCount { get { return _maxCount; } }
}
