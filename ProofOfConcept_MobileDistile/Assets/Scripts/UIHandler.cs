using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    /// <summary>
    /// Moves and places UI at current seleceted Interactable
    /// Also plays animation depending on if it needs to display ui or hide.
    /// 
    ///
    /// </summary>

    #region SerializedFields
    [SerializeField]
    private Animator animator;
    #endregion


    #region private 
    private Interactable potentialInteractable, selectedInteractable;

    private Camera camera;
    #endregion


    #region Coroutines
    Coroutine displayUICoroutine;
    #endregion



    private void OnEnable()
    { 
        InteractController.OnInteractableFound.AddListener(MoveInteractableUI); 
        Interactable.OnStateChangeDisplayUI.AddListener(HandleDisplayingUI); 


    }
    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        //ToDoCheck where the interactable is relative to the sceen and move it based on that.
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }
    private void MoveInteractableUI(Interactable foundInteractable)
    {
        //ToDo Maybe add pading to found interactable and adjust position based on that information || Depanding on how for its from the screen padding gets adjusted that way.
        if (selectedInteractable != foundInteractable)
        {
            // Do not excecute this code if the same interactable is selected
            LookAtCamera();
            HandleDisplayingUI(true);
            potentialInteractable = foundInteractable;
            selectedInteractable = foundInteractable;
            Vector3 desiredLocation = new Vector3(potentialInteractable.transform.position.x, potentialInteractable.transform.position.y + 0.22f, potentialInteractable.transform.position.z);

            transform.position = desiredLocation;
        }

    }


    #region Handle showing and hiding UI
    private void HandleDisplayingUI(bool showHide)
    {

        if (displayUICoroutine != null)
        {
            StopCoroutine(displayUICoroutine);
        }

        displayUICoroutine = StartCoroutine(ChangeUIAnimation(showHide));
    }


    IEnumerator ChangeUIAnimation(bool showHide)
    {
        animator.Play("Empty");
        if (showHide)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            animator.Play("ObjectOptions");

        }
        else
        {
            yield return new WaitForEndOfFrame();
            animator.Play("Empty");
        }
    }
    #endregion

}
