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

    public int TypeCount = System.Enum.GetValues(typeof(ItemType)).Length;

    public List<ItemData> ItemDatas;

    public Item item;

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

            GenerateItem((ItemType)i);
        }
    }

    public void GenerateItem(ItemType type)
    {
        for(int i = 0; i < ItemDatas[(int)type].MaxCount; ++i)
        {
            _itemPool[(int)type][i] = Instantiate(_itemPrefab[(int)type]);
            _itemPool[(int)type][i].GetComponent<Item>();
            _itemPool[(int)type][i].SetActive(false);
            _itemPool[(int)type][i].transform.SetParent(transform);
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

    public void OffItem(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(Instance.transform);
    }

}
