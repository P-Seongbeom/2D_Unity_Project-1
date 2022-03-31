using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScrollingBackground : MonoBehaviour
{
    public Transform[] ScrollObject;

    public GameObject SpawnObject;
    public Transform[] SpawnObjects;

    public float ScrollSpeed;
    public float AddSpeed = 0f;
    public float ScrollObjectFrequency;
    public float SpawnObjectFrequency;
    public bool ItemSpawn;

    private float _resetPosition = -12f;
    private bool _spawnedOstacle = false;

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
        if(ItemSpawn)
        {
            ItemSpawner.Instance.InitItem(ScrollObject.Length * 6);

            ItemSpawner.Instance.ApplyPattern(ScrollObject.Length - 1);
        }
    }

    void Update()
    {
        if(GameManager.Instance.GameOver)
        {
            return;
        }

        if(ItemSpawn)
        {
            ItemSpawner.Instance.ScrollItem(ScrollObject.Length - 1, ScrollSpeed, _resetPosition);
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

                    if(ScrollObject[i].gameObject.activeSelf && ItemSpawn)
                    {
                        ItemSpawner.Instance.RespawnItem(reposition, i, _spawnedOstacle);
                    }

                    _spawnedOstacle = false;
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
                _spawnedOstacle = true;
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
        ScrollSpeed += AddSpeed * Time.deltaTime;
    }
}
