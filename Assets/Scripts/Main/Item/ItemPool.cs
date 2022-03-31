using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public static ItemPool Instance;

    public List<ItemData> ItemDatas;

    [SerializeField]
    private List<GameObject> _itemPrefab;

    private List<GameObject[]> _itemPool = new List<GameObject[]>();


    void Awake()
    { 
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        for (int i = 0; i < ItemDatas.Count; ++i)
        {
            GameObject[] objects = new GameObject[ItemDatas[i].MaxCount];
            _itemPool.Add(objects);

            GenerateItem(i);
        }
    }

    
    public void GenerateItem(int type)
    {
        for(int i = 0; i < ItemDatas[type].MaxCount; ++i)
        {
            _itemPool[type][i] = Instantiate(_itemPrefab[type]);
            _itemPool[type][i].SetActive(false);
            _itemPool[type][i].transform.SetParent(transform.GetChild(type));
        }
    }

    public GameObject SetItem(int type)
    {
        foreach(GameObject child in _itemPool[type])
        {
            if(false == child.activeSelf)
            {
                child.SetActive(true);
                return child;
            }
        }
        return null;
    }

    public void OffItem(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
