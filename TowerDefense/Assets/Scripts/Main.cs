using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Main : MonoBehaviour
{
    public Camera MainCamera;
    public Camera StaticBuildCamera;
    public Turret[] turarray;
    public Color highlightColor;
    public Color defaultColor;
    public static bool build_mode = false;
    public static string turname;
    public GameObject buildbprefab;
    public GameObject Backprefab;
    public GameObject defencebprefab;
    static List<GameObject> defencebuttons = new List<GameObject>();
    GameObject buildbutton;
    GameObject BackButton;
    public Canvas canvasprefab;
    public Canvas can;
    public static GameObject turret_to_plant;
    void Start()
    {
        MainCamera.enabled = true;
        StaticBuildCamera.enabled = false;
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
        MainCamera.enabled = false;
        StaticBuildCamera.enabled = true;
        build_mode = true;
        BackButton = Instantiate(Backprefab);
        BackButton.transform.SetParent(can.transform, false);
        Destroy(buildbutton.gameObject);
        CreateDefenceButtons();
        BackButton.GetComponent<Button>().onClick.AddListener(delegate { Destroy(BackButton);
            MainCamera.enabled = true;
            StaticBuildCamera.enabled = false;
            build_mode = false;
            DeleteDefenceButtons();
            buildbutton = Instantiate(buildbprefab);
            buildbutton.transform.SetParent(can.transform, false);
            buildbutton.GetComponent<Button>().onClick.AddListener(delegate { BuildMode(); });
        });
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
        //ResetToDefaultColors();
    }
    void DeleteDefenceButtons()
    {
        foreach (var t in defencebuttons)
        {
            Destroy(t);
        }
        defencebuttons.Clear();
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
            //t.transform.localScale = new Vector3(1.0f/7.0f, 5, 1.0f/7.0f);
            Debug.Log("YEY"); 
        }
    }

    public void ResetToDefaultColors()
    {
        foreach (GameObject bt in defencebuttons)
        {
            ColorBlock cb = bt.GetComponent<Button>().colors;
            cb.normalColor = defaultColor;
            cb.highlightedColor = defaultColor;
            cb.pressedColor = highlightColor;
            bt.GetComponent<Button>().colors = cb;
        }
    }
}
