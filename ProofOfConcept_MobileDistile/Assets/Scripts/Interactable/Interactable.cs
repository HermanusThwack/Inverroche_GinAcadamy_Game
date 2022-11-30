using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;


public enum InteractableState
{
    Idle,
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
    private InteractableMovement interactableMovement;



    /// <summary>
    /// Moving the object around the screen will move the Z axis || Depth closer towards either targetTransform or StartLocation.
    /// </summary>

    [SerializeField]
    private InteractableInformantion interactablePanelInfo;

    [SerializeField]
    private bool displayBigPanel = true;

    [SerializeField]
    private bool draggingMovement = true;
    #endregion

    #region Properties
    public InteractableState CurrentState { get => currentState; }


    /// <summary>
    /// For certain interactable target location might need to be able to change.
    /// </summary>

    #endregion


    #region private
    private Coroutine grabbedCoroutine;

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
            case InteractableState.Idle:
                InteractController.OnPositionTracking.RemoveListener(interactableMovement.GetTrackedPosition); // Find a diffrent way to do this
                if (grabbedCoroutine == null) return;
                StopCoroutine(grabbedCoroutine);
                break;

            case InteractableState.DisplayingInfo:
                OnDisplayUIPanel.Invoke(interactablePanelInfo, displayBigPanel);
                break;

            case InteractableState.InteractableSelected:
                if (draggingMovement)
                {
                    InteractController.OnPositionTracking.AddListener(interactableMovement.GetTrackedPosition);
                    interactableMovement.InitializeGrabbing();
                }
                else
                {
                    interactableMovement.LerpInteractableToTarget();
                }

                break;

            default:
                break;
        }

        Debug.Log($"State changed to {currentState}");
    }

}




