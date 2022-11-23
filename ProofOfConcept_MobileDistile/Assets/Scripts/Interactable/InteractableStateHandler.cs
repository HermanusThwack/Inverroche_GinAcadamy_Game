using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class InteractableStateHandler : MonoBehaviour
{
    /// <summary>
    /// TODO Make a singleton
    /// --------------------------
    /// 
    /// This spawns an UI element at an interactable mainly to display options that Grab/display info 
    /// Also it sets the current interactable being interacted with so that we do not display mutliple instances of interaction UI. 
    ///
    /// </summary>
    /// 


    public static UnityEvent<Interactable> OnCurrentInteractableUsed = new UnityEvent<Interactable>();
    public static UnityEvent OnDisplayUI = new UnityEvent();

    private Interactable currentInteractable;


    private void OnEnable()
    {
        InteractController.OnInteractableFound.AddListener(AssignCurrentInteractable);

    }


    private void AssignCurrentInteractable(Interactable _currentInteractable)
    {
        currentInteractable = _currentInteractable;
        currentInteractable.ChangeCurrentState(InteractableState.Interacted, true);  // -----> Interactable State <------
    }

    public void ChangeToGrab()
    {

        currentInteractable.ChangeCurrentState(InteractableState.Grabbed, false); // -----> Interactable State <------
        OnCurrentInteractableUsed.Invoke(currentInteractable); // -----> Interactable State <------


    }

    public void ChangeToDisplayInfo()
    {
        currentInteractable.ChangeCurrentState(InteractableState.DisplayingInfo, false);// -----> Interactable State <------
        OnCurrentInteractableUsed.Invoke(currentInteractable); // -----> UIHandler <------
        OnDisplayUI.Invoke(); // -----> UIHandler <------

    }

    public void SelectInteractableItem()
    {
        currentInteractable.ChangeCurrentState(InteractableState.InteractableSelected, false);
        OnCurrentInteractableUsed.Invoke(currentInteractable); 
    }
}
