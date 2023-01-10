using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class SequenceEvent
{
    public string EventName = "***_Enter Event Name_***";
    public int index;

    public bool stepsCompleted = false;
    public UnityEvent[] revertTasks = new UnityEvent[0];

}

public class SequenceEventTracker : MonoBehaviour
{
    [SerializeField]
    private SequenceEvent[] steps = new SequenceEvent[0];
}
