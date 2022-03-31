using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;

    public ItemPattern ItemPattern;
    public ItemPattern[] ItemPatterns;

    public bool TypeFixed = false;
    public int FixedType;

    private List<ItemData> _itemInfo;
    private List<int[]> _nextPattern;
    private int _patternType;

    private GameObject[] _createdItems;
    private int createdItemIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitItem(int NumberOfAllItem)
    {
        _itemInfo = new List<ItemData>();

        _itemInfo = ItemPool.Instance.ItemDatas.ToList();

        _itemInfo.Sort(delegate (ItemData a, ItemData b) { return a.SpawnFrequency.CompareTo(b.SpawnFrequency); });

        _createdItems = new GameObject[NumberOfAllItem];
    }

    public void RespawnItem(Vector2 reposition, int index, bool spawnedOblstacle)
    {
        if (ItemPattern == null)
        {
            return;
        }

        ItemPatterns[index - 1].transform.position = reposition;

        _patternType = _nextPattern[_patternType][Random.Range(0, _nextPattern[_patternType].Length)];

        if (spawnedOblstacle)
        {
            _patternType = (int)PatternType.D;
        }

        for (int i = 0; i < ItemPatterns[index - 1].Patterns[_patternType].transform.childCount; ++i)
        {
            if (createdItemIndex == _createdItems.Length - 1)
            {
                createdItemIndex = 0;
            }
            GameObject item = ItemPool.Instance.SetItem(SetItemType());
            item.transform.position = ItemPatterns[index - 1].Patterns[_patternType].transform.GetChild(i).position;

            _createdItems[createdItemIndex] = item;

            ++createdItemIndex;
        }

    }

    public void ScrollItem(int numberOfPattern, float scrollSpeed, float resetPosition)
    {
        for (int i = 0; i < numberOfPattern; ++i)
        {
            ItemPatterns[i].transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }
        for (int i = 0; i < _createdItems.Length; ++i)
        {
            if (_createdItems[i])
            {
                if (false == _createdItems[i].activeSelf)
                {
                    _createdItems[i] = null;
                }
                else
                {
                    _createdItems[i].transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

                    if (_createdItems[i].transform.position.x < resetPosition)
                    {
                        ItemPool.Instance.OffItem(_createdItems[i]);
                        _createdItems[i] = null;
                    }
                }
            }
        }
    }

    public void ApplyPattern(int numberOfPattern)
    {
        ItemPatterns = new ItemPattern[numberOfPattern];

        for (int i = 0; i < numberOfPattern; ++i)
        {
            ItemPatterns[i] = Instantiate(ItemPattern);

            foreach (GameObject child in ItemPatterns[i].Patterns)
            {
                child.SetActive(false);
            }
        }

        _nextPattern = new List<int[]>();

        int[] a = { (int)PatternType.B, (int)PatternType.D, (int)PatternType.E };
        int[] b = { (int)PatternType.C, (int)PatternType.E };
        int[] c = { (int)PatternType.A, (int)PatternType.C, (int)PatternType.E };
        int[] d = { (int)PatternType.B, (int)PatternType.E };
        int[] e = { (int)PatternType.A, (int)PatternType.B, (int)PatternType.C, (int)PatternType.D, (int)PatternType.E };

        _nextPattern.Add(a);
        _nextPattern.Add(b);
        _nextPattern.Add(c);
        _nextPattern.Add(d);
        _nextPattern.Add(e);

        _patternType = Random.Range(0, ItemPattern.Patterns.Count);
    }

    public int SetItemType()
    {
        if (TypeFixed)
        {
            return FixedType;
        }
        else
        {
            float random = Random.Range(0f, 100f);

            for (int i = 0; i < _itemInfo.Count; ++i)
            {
                if (random <= _itemInfo[i].SpawnFrequency)
                {
                    return _itemInfo[i].ItemNumber;
                }
            }

            return _itemInfo.Last().ItemNumber;
        }
    }

    public void ChangeAllItem(int itemNumber,float duration)
    {
        StartCoroutine(ChangeAllItemHelper(itemNumber, duration));
    }

    public IEnumerator ChangeAllItemHelper(int itemNumber, float duration)
    {
        TypeFixed = true;

        FixedType = itemNumber;

        yield return new WaitForSeconds(duration);

        TypeFixed = false;
    }

}
