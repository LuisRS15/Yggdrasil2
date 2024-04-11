using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CombateCuerpoACuerpo : MonoBehaviour
{
    private Animator animator;
    public float cooldown = 1f;
    private bool isCooldown = false;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>(); // Busca automáticamente el Animator en el GameObject
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCooldown)
        {
            Golpe();
        }
    }

    void Golpe()
    {
        animator.SetTrigger("Golpe");

        StartCoroutine(StartCooldown());

        playerMovement.canMove = false;
        Invoke("EnableMovement", 1f);
    }

    IEnumerator StartCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldown);

        isCooldown = false;
    }

    void EnableMovement()
    {
        playerMovement.canMove = true;
    }
}