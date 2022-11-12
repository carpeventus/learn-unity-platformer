using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;
    private AudioSource au;
    private AudioClip pickupCoin;
    private AudioClip throwCoinInTashBin;
    // Start is called before the first frame update

    private void Awake() {
        Instance = this;
    }

    void Start() {
        au = GetComponent<AudioSource>();
        pickupCoin = Resources.Load<AudioClip>("SoundFX/PickCoin");
        throwCoinInTashBin = Resources.Load<AudioClip>("SoundFX/ThrowCoin");
    }

    public void PlayPickupCoinSound() {
        au.PlayOneShot(pickupCoin);
    }
    
    public void PlayThrowCoinInTrashBinSound() {
        au.PlayOneShot(throwCoinInTashBin);

    }
}
