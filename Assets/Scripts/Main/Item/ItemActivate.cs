using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivate : MonoBehaviour
{
    public static ItemActivate Instance;

    public ItemType ChangeType;

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

    public void ActivateItem(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }

    public IEnumerator ChangeItemType(ItemData data)
    {
        ItemManager.Instance.TypeFix = true;

        ChangeType = (ItemType)data.ItemNumber;

        yield return new WaitForSeconds(ItemManager.Instance._typeFixTime);

        ItemManager.Instance.TypeFix = false;
    }

    public IEnumerator ChangToGiant()
    {
        if(false == ItemManager.Instance.GiantState)
        {
            ItemManager.Instance.GiantState = true;

            yield return StartCoroutine(ItemManager.Instance.Player.IncreasePlayerSize());

            yield return new WaitForSeconds(ItemManager.Instance._giantTime);

            yield return StartCoroutine(ItemManager.Instance.Player.DecreasePlayerSize());

            ItemManager.Instance.GiantState = false;
        }
    }

    public IEnumerator Flying()
    {
        if(false == ItemManager.Instance.FlyState)
        {
            ItemManager.Instance.FlyState = true;

            //ItemManager.Instance.Player

            yield return new WaitForSeconds(ItemManager.Instance._flyTime);

            ItemManager.Instance.FlyState = false;
        }
    }
}
