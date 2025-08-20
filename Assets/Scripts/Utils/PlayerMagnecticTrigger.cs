using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Items;

public class PlayerMagnecticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemCollectableBase i = other.transform.GetComponent<ItemCollectableBase>();
        if(i != null)
        {
            i.transform.AddComponent<Magnectic>();
        }
    }
}
