using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public float MaxHealth = 100;
    public float curHealth;
    float pc;
    public event Action<float> OnHealthPercChanged = delegate { };
    private void Awake()
    {
        curHealth = MaxHealth;
    }

    public void ModifyHealth(float amount)
    {
        curHealth += amount;
        pc = curHealth / MaxHealth;
        OnHealthPercChanged(pc);
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ModifyHealth(-10);
        }
    }
}

