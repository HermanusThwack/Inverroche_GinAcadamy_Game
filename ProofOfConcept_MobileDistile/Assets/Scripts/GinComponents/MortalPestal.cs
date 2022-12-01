using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalPestal : Collector, IInteractableAction
{

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject ingredientsPrefab;

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0f, 0f);

    private Coroutine processBotanicalsCoroutine;
    public void Interacted()
    {
        if (botanicalsAdded.Count == 0)
        {
            animator.Play("Reset");
            return;
        }
        InitializeBotanicalProcessing();
    }

    public void InitializeBotanicalProcessing()
    {
        if (processBotanicalsCoroutine != null)
        {
            StopCoroutine(processBotanicalsCoroutine);
        }

        processBotanicalsCoroutine = StartCoroutine(ProcessBotanicals());
    }

    IEnumerator ProcessBotanicals()
    {
        animator.CrossFade("CrushIngredients", 0f);


        yield return new WaitForSeconds(3f);

        GameObject spawnedIngredient = Instantiate(ingredientsPrefab);
        spawnedIngredient.transform.position = transform.position + offset;
        spawnedIngredient.transform.rotation = transform.rotation;

        Collector spawnedCollector = spawnedIngredient.GetComponent<Collector>();

        for (int i = 0; i < botanicalsAdded.Count; i++)
        {
            spawnedCollector.AddBotanical(botanicalsAdded[i]);
        }

        botanicalsAdded = new List<Interactable>();

    }

}
