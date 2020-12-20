using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlant : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject turret;
    public Transform plantPoint;

    private void OnMouseDown()
    {
        if (Main.build_mode)
        {
            Main.PlantTurret(plantPoint);
        }
    }
}
