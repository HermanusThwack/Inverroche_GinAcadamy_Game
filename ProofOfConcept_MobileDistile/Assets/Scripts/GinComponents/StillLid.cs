using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillLid : MonoBehaviour, IInteractableAction
{


    [SerializeField]
    private Transform stillTop;
    [SerializeField]
    private Transform stillClosedPosition;
    [SerializeField]
    private float smoothTime = 0.2f;
    [SerializeField]
    private Still still;

    [SerializeField]
    private TaskCompleted task;

    [SerializeField]
    private Interactable interactable;

    [SerializeField]
    private InteractController interactController;

    private Vector3 velocity = Vector3.zero;

    private bool stillClosed = false;

    private void Start()
    {
        
    }
    public void Interacted()
    {
        interactController.ClearInteractable();
        
        CloseStill();
        still.enabled = true;
        still.GetComponent<Interactable>().ChangeCurrentState(InteractableState.Idle, false);
        this.enabled = false;
        Debug.Log("Everything happened");
    }

    public void CloseStill()
    {

        Debug.Log("Shit is happening !");
        stillTop.position = Vector3.MoveTowards(stillTop.position, stillClosedPosition.position, smoothTime);
        stillClosed = true;
        interactable.IInteractableAction = still;
    }

}
