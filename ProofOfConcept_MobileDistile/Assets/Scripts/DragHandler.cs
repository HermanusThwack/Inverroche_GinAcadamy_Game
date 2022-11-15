using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{

    private Coroutine _draggingCoroutine;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            StartDrag(ray);
        }
    }

    private void StartDrag(Ray _ray)
    {
        if (_draggingCoroutine != null)
        {
            StopCoroutine(_draggingCoroutine);
        }

        _draggingCoroutine = StartCoroutine(FollowCursor(_ray));
    }
    IEnumerator FollowCursor(Ray _ray)
    {
        Ray ray = _ray;
        RaycastHit hit;

        while (true)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);


            }

            yield return null;
        }

    }
}
