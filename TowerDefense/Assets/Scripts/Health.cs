using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public float MaxHealth = 100;
    float curHealth;
    float pc;
    public event Action<float> OnHealthPercChanged = delegate { };
    private void Awake()
    {
        curHealth = MaxHealth;
    }

    public void ModifyHealth(float amount){
        curHealth += amount;
        pc = curHealth / MaxHealth;
        OnHealthPercChanged(pc);
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ModifyHealth(-10);
        }
    }
}

