using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GritoGigante : StateMachineBehaviour
{
    public AudioClip audioClip; // Este será el audio que reproduciremos

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, animator.transform.position);
        }
        else
        {
            Debug.LogWarning("¡El audioClip no está asignado en el inspector de Unity!");
        }
    }
}