using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Turret[] turarray;
    public static bool build_mode = false;
    public static string turname;
    public GameObject buildbprefab;
    public GameObject defencebprefab;
    List<GameObject> defencebuttons = new List<GameObject>();
    public GameObject buildbutton;
    public Canvas canvasprefab;
    public Canvas can;
    public static GameObject turret_to_plant;
    void Start()
    {
        turarray = Resources.LoadAll<Turret>("Defences");
        can = Instantiate(canvasprefab);
        buildbutton = Instantiate(buildbprefab);
        buildbutton.transform.SetParent(can.transform, false);
        buildbutton.GetComponent<Button>().onClick.AddListener(delegate { BuildMode(); });
    }

    
    void Update()
    {

    }

    public void BuildMode()
    {
        //Smena Kamery na vid verh
        build_mode = true;
        Destroy(buildbutton.gameObject);
        CreateDefenceButtons();
    }

    void CreateDefenceButtons()
    {
        foreach (Turret t in turarray)
        {
            defencebuttons.Add(Instantiate(defencebprefab));
            defencebuttons[defencebuttons.Count - 1].transform.SetParent(can.transform, false);
            defencebuttons[defencebuttons.Count - 1].transform.position += new Vector3(0, 20 * defencebuttons.Count, 0);
            defencebuttons[defencebuttons.Count - 1].GetComponent<Button>().GetComponentInChildren<Text>().text = t.name;
            defencebuttons[defencebuttons.Count - 1].GetComponent<Button>().onClick.AddListener(delegate { SelectTurret(defencebuttons[defencebuttons.Count - 1]); });
        }
    }

    void SelectTurret(GameObject b)
    {
        turname = b.GetComponent<Button>().GetComponentInChildren<Text>().text;
    }

    public static void PlantTurret(Transform platform)
    {
        if (turname != null) {
            turret_to_plant = (GameObject)Resources.Load("Defences" + "/" + turname);
            GameObject t = Instantiate(turret_to_plant, platform);
            t.transform.localScale = new Vector3(1.0f/7.0f, 5, 1.0f/7.0f);
            Debug.Log("YEY"); 
        }
    }
}
