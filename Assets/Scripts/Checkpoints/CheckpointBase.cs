using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public GameObject uiCheckpointSaved;
    public int key = 1;

    private bool checkpointActived = false;
    private string checkpointKey = "checkpointKey";

    private void OnTriggerEnter(Collider other)
    {
        if(!checkpointActived && other.transform.tag == "Player")
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnIt();
        SaveCheckpoint();
        Invoke(nameof(TurnOffMessage), 1f);
    }

    private void TurnIt()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
        uiCheckpointSaved.SetActive(true);
    }

    private void TurnOffMessage()
    {
        uiCheckpointSaved.SetActive(false);
    }

    private void SaveCheckpoint()
    {
        /*só salva se for um checkpoint superior.
        if(PlayerPrefs.GetInt(checkpointKey, 0) > key)
        {
            PlayerPrefs.SetInt(checkpointKey, key);
        }*/
        CheckpointManager.Instance.SaveCheckpoint(key);
        checkpointActived = true;
    }
}
