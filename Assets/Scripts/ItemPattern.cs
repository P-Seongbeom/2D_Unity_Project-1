using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PatternType
{
    A, B, C, D, E
}
public class ItemPattern : MonoBehaviour
{
    //public static ItemPattern Instance;

    [SerializeField]
    private List<Transform> _patterns;

    void Awake()
    {
        //if(Instance == null)
        //{
        //    Instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    public void ApplyPattern(Transform[] transform, PatternType type)
    {
        for (int i = 0; i < 3; ++i)
        {
            transform[i].position = _patterns[(int)type].GetChild(i).position;
            //Debug.Log($"패턴{type} X : " + _patterns[(int)type].GetChild(i).position.x);
            //Debug.Log($"패턴{type} Y : " + _patterns[(int)type].GetChild(i).position.y);
        }
    }
}
