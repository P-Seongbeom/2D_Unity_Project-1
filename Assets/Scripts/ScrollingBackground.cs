using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Transform Player;

    public Transform[] ScrollObject;

    public GameObject SpawnObject;
    public Transform[] SpawnObjects;

    public float ScrollSpeed;
    public float AddSpeedRate = 0f;
    public float ScrollObjectFrequency;
    public float SpawnObjectFrequency;

    private float _resetPosition = -12f;

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


    void Update()
    {
        if(GameManager.Instance.GameOver)
        {
            return;
        }

        AddScrollSpeed();

        foreach (Transform child in ScrollObject)
        {
            if (child.name != transform.name)
            {
                child.transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime);

                if(child.position.x < (_resetPosition))
                {
                    child.gameObject.SetActive(true);

                    Vector2 reposition = new Vector2(child.position.x + Mathf.Abs(_resetPosition * 2), child.position.y);
                    child.transform.position = reposition;

                    if (SpawnObjects.Length > 0)
                    {
                        int index = Random.Range(1, SpawnObjects.Length);
                        RandomGeneration(child, ScrollObjectFrequency, SpawnObjects[index], SpawnObjectFrequency);
                    }
                    else
                    {
                        RandomGeneration(child, ScrollObjectFrequency);
                    }
                }
            }
        }

        MoveSpawnObject();
    }

    private void RandomGeneration(Transform scrollObject, float scrollFrequency,Transform spawnObject, float spawnFrequency)
    {
        float scrollNum = Random.Range(0, 100);

        if(scrollNum > scrollFrequency)
        {
            scrollObject.gameObject.SetActive(false);
        }
        else
        {
            float spawnNum = Random.Range(0, 100);

            if(spawnNum < spawnFrequency && false == spawnObject.gameObject.activeSelf)
            {
                spawnObject.gameObject.SetActive(true);
                spawnObject.position = scrollObject.position;
            }
        }

    }

    private void RandomGeneration(Transform scrollObject, float scrollFrequency)
    {
        float scrollNum = Random.Range(0, 100);

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
}
