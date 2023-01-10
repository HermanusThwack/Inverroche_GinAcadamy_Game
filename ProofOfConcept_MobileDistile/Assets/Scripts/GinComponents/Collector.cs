using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    /// <summary>
    /// Gin Bag collects a data structure that will be sent to the gin pot 
    /// The result her could also be used to be sent to the server or where ever for the order probably cash it as a potential ginRecipe untill order request!
    /// </summary>

    #region SerializedFields

    /*    [SerializeField]
        public List<Interactable> botanicalsAdded = new List<Interactable>();*/

    [SerializeField]
    private Collider ownCollider;

    [SerializeField]
    private CollectableBy collectionType;

    // Restperiod is for the coroutine to wait before doing another physics call || Sphere size is the physics overlap cirle.
    [SerializeField]
    private float restPeriod = 0.2f, sphereSize = 0.2f;

    [SerializeField, Header("Add ingredients to data manager")]
    private bool trackData = true;

    [Space(2), SerializeField, Header("StepChecker")]
    private bool stepChecker = true;

    #endregion

    #region Coroutine 
    private Coroutine checkForInteractableCoroutine;
    #endregion

    protected virtual void Start()
    {
        InitialiseInteractableCheck();

        Debug.LogWarning($"{gameObject.name} has started collection Coroutine");
    }

    /// <summary>
    /// Adding to the data structure for the order potentially might need to change
    /// </summary>
    /// <param name="interactableAdded"></param>

    public void InitialiseInteractableCheck()
    {
        if (checkForInteractableCoroutine != null)
        {
            StopCoroutine(checkForInteractableCoroutine);
            Debug.LogWarning($"{gameObject.name} has stopped collection Coroutine");
        }

        checkForInteractableCoroutine = StartCoroutine(InteractableCheck());
    }


    /// <summary>
    /// Checks for interactables is n a radius around it and then deactivate them for now.
    /// 
    /// It also updates the potential recipe. And based of the component is collecting base ingredients or not it will rather update the last potential recipe.
    /// </summary>

    IEnumerator InteractableCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(restPeriod);
            Collider[] col = Physics.OverlapSphere(transform.position, sphereSize);

            for (int i = 0; i < col.Length; i++)
            {

                if (col[i] != ownCollider)
                {

                    if (col[i].TryGetComponent<Interactable>(out Interactable interactable))

                        // Not to collect self and or other similar components.
                        if (collectionType == interactable.interactableData.canBeCollectedBy)
                        {
                            interactable.ChangeCurrentState(InteractableState.Idle, false);
                            col[i].gameObject.SetActive(false);

                            // Does not need to add ingredients to data manager.
                            if (trackData)
                            {
                                DataManager.Instance.AddToPotentialRecipe(interactable);
                            }
                            else
                            {
                                DataManager.Instance.UpdatePotentialLastRecipe();
                            }

                            if (stepChecker)
                            {
                                if (col[i].TryGetComponent<TaskCompleted>(out TaskCompleted taskCompleted))
                                {
                                    taskCompleted.CompleteTask();
                                    interactable.ChangeCurrentState(InteractableState.Idle, false);
                                }
                            }

                        }
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, sphereSize);
    }
}

