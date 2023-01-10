using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerScript : MonoBehaviour
{
    [SerializeField]
    private Light lightSource;

    [SerializeField]
    private float flickerRate = 0.2f;

    [SerializeField]
    private float minValue, maxValue;

    private Coroutine lightFlicker;

    private void Start()
    {
        lightFlicker = StartCoroutine(Flicker());        
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            if (lightSource != null && lightSource.gameObject.activeSelf)
            {
                lightSource.intensity = Random.Range(minValue, maxValue);
                yield return new WaitForSeconds(flickerRate);
            }
        }
    }
}
