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
    private Image fillImage;

    [SerializeField]
    private float stepValue = 0.13f;

    [SerializeField]
    private float currentIncrementValue = 0f;

    [SerializeField]
    private float incrementSpeed = 0.4f;
    private float incrementCap;

    private Coroutine StepFillerCouroutine;

    private void Awake()
    {
        incrementCap = 0;
    }
    private void Start()
    {
        StartFillCoroutine();
    }

    public void StartFillCoroutine()
    {
        if(StepFillerCouroutine != null)
        {
            StopCoroutine(StepFillerCouroutine);
        }

        StepFillerCouroutine = StartCoroutine(IncreaseFill());
    }
    public void Increament()
    {
        if(incrementCap >= 1f)
        {
            incrementCap = 0f;
            currentIncrementValue = 0;
            fillImage.fillAmount = currentIncrementValue;
            StartFillCoroutine();
        }
        else if (incrementCap < 1f)
        {
            incrementCap += stepValue;
        }

    }

    IEnumerator IncreaseFill()
    {
        while (currentIncrementValue <= 1f)
        {
            if (currentIncrementValue < incrementCap)
            {
                currentIncrementValue += Time.fixedDeltaTime * incrementSpeed;

                fillImage.fillAmount = currentIncrementValue;
            }
            yield return null;
        }
    }
}
