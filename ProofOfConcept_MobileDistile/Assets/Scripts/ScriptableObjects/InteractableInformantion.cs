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
    /// </summary>

    [Header("Display Name")]
    public string componentName;

    [Header("Image")]
    public Texture2D stockImage;

    [Header("Discription"), TextArea()]
    public string interactableDiscription = new string("**");

    public CollectableBy canBeCollectedBy;
    public NoteType currentNote;
}
