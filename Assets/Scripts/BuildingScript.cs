using UnityEngine;
using System.Collections;

public class BuildingScript : MonoBehaviour
{

    GameObject troop;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn(GameObject troop)
    {
        float cost = PlayerPrefsX.GetFloatArray(troop.name+"Stat")[0];
        if (PlayerPrefs.GetFloat("money") >= cost)
        {
            Debug.Log(troop.name + " costs " + cost);
            Instantiate(troop, new Vector3(this.transform.position.x + 1, 2, this.transform.position.z + 1), Quaternion.identity);
            Debug.Log("unit spawned at x:" + this.transform.position.x + 1 + "  y: " + 2 + "  :z" + this.transform.position.z + 1);
            PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money")- cost);
        }
        else
        {
            Debug.Log("necisitas " + cost + " dinero");
        }
    }
}
