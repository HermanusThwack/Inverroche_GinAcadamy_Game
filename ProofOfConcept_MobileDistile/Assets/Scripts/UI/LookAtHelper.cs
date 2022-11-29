using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtHelper : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Camera cam;
    private void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        //ToDoCheck where the interactable is relative to the screen and move it based on that.
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
