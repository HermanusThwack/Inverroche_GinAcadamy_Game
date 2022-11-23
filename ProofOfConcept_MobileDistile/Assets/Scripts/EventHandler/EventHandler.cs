using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GinSequence
{
    public string stepName = "Insert Step Name Here";
    public UnityEvent ginStep = new UnityEvent();

    public int index = 0; // Index step here

}
public class EventHandler : MonoBehaviour
{
    public  GinSequence[] sequence = new GinSequence[0];
}
