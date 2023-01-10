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

    [SerializeField]
    private Transform stillTop;
    [SerializeField]
    private Transform stillOpenPosition, stillClosedPosition;
    [SerializeField]
    private float smoothTime = 0.2f;

    private Vector3 velocity =  Vector3.zero;

    private Coroutine displayResultCoroutine;

    private bool stillClosed = false;

    public void Interacted()
    {
        if (!stillClosed)
        {
            CloseStill();
        }
        else if(DataManager.Instance.LastPotentialRecipe != null)
        {
            InitializeDisplayResult();
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


    public void CloseStill()
    {
        Debug.Log("Shit is happening !");
        stillTop.position = Vector3.MoveTowards(stillTop.position, stillClosedPosition.position,  smoothTime);
        stillClosed= true;

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
