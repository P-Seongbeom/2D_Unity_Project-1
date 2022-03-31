using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChangeItem : Item
{
    [SerializeField]
    private ItemData _ChangeItem;

    public float TypeFixTime = 5f;

    protected override void Activate()
    {
        GameManager.Instance.AddItemScore(_itemData.Score);
        ItemSpawner.Instance.ChangeAllItem(_ChangeItem.ItemNumber, TypeFixTime);
    }
}
