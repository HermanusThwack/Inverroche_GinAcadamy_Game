using UnityEditor;
using UnityEngine;



public enum InteractionMode
{
    GinMaking,
    ObjectViewer

}

public class ModeHandler : MonoBehaviour
{
    #region SerializedFields
    [SerializeField, Header("Interacting Mode")]
    private InteractionMode currentInteractionMode = InteractionMode.GinMaking;

    #endregion

    #region Components
    [SerializeField, Header("Components")]
    private InteractController interactController;
    [SerializeField]
    private ObjectViewerController objectViewerController;
    [SerializeField]
    private GameObject cameraObject;

    #endregion
    #region Properties
    public InteractionMode CurrentInteractingMode { get => currentInteractionMode; }

    #endregion

    private void Awake()
    {
        if(cameraObject == null) { cameraObject = gameObject; }

        ModeSelection();
    }

    private void ModeSelection()
    {
        switch (currentInteractionMode)
        {
            case InteractionMode.GinMaking:
                interactController.enabled = true;
                objectViewerController.enabled = false;
                break;

            case InteractionMode.ObjectViewer:
                objectViewerController.enabled = true;
                interactController.enabled = false;
                break;

            default:
                Debug.LogError("Out of mode mode range.");
                break;
        }
    }

    public void ChangeInteractingMode(InteractionMode newInteractionMode)
    {
        if (currentInteractionMode == newInteractionMode) return;

        Debug.Log($"Interaction mode changed from {currentInteractionMode} to {newInteractionMode}.");
        
        currentInteractionMode = newInteractionMode;
        ModeSelection();


    }
}
