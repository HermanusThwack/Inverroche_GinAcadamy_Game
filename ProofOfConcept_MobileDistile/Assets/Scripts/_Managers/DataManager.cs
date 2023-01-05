using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{

    #region SerializedFields
    [SerializeField]
    private List<Interactable> potentialRecipe = new List<Interactable>();

    [SerializeField]
    private List<Interactable> recipe = new List<Interactable>();
    #endregion

    #region Private
    private DataManager dataManager;
    [SerializeField] // remove after debugging
    private List<Interactable> lastPotentialRecipe = new List<Interactable>();
    [SerializeField]
    private List<string> acceptedRecipe = new List<string>(); // Might need to be a dictonary depending on if the client want to include grams and or javascript / what ever language this is going to can read the data. 
    #endregion

    #region Properties
    public List<Interactable> PotentialRecipe { get => potentialRecipe; set => potentialRecipe = value; }
    public List<Interactable> LastPotentialRecipe { get => lastPotentialRecipe; set => lastPotentialRecipe = value; }

    public List<string> AcceptedRecipe { get => acceptedRecipe; }
    #endregion
    private void Awake()
    {
        DataManager.instance = dataManager = this;
    }

    /// <summary>
    /// Adds the ingredient to potential interactable
    /// </summary>
    /// <param name="_currentInteractable"></param>
    public void AddToPotentialRecipe(Interactable _currentInteractable)
    {
        potentialRecipe.Add(_currentInteractable);
    }

    /// <summary>
    /// Updates the potential last recipe
    /// 
    /// This is going to have 2 change for each component almost like a new instance and or maybe add individual list for the part or something
    /// </summary>
    /// <param name="_currentInteractable"></param>
    public void UpdatePotentialLastRecipe()
    {
        for (int i = 0; i < potentialRecipe.Count; i++)
        {
            lastPotentialRecipe.Add(potentialRecipe[i]);
        }
        potentialRecipe = new List<Interactable>();
    }

    //Could change to return and result
    public void GenerateAcceptedRecipe()
    {
   
        for (int i = 0; i < lastPotentialRecipe.Count; i++)
        {
            acceptedRecipe.Add(lastPotentialRecipe[i].interactableData.componentName);
           
        }
    }

    public string GenerateStringResult()
    {
        string resultString = "New recipe";

        if (acceptedRecipe.Count == 0) { GenerateAcceptedRecipe(); }

        resultString = "Your recipe is: ";
        for (int i = 0; i < acceptedRecipe.Count; i++)
        {
            resultString += $"\n {acceptedRecipe[i]}";
        }

        return resultString;
    }
}
