using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType
{
    NoneNoteType,
    Floral,
    Citrus,
    Herbal,
    Cool,
    Spicy,
    Rooibos,
    Honeybush,
    Nutmeg,
    ToastedCoconut,
    Salt,
    DemeraraSugar,
    Raisins
}

public enum CollectableBy
{
    MortarPestle,
    Teabag,
    Still,
    Container,
    Result,
    None
}

[CreateAssetMenu(menuName = "My Assets/Interactable Data")]
public class InteractableInformantion : ScriptableObject
{
    /// <summary>
    /// Any information needed for the items that would be displayed! 
    /// Uses images for designers to create the visuals according to the CI it will be easier than doing it in
    /// unity.
    /// </summary>

    [Header("Content")]

    public Texture2D stockImage ;
    public Texture2D contentImage;

    /// <summary>
    /// To be impleminted Note Type is currently redundent will be impleminted once a need arrises.
    /// Can be collected by is to be used for physics trigger to not collect wrong items.
    /// </summary>

    [Header("Settings")]

    public string componentName = "** NAME NOT GIVEN **";
    public CollectableBy canBeCollectedBy;
    public NoteType currentNote;
}
