using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    int count = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.blue);
                    Debug.Log(interactable.CurrentState);
                    interactable.ChangeCurrentState(InteractableState.Grabbed);
                    Debug.Log($"Hitting target with script {count++}");
                }

            }

        }


    }
}
