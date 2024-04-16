using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gigante : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private Transform objetivo;
    [SerializeField] private float distanciaMaximaParaMirar;

    private Animator animator;
    private bool isDead = false;
    public Rigidbody rb;
    public Transform jugador;

    [Header("Ataque")]

    [SerializeField] private Transform controladorAtaque;

    [SerializeField] private float radioAtaque;

    [SerializeField] private float dañoAtaque;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float distanciaJugador = Vector3.Distance(transform.position, objetivo.position);
        animator.SetFloat("distanciaJugador", distanciaJugador);
    }

    public void RecibirGolpe(float daño)
    {
        if (!isDead)
        {
            vida -= daño;
            if (vida <= 0)
            {
                Muerte();
            }
            else
            {
                animator.SetTrigger("Golpe");
            }
        }
    }

    private void Muerte()
    {
        isDead = true;

        animator.SetTrigger("Muerte");
        Invoke("DestruirObjeto", animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void DestruirObjeto()
    {
        Destroy(gameObject);
    }

    public void MirarJugador()
    {
        if (objetivo != null)
        {
            float distanciaAlObjetivo = Vector3.Distance(transform.position, objetivo.position);
            if (distanciaAlObjetivo <= distanciaMaximaParaMirar)
            {
                Vector3 direccionAlObjetivo = (objetivo.position - transform.position).normalized;

                direccionAlObjetivo.y = 0f;

                direccionAlObjetivo = direccionAlObjetivo.normalized;

                transform.rotation = Quaternion.LookRotation(direccionAlObjetivo);
            }
        }
    }

    public void Ataque()
    {
        Collider[] objetos = Physics.OverlapSphere(controladorAtaque.position, radioAtaque);

        foreach (Collider collision in objetos)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<CombateCuerpoACuerpo>().TomarDaño(dañoAtaque);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}