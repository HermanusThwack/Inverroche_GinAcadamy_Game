using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableStateHandler : MonoBehaviour
{
    private Interactable currentInteractable;


    private void OnEnable()
    {
        InteractController.OnInteractableFound.AddListener(AssignCurrentInteractable);
 
    }


    private void AssignCurrentInteractable(Interactable _currentInteractable)
    {
        currentInteractable = _currentInteractable;
    }

    public void ChangeToGrab()
    {
        currentInteractable.ChangeCurrentState(InteractableState.Grabbed);
    }
}
