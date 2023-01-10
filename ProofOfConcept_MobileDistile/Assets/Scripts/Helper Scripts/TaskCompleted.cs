using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCompleted : MonoBehaviour
{
    [SerializeField]
    private bool completed;
    
    public bool Completed { get => completed;}
    public void CompleteTask()
    {
        completed = true;   
    }

    public void TaskReverted()
    {
        completed = false;  
    }
    
}
