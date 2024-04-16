using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class CombateCuerpoACuerpo : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float da�oGolpe;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float vida;
    [SerializeField] private float tiempoInmunidad = 1f; // Nuevo: tiempo de inmunidad

    private AudioSource audioSource;
    private Animator animator;
    private bool isCooldown = false;
    private bool isImmune = false; // Nuevo: estado de inmunidad
    private float immuneTimer = 0f; // Nuevo: temporizador de inmunidad
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TomarDa�o(float da�o)
    {
        if (!isImmune) // Nuevo: verificar si el jugador no es inmune
        {
            vida -= da�o;

            if (vida <= 0)
            {
                // Reiniciar la escena
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void Danio()
    {
        Collider[] objetos = Physics.OverlapSphere(controladorGolpe.position, radioGolpe);

        foreach (Collider colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                audioSource.Play();
                colisionador.transform.GetComponent<Svero>().RecibirGolpe(da�oGolpe);
            }

            if (colisionador.CompareTag("Gigante"))
            {
                audioSource.Play();
                colisionador.transform.GetComponent<Gigante>().RecibirGolpe(da�oGolpe);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCooldown)
        {
            Golpe();
        }

        // Nuevo: Verificar si se presion� la tecla "x" y no est� en tiempo de inmunidad
        if (Input.GetKeyDown(KeyCode.X) && !isImmune)
        {
            StartImmunity(); // Iniciar inmunidad
        }

        // Nuevo: Actualizar temporizador de inmunidad
        if (isImmune)
        {
            immuneTimer -= Time.deltaTime;
            if (immuneTimer <= 0)
            {
                isImmune = false; // Finalizar inmunidad cuando el temporizador llegue a cero
            }
        }
    }

    private void Golpe()
    {
        animator.SetTrigger("Golpe");
        Danio();

        StartCoroutine(StartCooldown());

        playerMovement.canMove = false;
        Invoke("EnableMovement", 1f);
    }

    private IEnumerator StartCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isCooldown = false;
    }

    private void EnableMovement()
    {
        playerMovement.canMove = true;
    }

    private void StartImmunity() // Nuevo: M�todo para iniciar la inmunidad
    {
        isImmune = true;
        immuneTimer = tiempoInmunidad; // Configurar el temporizador de inmunidad
    }

    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}