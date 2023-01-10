using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenBurner : MonoBehaviour, IInteractableAction
{

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TaskCompleted taskCompleted;

    public void Interacted()
    {
        animator.CrossFade("TurnOn", 0f);
        taskCompleted.CompleteTask();
    }



    public void ResetActions()
    {
        animator.CrossFade("Empty", 0f);
        taskCompleted.TaskReverted();   
    }
}
