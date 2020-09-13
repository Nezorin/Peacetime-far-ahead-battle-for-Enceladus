using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTank : MonoBehaviour
{
    public Transform cam;
    float hor_speed = 50.0f;
    float ver_speed = 4.0f;
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(0, hor_speed * hor * Time.deltaTime));
        transform.position += transform.forward * -ver_speed * ver * Time.deltaTime;
    }
}
