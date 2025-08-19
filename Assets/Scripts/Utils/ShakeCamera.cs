using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.Core.Singleton;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;

    [Header("Shake Values")]
    public float shakeTime;
    public float frequencyShake = 3f;
    public float amplitudeShake = 3f;
    public float time = 0.2f;

    private CinemachineBasicMultiChannelPerlin c;

    public void Shake()
    {
        Shake(amplitudeShake, frequencyShake, time);
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        c = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        c.m_AmplitudeGain = amplitude;
        c.m_FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if(c != null)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
            }
            else
            {
                c.m_AmplitudeGain = 0f;
                c.m_FrequencyGain = 0f;
            }
        }
    }
}
