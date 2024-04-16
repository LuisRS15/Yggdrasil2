using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private bool canRoll = true;

    public bool canMove = true;
    public new Transform camera;
    public float speed;
    public float gravity = -9.8f;
    public Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            Vector3 movement = Vector3.zero;

            if (hor != 0 || ver != 0)
            {
                Vector3 forward = camera.forward;
                forward.y = 0;
                forward.Normalize();

                Vector3 right = camera.right;
                right.y = 0;
                right.Normalize();

                Vector3 direction = forward * ver + right * hor;
                direction.Normalize();

                movement = direction * speed * Time.deltaTime;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.2f);

                animator.SetBool("IsWalk", true);
            }
            else
            {
                animator.SetBool("IsWalk", false);
            }

            if (Input.GetKeyDown(KeyCode.X) && canRoll)
            {
                animator.SetTrigger("Rodar");
                canRoll = false;

                Invoke("HabilitarRodar", 1f);
            }

            movement.y += gravity * Time.deltaTime;

            characterController.Move(movement);
        }
    }

    void HabilitarRodar()
    {
        canRoll = true;
    }
}