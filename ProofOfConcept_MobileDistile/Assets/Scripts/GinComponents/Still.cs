using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Still : MonoBehaviour, IInteractableAction
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private UIHandler uiHandler;

    private Coroutine displayResultCoroutine;


    public void Interacted()
    {
        InitializeDisplayResult();
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
        InteractableInformantion resultData = ScriptableObject.CreateInstance<InteractableInformantion>();

        resultData.interactableDiscription = recipeData;


        yield return new WaitForSeconds(3f);
        uiHandler.GetPanelInformation(resultData, false);
        uiHandler.DisplayPanel();
    }

}
