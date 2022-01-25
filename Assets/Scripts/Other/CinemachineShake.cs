using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVC;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        cinemachineVC = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time) 
    {
        CinemachineBasicMultiChannelPerlin cinemachinePerlin = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachinePerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachinePerlin = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachinePerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
