using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItem : Item
{
    protected override void Activate()
    {
        GameManager.Instance.AddItemScore(_itemData.Score);
        ItemActivate.Instance.ActivateItem(ItemActivate.Instance.Flying());
    }
}
