using Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppear : MonoBehaviour
{
    public GameObject enemyAppear;
    public GameObject uiBoss;
    public BossBase bossReference;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            enemyAppear.SetActive(true);
            uiBoss.SetActive(true);
            bossReference.SwitchState(BossAction.WALK);
        }
    }
}
