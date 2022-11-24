using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// This is a sequence of unity events inoder to update:
/// UI,             So indictors of what to do next ect.
/// Audio,          Audio to play at right time.
/// Certain states  Incase we want to go back to a previous step 
/// </summary>

public class SequenceBase 
{
    public string stepName = "Insert Step Name Here";
    public UnityEvent ginStep = new UnityEvent();

    public int index = 0; // Index step here
}
