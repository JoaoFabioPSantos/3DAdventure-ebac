using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Items;

public class ChestItemCoin : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coinObject;
    public Ease easeCoinChest = Ease.OutBack;
    public Vector2 randomRange = new Vector2(-2f, 2f);
    public float tweenEndTime = .5f;

    private List<GameObject> _items = new List<GameObject>();

    private bool _collected = false;


    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    [NaughtyAttributes.Button]    
    private void CreateItems()
    {
        for(int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position + Vector3.forward * Random.Range(randomRange.x, randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);
            item.transform.DOScale(0, .2f).SetEase(easeCoinChest).From();
            _items.Add(item);
        }
    }

    [NaughtyAttributes.Button]
    public override void Collect()
    {
        if (_collected) return;
        _collected = true;
     
        foreach (var i in _items)
        {
            i.transform.DOMoveY(2f, tweenEndTime).SetRelative();
            i.transform.DOScale(0, tweenEndTime / 2).SetDelay(tweenEndTime / 2);
            if (ItemManager.Instance != null)
            {
                ItemManager.Instance.AddByType(ItemType.COIN);
            }
            else
            {
                Debug.LogError("ItemManager.Instance está nulo!");
            }
        }
        _items.Clear();
    }
}
