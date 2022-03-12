using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Cake, Egg, GoldBar, GoldenEgg, Meat, TripleEgg, Wing
}
public class ItemPool : MonoBehaviour
{
    public static ItemPool Instance;

    [SerializeField]
    private List<ItemData> _itemDatas;
    [SerializeField]
    private List<GameObject> _itemPrefab;
    [SerializeField]
    private List<GameObject[]> _itemPool;

    void Awake()
    {

        for (int i = 0; i < _itemDatas.Count; ++i)
        {
            _itemPool[i] = new GameObject[_itemDatas[i].MaxCount];


            GenerateItem((ItemType)i);
        }
    }

    public void GenerateItem(ItemType type)
    {
        for(int i = 0; i < _itemDatas[(int)type].MaxCount; ++i)
        {
            _itemPool[(int)type][i] = Instantiate(_itemPrefab[(int)type]);
            _itemPool[(int)type][i].GetComponent<Item>();
            _itemPool[(int)type][i].SetActive(false);
        }
    }

    public GameObject SetItem(ItemType type)
    {
        foreach(GameObject child in _itemPool[(int)type])
        {
            if(false == child.activeSelf)
            {
                child.SetActive(true);
                return child;
            }
        }

        return null;
    }

    public static void OffItem(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(Instance.transform);
    }

}
