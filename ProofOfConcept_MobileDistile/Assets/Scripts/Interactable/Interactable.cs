using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;


public enum InteractableState
{
    Idle,
    Grabbed, // Chosen Option Grabbed
    Interacted, // Clicked
    DisplayingInfo,
    InteractableSelected // Move the interactable to its destination

}

public class Interactable : MonoBehaviour
{
    #region UnityEvents
    public static UnityEvent<bool> OnStateChangeDisplayUI = new UnityEvent<bool>(); // Display UI Or Do not Display UI;
    public static UnityEvent<InteractableInformantion, bool> OnDisplayUIPanel = new UnityEvent<InteractableInformantion, bool>(); // Info ,bool => Big or Small Panel
    #endregion


    #region SerializeFields
    [SerializeField]
    private InteractableState currentState = InteractableState.Idle;

    [SerializeField]
    private LayerMask targetLayer;

    [SerializeField]
    private Camera cam;

    /// <summary>
    /// Moving the object around the screen will move the Z axis || Depth closer towards either targetTransform or StartLocation.
    /// </summary>
    [SerializeField]
    private Transform targetTransform, startTransform;

    [SerializeField]
    private InteractableInformantion interactablePanelInfo;

    [SerializeField]
    private bool displayBigPanel = true;

    [SerializeField]
    private float draggingSpeed = 1f;

    [SerializeField, Range(0.1f, 0.6f)]
    private float calculatedOffset = 0.3f;
    #endregion

    #region Properties
    public InteractableState CurrentState { get => currentState; }

    public Transform StartLocation { get => startTransform; }

    /// <summary>
    /// For certain interactable target location might need to be able to change.
    /// </summary>
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }

    public NoteType CurrentNoteType { get => currentNoteType; }
    #endregion


    #region private
    private Coroutine grabbedCoroutine;
    private Coroutine lerpInteractableCoroutine;
    private Coroutine depthMovementCoroutine;

    private RaycastHit hitResult;
    private float speed = 0.5f;
    private NoteType currentNoteType;
    #endregion

    private void Awake()
    {
        currentNoteType = interactablePanelInfo.currentNote;
    }

    /// <summary>
    /// new state change state in code. Show Hide -> show or hide ui
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeCurrentState(InteractableState newState, bool showHide)
    {
        if (currentState != newState)
        {
            Debug.Log($"State changing to {newState}");
            currentState = newState;
            CheckState(showHide);
        }

    }

    private void CheckState(bool showHide)
    {
        OnStateChangeDisplayUI.Invoke(showHide);
        Debug.Log($"State Changing : Show or Hide UI {showHide}");
        switch (currentState)
        {
            case InteractableState.Interacted:

                break;
            case InteractableState.Grabbed:
                InteractController.OnPositionTracking.AddListener(GetTrackedPosition);
                GrabbedState();
                break;
            case InteractableState.Idle:
                InteractController.OnPositionTracking.RemoveListener(GetTrackedPosition);
                StopCoroutine(grabbedCoroutine);
                break;

            case InteractableState.DisplayingInfo:
                OnDisplayUIPanel.Invoke(interactablePanelInfo, displayBigPanel);
                break;

            case InteractableState.InteractableSelected:
                LerpInteractableToTarget(); // Change to selected object purpose
                break;

            default:
                break;
        }

        Debug.Log($"State changed to {currentState}");
    }


    // TODO: Add extra features and so on.
    #region Grabbing
    /// TODO Tweek the position of grabbed object remember its default location as well!

    /// <summary>
    /// Initializer for the StartGrabbing Coroutine 
    /// </summary>
    private void GrabbedState()
    {
        if (grabbedCoroutine != null)
        {
            StopCoroutine(grabbedCoroutine);
        }

        grabbedCoroutine = StartCoroutine("StartGrabbing");
    }

    private IEnumerator StartGrabbing()
    {

        Vector3 targetLocation = new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z);
        Vector3 startLocation = new Vector3(startTransform.position.x, startTransform.position.y, startTransform.position.z);
        float offset = Vector3.Distance(Vector3.zero + Vector3.up / 2, Camera.main.transform.position); // Not sure what this is but adding

        // Item follows mouse
        // Item changes cursor icon to inactive
        while (currentState == InteractableState.Grabbed)
        {

            transform.position = RayOffesetPosition();// Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane / (offset + DepthDistance(targetLocation, startLocation))));
            //transform.position += new Vector3(0, 0, DepthDistance(targetLocation, startLocation));
            yield return null;
        }


    }


    #endregion



    #region MovingFeatures 

    #region RaycastAndOffset

    public Vector3 RayOffesetPosition()
    {
        Vector3 desiredPos = new Vector3(0, 0, 0);

        if (currentState == InteractableState.Grabbed)
        {
            Vector3 calculatedPosition = new Vector3(hitResult.normal.x * calculatedOffset, hitResult.normal.y * calculatedOffset, hitResult.normal.z * calculatedOffset);
            desiredPos = hitResult.point + calculatedPosition;
        }


        return desiredPos;
    }

    private void GetTrackedPosition(RaycastHit _hitResult)
    {

        hitResult = _hitResult;
    }

    #endregion

    #region Lerp
    public void LerpInteractableToTarget()
    {
        Vector3 startPosition = new Vector3(startTransform.position.x, startTransform.position.y, startTransform.position.z);
        Vector3 destination = new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z);

        if (lerpInteractableCoroutine != null)
        {
            StopCoroutine(lerpInteractableCoroutine);
        }
        lerpInteractableCoroutine = StartCoroutine(StartLerpInteractable(startPosition, destination, 0f));
    }

    IEnumerator StartLerpInteractable(Vector3 startPosition, Vector3 destination, float fraction)
    {


        while (true)
        {
            if (fraction < 1)
            {
                fraction += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, destination, fraction);
            }

            yield return null;
        }


    }
    #endregion

    #region DepthMovement 
    /// <summary>
    /// Feature not impleminted bugs => Bug fixing
    /// Based of the interactable target location and start location the object will move on the z axis deeper or more shallow.
    /// </summary>
    /// <param name="targertLocation"></param>
    /// <param name="startedLocation"></param>
    /// <returns></returns>
    public float DepthDistance(Vector3 targertLocation, Vector3 startedLocation)
    {
        float depth = transform.position.z;
        Vector3 currentLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        float x = Vector3.Distance(targertLocation, currentLocation); //Distance between where the object is going and where it is.
        float z = Vector3.Distance(startedLocation, currentLocation); // Distance between whera the object started and where it is.

        if (x < z) // x Is smaller that means move toward x
        {

            // MoveCloser

            depth -= draggingSpeed; // speed rework to me same as mouse speed
            if (transform.position.z < targertLocation.z)
            {
                // if we move past target location stop moving in the x direction

                return targertLocation.z;
            }
            return depth;

        }
        else
        {
            //MoveFurther away

            depth += draggingSpeed;
            if (depth > startedLocation.z)
            {
                // if we move past start location stop moving in the x direction

                return startedLocation.z;
            }
            return depth;

        }


    }



    #endregion

    #endregion
}




