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
        Debug.Log("�̸� : " + _itemData.ItemName);
        Debug.Log("���� : " + _itemData.Score);
        Debug.Log("Ȯ�� : " + _itemData.SpawnFrequency);
        Debug.Log("�ִ� �� : " + _itemData.MaxCount);

    }
}


