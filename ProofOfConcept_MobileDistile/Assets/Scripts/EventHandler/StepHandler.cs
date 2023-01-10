using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Step
{
    public string stepName;
    public int stepIndex;

    public TaskCompleted[] stepComponents = new TaskCompleted[0];

}

public class StepHandler : MonoBehaviour
{
    [SerializeField]
    private Step[] steps = new Step[0]; 
    
    [Header("Components"),SerializeField]
    private StepFiller stepFiller;

    private int currentStep = 0;
    private int previousStep = -1;
    private List<TaskCompleted> currentStepComponents = new List<TaskCompleted>();    
    private Coroutine StepTrackingCoroutine;


    private void Start()
    {
        InitializesTrackingSteps();
    }

    public void InitializesTrackingSteps()
    {
        if(StepTrackingCoroutine != null)
        {
            StopCoroutine(StepTrackingCoroutine);
        }
        StepTrackingCoroutine = StartCoroutine(TrackingSteps());
    }

    private void GetCurrentStepComponents(int stepIndex)
    {
        currentStepComponents = new List<TaskCompleted>();

        for (int i = 0; i < steps.Length; i++)
        {
            if(i == currentStep)
            {
                for (int k = 0; k < steps[i].stepComponents.Length; k++)
                {
                    currentStepComponents.Add(steps[i].stepComponents[k]);  
                }
            }
        }
    }
    IEnumerator TrackingSteps()
    {
        while (true)
        {
            if(currentStep != previousStep)
            {
                GetCurrentStepComponents(currentStep);
            }
    
            for (int i = 0; i < currentStepComponents.Count; i++)
            {
                if (currentStepComponents[i].Completed == false)
                {
                    continue;
                }
                else
                {
                    stepFiller.Increament(currentStep + 1);
                    currentStep++;
                }
            }
            yield return null;
        }
    }

}
