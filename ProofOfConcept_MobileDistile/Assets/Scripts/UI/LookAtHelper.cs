using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtHelper : MonoBehaviour
{
    /// <summary>
    /// Makes the UI look at the camera according to a "refresh rate" = float.
    /// 
    /// When not completly redundent change to speed up refresh rate when interactable is moving! 
    /// </summary>

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float refreshRate = 5.0f;

    private Coroutine lookAtCameraCorotine;

    private void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void Start()
    {
        InitializeLookAtCamera();
    }

    public void InitializeLookAtCamera()
    {
        if (lookAtCameraCorotine != null)
        {
            StopCoroutine(lookAtCameraCorotine);
        }
        lookAtCameraCorotine = StartCoroutine(LookAtCamera());
    }

    IEnumerator LookAtCamera()
    {
        //ToDoCheck where the interactable is relative to the screen and move it based on that.
        while (true)
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
            yield return new WaitForSeconds(refreshRate);
        }

    }

}
