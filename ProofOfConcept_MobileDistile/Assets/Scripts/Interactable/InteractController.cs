using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractController : MonoBehaviour
{
    public static UnityEvent<Interactable> OnInteractableFound = new UnityEvent<Interactable>();


    /// <summary>
    /// THIS IS SO THAT MOBILE TOUCH INPUT CAN BE POTENTIALY USED!!!
    /// ------------------------------------------------------------
    /// 
    /// Potential interactable is for the raycast hitting anyInteractable - This will display the ui options: Grab and display info
    /// when grab is selected the potential interactable will become the current interactable untill when its dropped then both currentgrabbedinteractable
    /// and potential interactable becomes null.
    /// </summary>

    private Interactable potentialInteractable, currentInteractable;

    private void OnEnable()
    {
        InteractableStateHandler.OnCurrentInteractableUsed.AddListener(CurrentInteractableFound);
    }

    private void CurrentInteractableFound(Interactable _currentInteractable)
    {
        currentInteractable = _currentInteractable;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable foundedInteractable))
                {
                    potentialInteractable = foundedInteractable;
                    OnInteractableFound.Invoke(potentialInteractable); // ----> Interactable State handler <-----
                    // Potential interactable found 

                    if (currentInteractable != null) return; // Returns there is a intercatable dragged.
                    potentialInteractable.ChangeCurrentState(InteractableState.Interacted ,true);



                }

            }

        }

        /// Find a diffrent wat to release the object 
        /// The idea is to place back in origanal location
        /// Or in tea bag 
        /// Or in distill
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    if (currentInteractable != null)
        //    {
        //        currentInteractable.ChangeCurrentState(InteractableState.Idle);
        //    }
    }




}
