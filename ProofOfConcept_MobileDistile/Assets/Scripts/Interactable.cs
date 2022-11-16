using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractableState
{
    Idle,
    Grabbed, // Chosen Option Grabbed
    Interacted, // Clicked
    DisplayingInfo,

}
public class Interactable : MonoBehaviour
{

    #region SerializeFields
    [SerializeField]
    private InteractableState currentState = InteractableState.Idle;

    [SerializeField]
    private Animator animator;
    #endregion

    #region Properties
    public InteractableState CurrentState { get => currentState; }
    #endregion


    #region private
    private Coroutine grabbedCoroutine;
    #endregion


    /// <summary>
    /// To change state in code.
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeCurrentState(InteractableState newState)
    {
        if (currentState != newState)
        {
            Debug.Log($"State changing to {newState}");
            currentState = newState;
            CheckState();
        }

    }

    private void CheckState()
    {
        switch (currentState)
        {
            case InteractableState.Interacted:
                animator.Play("ObjectOptions");
                break;
            case InteractableState.Grabbed:
                animator.Play("Empty");
                GrabbedState();
                break;

            default:
                break;
        }

        Debug.Log($"State changed to {currentState}");
    }



    #region Grabbing
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

    #endregion

}




