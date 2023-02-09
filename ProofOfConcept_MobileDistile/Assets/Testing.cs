using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Testing : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    private Vector2 mouseAxis = Vector2.zero;
    [SerializeField]
    private float rotationSpeed = 2f;
    [SerializeField]
    private float lagTime = 3f;
    [SerializeField]
    private GameObject viewingItem;


    #endregion

    private Vector3 stationaryRotation = Vector3.zero;
    private Coroutine coRateObject;
    private bool rotateBack = false;
    private void Start()
    {
        StartObjectRotation();
    }


    public void StartObjectRotation()
    {

        if (coRateObject != null)
        {
            StopCoroutine(coRateObject);
        }
        coRateObject = StartCoroutine(ObjectRotation());
    }

    IEnumerator ObjectRotation()
    {
        bool waitTime = false;

        while (true)
        {

            float angle = Vector3.Angle(viewingItem.transform.forward, Vector3.forward);
            if (angle != 0 && !Input.GetKey(KeyCode.Mouse0))
            {
                float stepIntervals = rotationSpeed * 2 * Time.deltaTime;

                viewingItem.transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(stationaryRotation.x, stationaryRotation.y, stationaryRotation.z, 1), stepIntervals);
                yield return null;
            }

            if (!Input.GetKey(KeyCode.Mouse0))
            {

                Debug.LogWarning("Key not registered");

                if (angle < -0.1f || angle > 0.1f)
                {
                    float startTime = Time.time;
                    Debug.LogWarning("Rotate Back");
                    if (!waitTime)
                    {
                        while (Time.time - startTime < lagTime)
                        {
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                break;
                            }
                            yield return null;
                        }

                        float stepIntervals = rotationSpeed * 2 * Time.deltaTime;
                        viewingItem.transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(stationaryRotation.x, stationaryRotation.y, stationaryRotation.z, 1), stepIntervals);

                        waitTime = true;
                    }

                }

            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                waitTime = false;
                rotateBack = false;
                mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

                float rotationX = mouseAxis.y * rotationSpeed * Time.deltaTime;
                float rotationY = -mouseAxis.x * rotationSpeed * Time.deltaTime;

                viewingItem.transform.Rotate(rotationX, rotationY, 0f, Space.World);
            }

            yield return null;
        }
    }



}
