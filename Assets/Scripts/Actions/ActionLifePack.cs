using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ActionLifePack : MonoBehaviour
{
    public SOInt quantity;
    public GameObject alertNoLifePack;
    public KeyCode keyCode = KeyCode.C;
    
    private void Start()
    {
       quantity = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }

    private void RecoverLife()
    {
        if(quantity.value > 0)
        {
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            Player.Instance.healthBase.ResetLife();
        }
        else
        {
            if (alertNoLifePack != null) alertNoLifePack.SetActive(true);
            Invoke("DeactivateAlert", 1f);
        }
    }

    private void DeactivateAlert()
    {
       if(alertNoLifePack != null) alertNoLifePack.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            RecoverLife();
        }
    }
}
