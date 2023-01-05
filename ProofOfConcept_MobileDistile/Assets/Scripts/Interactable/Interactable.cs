using System.Collections;
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
    private IInteractableAction interactableAction;

    private InteractableMovement interactableMovement;

    /// <summary>
    /// Moving the object around the screen will move the Z axis || Depth closer towards either targetTransform or StartLocation.
    /// </summary>

    [SerializeField]
    public InteractableInformantion interactableData;

    [SerializeField]
    private bool displayBigPanel = true;

    #endregion

    #region private
    private Coroutine grabbedCoroutine;
    private bool isCollectable = false;

    #endregion
    #region Properties
    public InteractableState CurrentState { get => currentState; }

    public IInteractableAction IInteractableAction { get => interactableAction; set => interactableAction = value; }
    public bool IsCollectable { get => IsCollectable; set => isCollectable = value; }

    /// <summary>
    /// For certain interactable target location might need to be able to change.
    /// </summary>

    #endregion

    private void Awake()
    {
        if (gameObject.TryGetComponent<IInteractableAction>(out IInteractableAction _interactableAction))
        {
            interactableAction = _interactableAction;
        }
        else
        {
            Debug.LogError($"Interactable Action Not Found || Please Add a component that is typeof IInteractableAction to {gameObject.name}");
        }

        if (gameObject.TryGetComponent<InteractableMovement>(out InteractableMovement _interactableMovement))
        {
            interactableMovement = _interactableMovement;
        }
        else
        {
            Debug.LogWarning("If Interactable needs to move. || Please Add a compontent that is typeof InteractableMovement.");
        }
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
            case InteractableState.Idle:

                if (interactableMovement != null)
                {
                    InteractController.OnPositionTracking.RemoveListener(interactableMovement.GetTrackedPosition);
                }

                if (grabbedCoroutine == null) return;
                StopCoroutine(grabbedCoroutine);
                break;

            case InteractableState.DisplayingInfo:

                if (interactableMovement != null)
                {
                    InteractController.OnPositionTracking.RemoveListener(interactableMovement.GetTrackedPosition);
                }

                OnDisplayUIPanel.Invoke(interactableData, displayBigPanel);
                break;

            case InteractableState.InteractableSelected:

                // Made this an Interface for more convience 
                interactableAction.Interacted();

                break;

            default:
                break;
        }

        Debug.Log($"State changed to {currentState}");
    }

}




