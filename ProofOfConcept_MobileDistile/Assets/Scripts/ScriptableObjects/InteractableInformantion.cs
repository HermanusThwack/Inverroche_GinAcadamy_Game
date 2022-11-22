using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "My Assets/Interactable Data")]
public class InteractableInformantion : ScriptableObject
{
    [Header("Image")]
    public Texture2D stockImage;

    [Header("Discription"),TextArea()]
    public string interactableDiscription = new string("**");
}
