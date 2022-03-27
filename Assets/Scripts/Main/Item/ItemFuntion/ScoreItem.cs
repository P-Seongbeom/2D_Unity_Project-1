using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : Item
{
    protected override void Activate()
    {
        GameManager.Instance.AddItemScore(_itemData.Score);
    }
}
