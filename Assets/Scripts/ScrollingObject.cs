using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public Transform[] ScrollObject;
    public float ScrollSpeed;
    public float AddSpeedRate = 0f;
    public float GenerateFrequncy;

    void Start()
    {
        ScrollObject = GetComponentsInChildren<Transform>();
    }


    void Update()
    {
        if(GameManager.Instance.GameOver)
        {
            return;
        }

        //AddScrollSpeed(AddSpeedRate);

        //foreach (Transform child in ScrollObject)
        //{
        //    if (child.name != transform.name)
        //    {
        //        child.transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime);

        //        if(child.transform.position.x < (-9f))
        //        {
        //            child.gameObject.SetActive(true);

        //            Vector2 reposition = new Vector2(child.transform.position.x + 18f, child.transform.position.y);

        //            child.transform.position = reposition;

        //            RandomGeneration(child, GenerateFrequncy);
        //        }
        //    }
        //}
    }

    private void RandomGeneration(Transform transform, float frequency)
    {
        float num = Random.Range(0, 10);

        if(num > frequency)
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void AddScrollSpeed(float rate)
    {
        ScrollSpeed += ScrollSpeed * rate * Time.deltaTime;
    }


}
