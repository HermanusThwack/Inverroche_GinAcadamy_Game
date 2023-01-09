using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Something of a visual aid
/// </summary>
public class StepFiller : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    private int currentIndex = 0;
    
    
    private int animationIndex = 0;
    private int lastEnteredIndex = 0;

    private Coroutine StepCo;
    private bool playReverse = false;

    private void Start()
    {
        StartFillCoroutine();
    }

    public void StartFillCoroutine()
    {
        if (StepCo != null)
        {
            StopCoroutine(StepCo);
        }

        StepCo = StartCoroutine(ChangeCurrentStep());
    }
    public void Increament(int indexValue)
    {
        if (currentIndex == indexValue) return;
        currentIndex = indexValue;

        if (indexValue > lastEnteredIndex && indexValue != lastEnteredIndex)
        {
            animationIndex = indexValue;
        }
        else
        {
            animationIndex = -(indexValue + 1);
        }

        lastEnteredIndex = currentIndex;
  
    }

    IEnumerator ChangeCurrentStep()
    {

        while (true)
        {
            animator.CrossFade(animationIndex.ToString(), 0f);
            yield return new WaitForSeconds(0.2f);
        }
    }


}
