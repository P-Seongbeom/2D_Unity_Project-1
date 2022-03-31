using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected abstract void Activate();

    [SerializeField]
    protected ItemData _itemData;
    public ItemData ItemData { set { _itemData = value; } }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Activate();

        ItemPool.Instance.OffItem(gameObject);
    }
}


