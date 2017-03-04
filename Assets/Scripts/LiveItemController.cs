﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LiveItemController : NetworkBehaviour
{

    Animator anim;
    public int live_ammount;
    public RuntimeAnimatorController live_item_big_animator, live_item_small_animator;
    public float itemAvailableWaitTime = 2f;
    AudioClip itemCollectedSoundClip;
    CharacterHealth characterHealth;

    void Awake()
    {
        itemCollectedSoundClip = Resources.Load<AudioClip>("Sounds/LiveItemCollected_clean");
        // Se genera aleatoriamente la cantidad de vida que otorga el item
        live_ammount = Random.Range(10, 30);
        anim = GetComponentInChildren<Animator>();

        // Se muestra el item de vida correspondiente a la cantidad de vida que aporta el item
        if (live_ammount <= 18) { anim.runtimeAnimatorController = live_item_small_animator; }
        else { anim.runtimeAnimatorController = live_item_big_animator; }
        Destroy(gameObject, itemAvailableWaitTime);
    }
    void OnTriggerEnter(Collider other)
    {
        // Si el caracter toca el item, recupera vida
        if (other.tag == "Character")
        {
            characterHealth = other.GetComponent<CharacterHealth>();
            characterHealth.energy = Mathf.Clamp(characterHealth.energy + live_ammount, 0, 99);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(itemCollectedSoundClip, 1);
            Destroy(gameObject);
        }
    }
}
