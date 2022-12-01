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

    [SerializeField]
    public List<Interactable> botanicalsAdded = new List<Interactable>();

    [SerializeField]
    private Collider ownCollider;

    [SerializeField]
    private CollectableBy collectionType;
    /// <summary>
    /// Restperiod is for the coroutine to wait before doing another physics call || Sphere size is the physics overlap cirle.
    /// </summary>
    [SerializeField]
    private float restPeriod = 0.2f, sphereSize = 0.2f;

    #endregion



    #region Coroutine 
    private Coroutine checkForInteractableCoroutine;
    #endregion

    private void Start()
    {
        InitialiseInteractableCheck();
    }

    /// <summary>
    /// Adding to the data structure for the order potentially might need to change
    /// </summary>
    /// <param name="interactableAdded"></param>
    public void AddBotanical(Interactable interactableAdded)
    {
        Interactable currentInteractable = interactableAdded;
        botanicalsAdded.Add(currentInteractable);

    }


    public void InitialiseInteractableCheck()
    {
        if (checkForInteractableCoroutine != null)
        {
            StopCoroutine(checkForInteractableCoroutine);
        }

        checkForInteractableCoroutine = StartCoroutine(InteractableCheck());
    }


    /// <summary>
    /// Checks for interactables is n a radius around it and then deactivate them for now.
    /// </summary>
    /// <returns></returns>
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

                        if (collectionType == interactable.interactablePanelInfo.canBeCollectedBy) { 
                        
                            AddBotanical(interactable);
                            interactable.ChangeCurrentState(InteractableState.Idle, false);
                            col[i].gameObject.SetActive(false);
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

