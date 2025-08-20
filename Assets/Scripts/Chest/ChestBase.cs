using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public KeyCode keycodeChest = KeyCode.E;
    public Animator animatorChest;
    public string triggerOpen = "Open";
    public string tagToCompare = "Player";

    [Header("Notification")]
    public GameObject notificationChest;
    public float tweenDuration = .2f;
    public Ease easeChest = Ease.OutBack;

    [Space]
    public ChestItemBase chestItem;

    private bool _isOpenChest = false;
    private float _startScale;

    private void Start()
    {
        _startScale = notificationChest.transform.localScale.x;
        HideNotificationChest();
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        animatorChest.SetTrigger(triggerOpen);
        _isOpenChest = true;
        HideNotificationChest();
        Invoke(nameof(ShowItemChest), 1f);
    }

    private void ShowItemChest()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItems), 1f);
    }

    private void CollectItems()
    {
        chestItem.Collect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_isOpenChest && other.transform.tag == tagToCompare)
        {
            ShowNotificationChest();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isOpenChest && other.transform.tag == tagToCompare)
        {
            HideNotificationChest();
        }
    }

    private void ShowNotificationChest()
    {
        notificationChest.SetActive(true);
        notificationChest.transform.localScale = Vector3.zero;
        notificationChest.transform.DOScale(_startScale, tweenDuration).SetEase(easeChest);
    }

    private void HideNotificationChest()
    {
        notificationChest.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keycodeChest) && notificationChest.activeSelf && !_isOpenChest)
        {
            OpenChest();
        }
    }
}
