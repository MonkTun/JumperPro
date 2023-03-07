using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//This is an unused script. I wanted to create camera shake effect when hit the walls but it turns out to be just distrubing. 
public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    
    private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noiseControl;

    private float shakeStartTime;
    private float shakeDuration;

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        vcam = GetComponent<CinemachineVirtualCamera>();
        noiseControl = vcam.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        print(noiseControl);
    }

    void Update()
    {
        if (shakeStartTime > 0)
        {
            shakeStartTime -= Time.deltaTime;

            if (shakeStartTime <= 0)
            {
                //CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                noiseControl.m_AmplitudeGain = 0;
                noiseControl.m_FrequencyGain = 0;
            }
        }
    }


    public void CameraShake(float duration, float amplitude, float frequency)
    {
        shakeStartTime = Time.time;
        shakeDuration = duration;
        noiseControl.m_FrequencyGain = frequency;
        noiseControl.m_AmplitudeGain = amplitude;
        
    }
}
