using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public string tagToCheck = "Player";
    public Color gizmoColor = Color.yellow;

    public GameObject bossCamera;
    public GameObject enemyToShow;

    private void Awake()
    {
        bossCamera.SetActive(false);
        enemyToShow.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheck)
        {
            TurnCameraOn();
        }
    }

    private void TurnCameraOn()
    {
        bossCamera.SetActive(true);
        enemyToShow.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
}
