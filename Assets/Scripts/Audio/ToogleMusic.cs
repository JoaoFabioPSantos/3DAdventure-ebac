using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToogleMusic : MonoBehaviour
{
    private bool isMuted = false;

    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }
}
