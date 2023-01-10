using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GinBag inherits from collector and the interface interactable action.
/// 
/// Gin bag also ovorides start so that is can be deactivated with events. The closing bad method is not just for visual purpose, but
/// also to change the current interactable interactable action.
/// </summary>

public class GinBag : Collector, IInteractableAction
{


    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GinBag thisGinBag;
    [SerializeField]
    private InteractableMovement thisInteractableMovement;
    [SerializeField]
    private Interactable thisInteractable;

    [SerializeField]
    private TaskCompleted taskCompleted;

    private Coroutine closeBagCoroutine;
    public void Interacted()
    {
        if (DataManager.Instance.LastPotentialRecipe.Count == 0) { animator.Play("Reset"); return; }

        if (thisInteractableMovement.enabled) { return; }
        InitializeClosingBag();

        taskCompleted.CompleteTask();
    }

    protected override void Start()
    {
        base.Start();
        //To turn on and off object in runtime and through inspector events if necessary
    }


    public void InitializeClosingBag()
    {
        if (closeBagCoroutine != null)
        {
            StopCoroutine(closeBagCoroutine);
        }

        closeBagCoroutine = StartCoroutine(CloseBag());

    }

    IEnumerator CloseBag()
    {

        animator.CrossFade("ClosingBag", 0f);
        thisInteractableMovement.enabled = true;
        yield return new WaitForSeconds(1f);

        thisGinBag.enabled = false;

        //Changes the interactable for the ui buttons
        thisInteractable.IInteractableAction = thisInteractableMovement;


    }
}
