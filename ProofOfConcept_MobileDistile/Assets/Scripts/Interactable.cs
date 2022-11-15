using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractableState
{
    Idle,
    Grabbed,
    Interacted,
    DisplayingInfo,

}
public class Interactable : MonoBehaviour
{

    #region SerializeFields
    [SerializeField]
    private InteractableState currentState = InteractableState.Idle;
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
        Debug.Log("We Getting There");
        switch (currentState)
        {
            case InteractableState.Grabbed:
                Debug.Log($"State changed to {currentState}");
                GrabbedState();
                break;

            default:
                break;
        }
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




