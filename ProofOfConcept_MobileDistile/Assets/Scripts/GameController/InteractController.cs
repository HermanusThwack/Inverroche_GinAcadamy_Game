using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

/// <summary>
/// POTENTIAL MOBILE INPUT FOR WEBGL IS TOUCHES[] AND ACCELERATION
/// ------------------------------------------------------------
/// 
/// Potential interactable is for the raycast hitting anyInteractable - This will display the ui options: Grab and display info
/// when grab is selected the potential interactable will become the current interactable untill when its dropped then both currentgrabbedinteractable
/// and potential interactable becomes null.
/// </summary>

public class InteractController : MonoBehaviour
{


    #region UnityEvents
    public static UnityEvent<Interactable> OnInteractableFound = new UnityEvent<Interactable>();

    // Doing one physics call and then sending data to relavent places as need. ----> Adding listeners in statechanges for interactable.cs
    public static UnityEvent<RaycastHit> OnPositionTracking = new UnityEvent<RaycastHit>();

    #endregion

    #region SerializedFields


    //Does not do api call every frame with getting mousePosition.
    [SerializeField]
    private Camera mainCam;

    [SerializeField]
    private LayerMask interactableLayer, tableLayer;

    [SerializeField]
    private float dragSpeed = 2f;

    [SerializeField]
    private float smoothTime = 0.1f;


    #endregion
    #region private
    // potential intercactable is when you click on the interactable but does select a option, where as current interactable you have selected an option.
    [SerializeField]
    private Interactable potentialInteractable, currentInteractable;


    private Ray ray;
    private RaycastHit hit;

    private Vector3 dragOrigin;
    private Vector3 velocity;

    #endregion

    private void Start()
    {
        if (mainCam != null) return; mainCam.GetComponent<Camera>(); // Assign in inspector.
    }
    private void OnEnable()
    {
        InteractableStateHandler.OnCurrentInteractableUsed.AddListener(CurrentInteractableFound); // <---- Intectactable State Handler
    }

    private void CurrentInteractableFound(Interactable _currentInteractable)
    {
        currentInteractable = _currentInteractable;
    }

    // Might remove this and put into coroutine depinding on performance.
    private void Update()
    {


        ray = mainCam.ScreenPointToRay(Input.mousePosition);

        //Keep looking for intectable untill on is found
        if (currentInteractable == null)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.blue);
                if (Input.GetMouseButtonDown(0))
                {


                    if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable foundedInteractable) && foundedInteractable.isActiveAndEnabled)
                    {
                        potentialInteractable = foundedInteractable;
                        OnInteractableFound.Invoke(potentialInteractable); // ----> Interactable State handler 
                                                                           // Potential interactable found 

                        if (currentInteractable != null) return; // Returns there is a intercatable dragged.
                        potentialInteractable.ChangeCurrentState(InteractableState.Interacted, true);


                    }
                }


            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, tableLayer))
            {
                //Debug.DrawLine(ray.origin, hit.point, Color.red);
                //if (Input.GetMouseButtonDown(0))
                //{
                //    dragOrigin = Input.mousePosition;
                //    return;
                //}

                //if (!Input.GetMouseButton(0)) return;

                //Vector3 pos = mainCam.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
                //Vector3 move = new Vector3(pos.x * dragSpeed, 0, 0);

                //transform.position = Vector3.SmoothDamp(transform.position, transform.position + move, ref velocity, smoothTime);
            }

        }
        //After interactable is found send the position data to that interactable
        else
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tableLayer))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                OnPositionTracking.Invoke(hit); // ----> current selected interactable

                // if interactable is deselected look for a new interactable
                if (currentInteractable.CurrentState != InteractableState.InteractableSelected)
                {
                    ClearInteractable();
                }
            }
        }
    }

    /// <summary>
    /// Clears current interactable
    /// </summary>
    public void ClearInteractable()
    {
        if (currentInteractable == null) return;
        Debug.LogError($"Cleared || Current Interactable {currentInteractable.name}"); currentInteractable = null;
    }

}
