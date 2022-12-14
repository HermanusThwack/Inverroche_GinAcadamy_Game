using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InteractableMovement handles the movemet for the interactables. It also inherrits from IInteractableAction that allows 
/// you to change out the UI interaction button where needed. 
/// 
/// There are two types of movement currently 
///     1. Moves between two transforms
///     2. Moves based an a raycast point + an offset
/// </summary>

public class InteractableMovement : MonoBehaviour, IInteractableAction
{
    #region SerializeField
    [SerializeField]
    private bool isDragging = false;

    [SerializeField]
    private Transform targetTransform, startTransform;

    [SerializeField]
    private float draggingSpeed = 1f;

    [SerializeField]
    Interactable interactable;

    [SerializeField, Range(0.1f, 0.6f)]
    private float calculatedOffset = 0.2f;

    [SerializeField]
    private bool canMove = true;
    #endregion

    #region Properties
    public Transform StartLocation { get => startTransform; }
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }

    public bool CanMove { get => canMove; set => canMove = value; }

    #endregion

    #region Private
    private Coroutine grabbedCoroutine;
    private Coroutine lerpInteractableCoroutine;

    private RaycastHit hitResult;
    private float speed = 0.5f;

    #endregion

    #region MovingFeatures 

    #region Grabbingfeature

    private void Start()
    {
        //To turn on and off object in runtime and through inspector events if necessary
    }

    /// <summary>
    ///  Interact is called when to move and object based of witch movement type is selected.
    /// </summary>
    
    public void Interacted()
    {
        if (!canMove) return;
        MovementTypeSelection();
    }
    /// <summary>
    /// Initializer for the StartGrabbing Coroutine 
    /// </summary>
    public void MovementTypeSelection()
    {
        InteractController.OnPositionTracking.AddListener(GetTrackedPosition);

        if (isDragging)
            InitializeGrabbing();
        else
            InitializeMoveToTarget();

    }
    public void InitializeGrabbing()
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
        while (true)
        {
            transform.position = RayOffesetPosition();// Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane / (offset + DepthDistance(targetLocation, startLocation))));
            //transform.position += new Vector3(0, 0, DepthDistance(targetLocation, startLocation));
            yield return null;
        }


    }


    #endregion

    #region RaycastAndOffset
    /// <summary>
    /// Adds a offset based of the hit position and add offset based on the normal position.
    /// </summary>
    /// <returns></returns>
    public Vector3 RayOffesetPosition()
    {
        Vector3 desiredPos = new Vector3(0, 0, 0);


        Vector3 calculatedPosition = new Vector3(hitResult.normal.x * calculatedOffset, hitResult.normal.y * calculatedOffset, hitResult.normal.z * calculatedOffset);
        desiredPos = hitResult.point + calculatedPosition;

        return desiredPos;
    }

    public void GetTrackedPosition(RaycastHit _hitResult)
    {

        hitResult = _hitResult;
    }

    #endregion

    #region Lerp
    public void InitializeMoveToTarget()
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

    #endregion
}
