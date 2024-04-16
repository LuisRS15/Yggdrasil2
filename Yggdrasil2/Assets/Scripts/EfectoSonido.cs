using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip ataque;
    private float lastAttackTime;
    private float cooldown = 1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastAttackTime = -cooldown;
    }

    private void Update()
    {
        if (!IsGamePaused() && Input.GetMouseButtonDown(0) && Time.time - lastAttackTime >= cooldown)
        {
            audioSource.PlayOneShot(ataque);
            lastAttackTime = Time.time;
        }
    }

    private bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }
}
