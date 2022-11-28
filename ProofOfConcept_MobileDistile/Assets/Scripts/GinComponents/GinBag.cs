using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;



public class GinBag : MonoBehaviour
{
    #region DataStructures
    [SerializeField]
    private NoteType ResultType;
    #endregion

    #region SerializedFields
    // Additional botanicals
    [SerializeField]
    private List<Interactable> botanicalsAdded = new List<Interactable>();

    [SerializeField]
    private List<Interactable> flavourHintsAdded = new List<Interactable>();
    [SerializeField]
    private bool baseIngredientsAdded = false, botanicalNotesAdded = false, additionalNotesAdded = false;

    #endregion

    #region Private
    private int floralCount = 0, citrusCount = 0, herbalCount = 0, coolCount = 0, spicyCount = 0;
    #endregion


    public void AddBotanical(Interactable interactableAdded)
    {
        Interactable currentInteractable = interactableAdded;

        switch (currentInteractable.CurrentNoteType)
        {

            case NoteType.Floral:
                floralCount++;
                botanicalsAdded.Add(currentInteractable);
                break;
            case NoteType.Citrus:
                citrusCount++;
                botanicalsAdded.Add(currentInteractable);
                break;
            case NoteType.Herbal:
                herbalCount++;
                botanicalsAdded.Add(currentInteractable);
                break;
            case NoteType.Cool:
                coolCount++;
                botanicalsAdded.Add(currentInteractable);
                break;
            case NoteType.Spicy:
                spicyCount++;
                botanicalsAdded.Add(currentInteractable);
                break;

            case NoteType.Rooibos:
                flavourHintsAdded.Add(currentInteractable);
                break;
            case NoteType.Honeybush:
                flavourHintsAdded.Add(currentInteractable);
                break;
            case NoteType.Nutmeg:
                flavourHintsAdded.Add(currentInteractable);
                break;
            case NoteType.ToastedCoconut:
                flavourHintsAdded.Add(currentInteractable);
                break;
            case NoteType.Salt:
                flavourHintsAdded.Add(currentInteractable);
                break;  
            case NoteType.DemeraraSugar:
                flavourHintsAdded.Add(currentInteractable);
                break;
            case NoteType.Raisins:
                flavourHintsAdded.Add(currentInteractable);
                break;


        }

    }


    private void Update()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 0.3f);


        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].TryGetComponent<Interactable>(out Interactable interactable))
            {
                AddBotanical(interactable);
                col[i].gameObject.SetActive(false);
            }
        }
    }

}
