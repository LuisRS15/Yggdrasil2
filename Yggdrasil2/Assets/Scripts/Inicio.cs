using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip animacionFinal;
    private bool cambioIniciado = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(EsperarYCambiar());
    }

    private void Update()
    {

    }

    IEnumerator EsperarYCambiar()
    {
        yield return new WaitForSeconds(7f);

        if (!cambioIniciado)
        {
            StartCoroutine(CambiarEscena());
        }
    }

    IEnumerator CambiarEscena()
    {
        cambioIniciado = true;
        animator.SetTrigger("Iniciar");

        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene(2);
    }
}
