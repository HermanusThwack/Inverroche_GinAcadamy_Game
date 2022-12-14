using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This script is responsible for the visual side as well as the logic for the deciding what step your on
/// The step manager then reverts all steps neccesarry.
/// </summary>
public class StepFiller : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    Animator animator;

    [SerializeField]
    private int currentIndex = 0;
    #endregion

    #region Private
    private int animationIndex = 0;
    private int lastEnteredIndex = 0;

    #endregion


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
        animator.CrossFade(animationIndex.ToString(), 0.3f);
    }

}
    



