using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


/// <summary>
/// Still inherits from collector and the interface interactable action.
/// 
/// This component is responsible for visual displaying the destilling process as well as displaying the result of the distelled product.
/// 
/// For now it takes data from the DataManager instance and feed it to the UiHandler to display UI
/// </summary>
public class Still : MonoBehaviour, IInteractableAction
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private UIHandler uiHandler;


    private Coroutine displayResultCoroutine;

    [SerializeField]
    private TaskCompleted task;

    [SerializeField]
    InteractController interactController;

    private void Start()
    {
        
    }
    public void Interacted()
    {

        interactController.ClearInteractable();

        if (DataManager.Instance.LastPotentialRecipe != null)
        {
            InitializeDisplayResult();
            task.CompleteTask();
        }
    }

    public void InitializeDisplayResult()
    {
        if (displayResultCoroutine != null)
        {
            StopCoroutine(displayResultCoroutine);
        }
        displayResultCoroutine = StartCoroutine(DisplayResult());
    }




    IEnumerator DisplayResult()
    {
        animator.Play("StartStilling");
        string recipeData = DataManager.Instance.GenerateStringResult();
        ResultData resultData = ScriptableObject.CreateInstance<ResultData>();

        resultData.ProcessedResult = recipeData;

        yield return new WaitForSeconds(3f);

        //Feeding data into the data manager
        uiHandler.DisplayRecipeResult(resultData);
    }




}
