using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChangeItem : Item
{
    [SerializeField]
    private ItemData _spawnItem;
    protected override void Activate()
    {
        GameManager.Instance.AddItemScore(_itemData.Score);
        ItemActivate.Instance.ActivateItem(ItemActivate.Instance.ChangeItemType(_spawnItem));
    }
}
