using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemData _itemData;
    public ItemData ItemData { set { _itemData = value; } }

    public void GetInfo()
    {
        Debug.Log("이름 : " + _itemData.ItemName);
        Debug.Log("점수 : " + _itemData.Score);
        Debug.Log("확률 : " + _itemData.SpawnFrequency);
        Debug.Log("최대 수 : " + _itemData.MaxCount);

    }
}


