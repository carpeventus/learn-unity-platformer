using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour {

    private float shakeTime;
    private float startingAmplitude;
    private float shakeTimeTotal;
    private CinemachineVirtualCamera _virtualCamera;

    public static CinemachineShake Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float amplitude, float time) {
        var perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = amplitude;
        startingAmplitude = amplitude;
        shakeTimeTotal = time;
        shakeTime = time;
    }

    // Update is called once per frame
    void Update() {
        if (shakeTime > 0) {
            shakeTime -= Time.deltaTime;
            var perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            // 从大到小
            perlin.m_AmplitudeGain = Mathf.Lerp(startingAmplitude, 0, 1-(shakeTime / shakeTimeTotal));
        }
    }
}
