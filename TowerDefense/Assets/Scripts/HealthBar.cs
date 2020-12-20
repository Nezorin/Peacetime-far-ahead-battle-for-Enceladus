using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public bool RotateHealthBar = true;
    public Slider sld;
    void Start()
    {
        GetComponentInParent<Health>().OnHealthPercChanged += HealthBarChange;
    }

    void HealthBarChange(float pc)
    {
        sld.value = pc;
    }

    private void LateUpdate()
    {
        if (RotateHealthBar)
        {
        transform.LookAt(Camera.current.transform);
        }
        //transform.Rotate(0, 180, 0);
    }
}
