using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform targetToLookAt;
    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;
    public float minDistance = 50f;
    public float maxDistance = 500f;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetToLookAt);
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            if (Vector3.Distance(targetToLookAt.position, transform.position) < minDistance)
                return;
            transform.position += Camera.main.transform.forward * scrollSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // backward
        {
            if (Vector3.Distance(targetToLookAt.position, transform.position) > maxDistance)
                return;
            transform.position += Camera.main.transform.forward * -1 * scrollSpeed;
        }
        if (Input.GetKey("o"))
        {
            transform.position += Camera.main.transform.up * moveSpeed;
        }
        if (Input.GetKey("l"))
        {
            transform.position += Camera.main.transform.up * -1 * moveSpeed;
        }
        if (Input.GetKey(";"))
        {
            transform.position += Camera.main.transform.right * moveSpeed;
        }
        if (Input.GetKey("k"))
        {
            transform.position += Camera.main.transform.right * -1 * moveSpeed;
        }
    }
}