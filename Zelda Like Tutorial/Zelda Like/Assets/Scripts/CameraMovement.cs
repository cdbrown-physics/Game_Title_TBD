using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //private GameObject player;
    public Transform target;
    public float smoothing = 0.005f;
    //private float xCameraRange = 20.25f;
    //private float yCameraRange = 25.25f;
    public Vector2 maxCameraValues = new Vector2(21.0f, 25.7f);
    public Vector2 minCameraValues = new Vector2(-20.0f, -27.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minCameraValues.x, maxCameraValues.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minCameraValues.y, maxCameraValues.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

        }
    }
}
