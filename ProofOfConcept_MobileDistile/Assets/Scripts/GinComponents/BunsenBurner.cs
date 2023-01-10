using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenBurner : MonoBehaviour, IInteractableAction
{

    [SerializeField]
    private Animator animator;

    public void Interacted()
    {
        animator.CrossFade("TurnOn", 0f);
    }



    public void ResetActions()
    {
        animator.CrossFade("Empty", 0f);
    }
}
