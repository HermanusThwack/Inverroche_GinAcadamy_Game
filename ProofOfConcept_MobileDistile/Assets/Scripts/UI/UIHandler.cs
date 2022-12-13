using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIHandler : MonoBehaviour
{
    /// <summary>
    /// Moves and places UI at current seleceted Interactable
    /// Also plays animation depending on if it needs to display ui or hide.
    /// 
    ///
    /// </summary>

    #region SerializedFields
    [Header("Setting"), SerializeField]
    private float uiYOffset;


    [Header("Animators"), SerializeField]
    private Animator optionHandles, infoPanels;

    [Header("Panel Components"), SerializeField]
    private Image bigPanelImage;

    [SerializeField]
    private Image smallPanelImage;

    [SerializeField]
    private TextMeshProUGUI bigPanelTextArea, smallPanelTextArea;
    #endregion


    #region private 
    private Interactable potentialInteractable, selectedInteractable;

    [SerializeField] // For debugging remove 
    private InteractableInformantion currentInteractableData;
    private bool displayBigPanel;

    private Camera camera;
    #endregion


    #region Coroutines
    Coroutine displayUICoroutine;
    #endregion



    private void OnEnable()
    {
        InteractController.OnInteractableFound.AddListener(MoveInteractableUI);
        Interactable.OnStateChangeDisplayUI.AddListener(HandleDisplayingUI);
        Interactable.OnDisplayUIPanel.AddListener(GetPanelInformation);
        InteractableStateHandler.OnDisplayUI.AddListener(DisplayPanel);

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
        //ToDoCheck where the interactable is relative to the screen and move it based on that.
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
            Vector3 desiredLocation = new Vector3(potentialInteractable.transform.position.x, potentialInteractable.transform.position.y + uiYOffset, potentialInteractable.transform.position.z);

            transform.position = desiredLocation;
        }

    }


    #region Handle showing and hiding UI

    /// <summary>
    /// Handle options UI
    /// </summary>
    /// <param name="showHide"></param>
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

        optionHandles.CrossFade("ChangeTransparency", 0f);
        yield return new WaitForFixedUpdate();
        if (showHide)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            optionHandles.CrossFade("ObjectOptions", 0.1f);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            optionHandles.CrossFade("Empty", 0f);
        }
    }
    /// <summary>
    /// Interactable data for UI panels.
    /// Bool to display big or small panel.
    /// </summary>
    /// <param name="_data"></param>
    /// <param name="_displayBigPanel"></param>
    public void GetPanelInformation(InteractableInformantion _data, bool _displayBigPanel)
    {

        Debug.Log(_data.name + _displayBigPanel);
        currentInteractableData = _data;
        displayBigPanel = _displayBigPanel;

    }

    /// <summary>
    /// Display panel gets data from the interactable and based on a bool displays a big or small UI panel.
    /// </summary>
    public void DisplayPanel()
    {

        if (currentInteractableData != null)
        {
            Debug.Log("Interactable not null " + currentInteractableData);
            if (displayBigPanel)
            {
                if (currentInteractableData.stockImage != null)
                {
                    var newImage = Sprite.Create(currentInteractableData.stockImage, new Rect(0, 0, currentInteractableData.stockImage.width, currentInteractableData.stockImage.height), new Vector2(0, 0));
                    bigPanelImage.sprite = newImage;
                }
                bigPanelTextArea.text = currentInteractableData.interactableDiscription;

                infoPanels.CrossFade("DisplayBigPanel", 0f);

            }
            else
            {

                if (currentInteractableData.stockImage != null)
                {
                    var newImage = Sprite.Create(currentInteractableData.stockImage, new Rect(0, 0, currentInteractableData.stockImage.width, currentInteractableData.stockImage.height), new Vector2(0, 0));
                    smallPanelImage.sprite = newImage;
                }

                smallPanelTextArea.text = currentInteractableData.interactableDiscription;

                infoPanels.CrossFade("DisplaySmallPanel", 0f);
            }
        }
    }
    /// <summary>
    /// Hides the UI Panel based on witch is active
    /// </summary>
    public void HidePanel()
    {

        if (displayBigPanel)
        {
            infoPanels.CrossFade("HideBigPanel", 1f);

        }
        else
        {
            infoPanels.CrossFade("HideSmallPanel", 1f);
        }



    }


    #endregion

}
