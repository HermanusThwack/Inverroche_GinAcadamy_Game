using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractController : MonoBehaviour
{
    public static UnityEvent<Interactable> OnInteractableFound = new UnityEvent<Interactable>();

    private Interactable currentInteractable;


    int count = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    currentInteractable = interactable;
                    OnInteractableFound.Invoke(currentInteractable);

                    currentInteractable.ChangeCurrentState(InteractableState.Interacted);



                }

            }

        }


    }
}
