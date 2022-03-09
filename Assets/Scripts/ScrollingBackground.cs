using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Transform[] ScrollObject;

    public GameObject SpawnObject;
    public Transform[] SpawnObjects;

    public float ScrollSpeed;
    public float AddSpeedRate = 0f;
    public float ScrollObjectFrequency;
    public float SpawnObjectFrequency;

    //private Transform[] _selectedObject;

    void Start()
    {
        ScrollObject = GetComponentsInChildren<Transform>();

        if(SpawnObject)
        {
            SpawnObjects = SpawnObject.GetComponentsInChildren<Transform>();

            //_selectedObject = new Transform[ScrollObject.Length - 1];

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

        AddScrollSpeed(AddSpeedRate);

        foreach (Transform child in ScrollObject)
        {
            if (child.name != transform.name)
            {
                child.transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime);

                if(child.position.x < (-9f))
                {
                    child.gameObject.SetActive(true);

                    Vector2 reposition = new Vector2(child.position.x + 18f, child.position.y);
                    child.transform.position = reposition;

                    if (SpawnObjects.Length > 0)
                    {
                        int index = Random.Range(1, SpawnObjects.Length);
                        RandomGeneration(child, ScrollObjectFrequency, SpawnObjects[index], SpawnObjectFrequency);
                        //SpawnObjects[index].position = child.position;
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
        float scrollNum = Random.Range(0, 10);

        if(scrollNum > scrollFrequency)
        {
            scrollObject.gameObject.SetActive(false);
        }
        else
        {
            float spawnNum = Random.Range(0, 10);

            if(spawnNum <= spawnFrequency && false == spawnObject.gameObject.activeSelf)
            {
                spawnObject.gameObject.SetActive(true);
                spawnObject.position = scrollObject.position;
            }
        }

    }

    private void RandomGeneration(Transform scrollObject, float scrollFrequency)
    {
        float scrollNum = Random.Range(0, 10);

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

                if(child.position.x < (-9f))
                {
                    child.gameObject.SetActive(false);
                }
            }

        }
    }

    private void AddScrollSpeed(float rate)
    {
        ScrollSpeed += ScrollSpeed * rate * Time.deltaTime;
    }
}
