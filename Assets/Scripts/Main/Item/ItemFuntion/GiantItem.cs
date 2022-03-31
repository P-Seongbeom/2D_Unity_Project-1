using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantItem : Item
{
    public float _giantTime = 5f;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Player");
    }
    protected override void Activate()
    {
        GameManager.Instance.AddItemScore(_itemData.Score);
        _player.GetComponent<PlayerController>().Giantize(_giantTime);
    }
}
