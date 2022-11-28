using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType
{
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
[CreateAssetMenu(menuName = "My Assets/Interactable Data")]
public class InteractableInformantion : ScriptableObject
{   
    /// <summary>
    /// Any information needed for the items that would be displayed!
    /// </summary>

    [Header("Image")]
    public Texture2D stockImage;

    [Header("Discription"), TextArea()]
    public string interactableDiscription = new string("**");

    public NoteType currentNote;
}
