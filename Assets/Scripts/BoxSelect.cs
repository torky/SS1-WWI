using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxSelect
{
    int count;
    // Use this for initialization
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    public static List<GameObject> GetSelected(Ray start, Ray end)
    {
        RaycastHit startHit;
        RaycastHit endHit;
        List<GameObject> unitsSelected = new List<GameObject>();
        Physics.Raycast(start, out startHit, 1000);
        Physics.Raycast(end, out endHit, 1000);
        GameObject[] allUnits = GameObject.FindGameObjectsWithTag("Unit");

        foreach (GameObject unit in allUnits)
        {

            float x = unit.transform.position.x;
            float z = unit.transform.position.z;

            if (IsBetween(x, z, startHit, endHit))
            {
                unitsSelected.Add(unit);
                Unit u = unit.GetComponent<Unit>();
                u.listsContainingThis.Add(unitsSelected);
            }
        }

        return unitsSelected;
    }

    static bool IsBetween(float x, float z, RaycastHit startHit, RaycastHit endHit)
    {
        bool isBetween = false;

        float startX = startHit.point.x;
        float startZ = startHit.point.z;
        float endX = endHit.point.x;
        float endZ = endHit.point.z;

        if (startX > endX)
        {
            var temp = startX;
            startX = endX;
            endX = temp;
        }

        if (startZ > endZ)
        {
            var temp = startZ;
            startZ = endZ;
            endZ = temp;
        }

        if (x > startX && x < endX && z > startZ && z < endZ)
        {
            isBetween = true;
        }

        return isBetween;
    }
}
