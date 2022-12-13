using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

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

    private Coroutine closeBagCoroutine;
    public void Interacted()
    {
        if (DataManager.Instance.LastPotentialRecipe.Count == 0) { animator.Play("Reset"); return; }

        if (thisInteractableMovement.enabled) { return; }
        InitializeClosingBag();
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

        thisInteractable.IInteractableAction = thisInteractableMovement;


    }
}
