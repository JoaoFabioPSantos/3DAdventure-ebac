using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;
    public float shakeDurationItem = .1f;
    public int shakeForceItem = 1;

    public int amountCoins = 3;
    public GameObject coinPrefab;
    public Transform dropPosition;

    private void OnValidate()
    {
        if(healthBase == null)healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDurationItem, Vector3.up/2, shakeForceItem);
        DropCoins();
    }

    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.position;
        i.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
    }

    private void DropGroupofCoins()
    {
        StartCoroutine(DropGroupOfCoinsCoroutine());
    }

    IEnumerator DropGroupOfCoinsCoroutine()
    {
        for (int i = 0; i < amountCoins; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(.1f);
        }
    }
}
