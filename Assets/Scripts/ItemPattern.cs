using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatternType
{
    A, B, C, D, E
}
public class ItemPattern : MonoBehaviour
{
    public List<GameObject> Patterns;

    public void ApplyPattern(Transform[] transform, PatternType type)
    {
        foreach(GameObject pattern in Patterns)
        {
            pattern.SetActive(false);
        }

        Patterns[(int)type].SetActive(true);

        for (int i = 0; i < Patterns[(int)type].transform.childCount; ++i)
        {
            transform[i].position = Patterns[(int)type].transform.GetChild(i).position;
            //Debug.Log($"패턴{type} X : " + _patterns[(int)type].GetChild(i).position.x);
            //Debug.Log($"패턴{type} Y : " + _patterns[(int)type].GetChild(i).position.y);
        }
    }
}
