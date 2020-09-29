using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class CoinItem : MonoBehaviour
{
    public PlayerCharacterSettings playerCharacterSettings;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<PlayerStats>()){
            if(pickupSound) EazySoundManager.PlaySound(pickupSound);
            playerCharacterSettings.coins++;
            gameObject.SetActive(false);
        }
    }
}
