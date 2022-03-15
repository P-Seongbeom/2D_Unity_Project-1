using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScrollingBackground : MonoBehaviour
{
    public Transform[] ScrollObject;

    public GameObject SpawnObject;
    public Transform[] SpawnObjects;

    public ItemPattern ItemPattern;
    public ItemPattern[] ItemPatterns;

    public float ScrollSpeed;
    public float AddSpeedRate = 0f;
    public float ScrollObjectFrequency;
    public float SpawnObjectFrequency;

    private List<ItemData> _itemInfo;
    private List<int> _hitNumber;

    private GameObject[] _createdItems;

    private float _resetPosition = -12f;
    private int createdItemIndex = 0;
    private bool _spawnedObject = false;

    void Awake()
    {
        ScrollObject = GetComponentsInChildren<Transform>();
        
        if(SpawnObject)
        {
            SpawnObjects = SpawnObject.GetComponentsInChildren<Transform>();

            foreach (Transform child in SpawnObjects)
            {
                if(child.name != SpawnObject.name)
                {
                   child.gameObject.SetActive(false);
                }
            }
        }
    }

    void Start()
    {
        if (ItemPattern)
        {
            ItemPatterns = new ItemPattern[ScrollObject.Length - 1];

            for (int i = 0; i < ScrollObject.Length - 1; ++i)
            {
                ItemPatterns[i] = Instantiate(ItemPattern);

                foreach (GameObject child in ItemPatterns[i].Patterns)
                {
                    child.gameObject.SetActive(false);
                }
            }

            _itemInfo = new List<ItemData>();

            _itemInfo = ItemPool.Instance.ItemDatas.ToList<ItemData>();

            _itemInfo.Sort(delegate (ItemData a, ItemData b){ return a.SpawnFrequency.CompareTo(b.SpawnFrequency); });

            _hitNumber = new List<int>();

            _createdItems = new GameObject[ScrollObject.Length * 7];
        }


    }

    void Update()
    {
        if(GameManager.Instance.GameOver)
        {
            return;
        }

        if(ItemPattern)
        {
            ScrollItem();
        }

        AddScrollSpeed();

        for (int i = 1; i < ScrollObject.Length; ++i)
        {
            if (ScrollObject[i].name != transform.name)
            {
                ScrollObject[i].transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime);

                if(ScrollObject[i].position.x < _resetPosition)
                {
                    ScrollObject[i].gameObject.SetActive(true);

                    Vector2 reposition = new Vector2(ScrollObject[i].position.x + Mathf.Abs(_resetPosition * 2), ScrollObject[i].position.y);
                    ScrollObject[i].transform.position = reposition;

                    if (SpawnObjects.Length > 0)
                    {
                        int index = Random.Range(1, SpawnObjects.Length);
                        RandomScrollObjectGeneration(ScrollObject[i], ScrollObjectFrequency, SpawnObjects[(int)index], SpawnObjectFrequency);
                    }
                    else
                    {
                        RandomScrollObjectGeneration(ScrollObject[i], ScrollObjectFrequency);
                    }

                    if(ScrollObject[i].gameObject.activeSelf)
                    {
                        RespawnItem(reposition, i);
                    }

                    _spawnedObject = false;
                }
            }
        }

        MoveSpawnObject();
    }

    private void RandomScrollObjectGeneration(Transform scrollObject, float scrollFrequency,Transform spawnObject, float spawnFrequency)
    {
        float scrollNum = Random.Range(0f, 100f);

        if(scrollNum > scrollFrequency)
        {
            scrollObject.gameObject.SetActive(false);
        }
        else
        {
            float spawnNum = Random.Range(0f, 100f);

            if(spawnNum < spawnFrequency && false == spawnObject.gameObject.activeSelf)
            {
                spawnObject.gameObject.SetActive(true);
                spawnObject.position = scrollObject.position;
                _spawnedObject = true;
            }
        }
    }

    private void RandomScrollObjectGeneration(Transform scrollObject, float scrollFrequency)
    {
        float scrollNum = Random.Range(0f, 100f);

        if (scrollNum > scrollFrequency)
        {
            scrollObject.gameObject.SetActive(false);
        }
    }

    private void MoveSpawnObject()
    {
        foreach(Transform child in SpawnObjects)
        {
            if(child.name != SpawnObject.name && true == child.gameObject.activeSelf)
            {
                child.transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime);

                if(child.position.x < (_resetPosition))
                {
                    child.gameObject.SetActive(false);
                }
            }

        }
    }

    private void AddScrollSpeed()
    {
        ScrollSpeed += ScrollSpeed * AddSpeedRate * Time.deltaTime;
    }

    private void ScrollItem()
    {
        for (int i = 0; i < ScrollObject.Length - 1; ++i)
        {
            ItemPatterns[i].transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime);
        }
        for(int i = 0; i < _createdItems.Length; ++i)
        {
            if(_createdItems[i])
            {
                _createdItems[i].transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime);

                if(_createdItems[i].transform.position.x < _resetPosition)
                {
                    ItemPool.Instance.OffItem(_createdItems[i]);
                    _createdItems[i] = null;
                }
            }    
        }
    }

    private void RespawnItem(Vector2 reposition, int index)
    {
        if(ItemPattern == null)
        {
            return;
        }

        ItemPatterns[index - 1].transform.position = reposition;

        int patternType = Random.Range(0, ItemPattern.Patterns.Count);

        if (_spawnedObject)
        {
            patternType = (int)PatternType.D;
        }

        for (int j = 0; j < ItemPatterns[index - 1].Patterns[patternType].transform.childCount; ++j)
        {
            if (createdItemIndex == ScrollObject.Length * 7)
            {
                createdItemIndex = 0;
            }
            GameObject item = ItemPool.Instance.SetItem(SetItemType());
            item.transform.position = ItemPatterns[index - 1].Patterns[patternType].transform.GetChild(j).position;

            _createdItems[createdItemIndex] = item;

            ++createdItemIndex;
        }

    }

    private ItemType SetItemType()
    {
        float random = Random.Range(0f, 100f);

        for(int i = 0; i < _itemInfo.Count; ++i)
        {
            if(random <= _itemInfo[i].SpawnFrequency)
            {
                return (ItemType)_itemInfo[i].ItemNumber;
            }
        }

        return (ItemType)1;
    }
}
