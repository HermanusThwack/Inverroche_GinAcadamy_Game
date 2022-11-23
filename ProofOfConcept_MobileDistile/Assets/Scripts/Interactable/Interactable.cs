using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public enum InteractableState
{
    Idle,
    Grabbed, // Chosen Option Grabbed
    Interacted, // Clicked
    DisplayingInfo,
    InteractableSelected // Move the interactable to its destination

}

public enum MoveCondition
{
    GrabMovement,
    ArchMovement,
    ClickMovement
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
    private MoveCondition moveCondition = MoveCondition.GrabMovement;

    /// <summary>
    /// Moving the object around the screen will move the Z axis || Depth closer towards either targetTransform or StartLocation.
    /// </summary>
    [SerializeField]
    private Transform targetTransform, startLocation;

    [SerializeField]
    private InteractableInformantion interactablePanelInfo;

    [SerializeField]
    private bool displayBigPanel = true;

    #endregion

    #region Properties
    public InteractableState CurrentState { get => currentState; }

    public Transform StartLocation { get => startLocation; }

    /// <summary>
    /// For certain interactable target location might need to be able to change.
    /// </summary>
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }
    #endregion


    #region private
    private Coroutine grabbedCoroutine;
    private Coroutine lerpInteractableCoroutine;

    private float speed = 0.5f;
    #endregion


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
                GrabbedState();
                break;
            case InteractableState.Idle:

                StopCoroutine(grabbedCoroutine);
                break;

            case InteractableState.DisplayingInfo:
                OnDisplayUIPanel.Invoke(interactablePanelInfo, displayBigPanel);
                break;

            case InteractableState.InteractableSelected:
                LerpInteractableToTarget();
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
        float d = Vector3.Distance(transform.position, Camera.main.transform.position);

        // Item follows mouse
        // Item changes cursor icon to inactive
        while (currentState == InteractableState.Grabbed)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, d));
            yield return null;
        }


    }

    private void GetRelativeOffset(Transform _startPos, Transform _targetPos)
    {

    }


    #endregion



    #region MovingFeatures 

    #region Lerp
    public void LerpInteractableToTarget()
    {
        Vector3 startPosition = new Vector3(startLocation.position.x, startLocation.position.y, startLocation.position.z);
        Vector3 destination = new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z);

        if (lerpInteractableCoroutine != null)
        {
            StopCoroutine(lerpInteractableCoroutine);
        }
        lerpInteractableCoroutine = StartCoroutine(StartLerpInteractable(startPosition, destination, 0f)) ;
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




