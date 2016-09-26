using UnityEngine;
using System.Collections;

public class Cursor: MonoBehaviour
{
    private GameObject selected;
    private float cursorTime = 0;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public bool Select(KeyCode key)
    {
        bool somethingSelected = false;
        if (Input.GetKeyUp(key))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                //cursor marker on rightclick
                gameObject.transform.position = hit.point;
                gameObject.SetActive(true);
                cursorTime = Time.time;
                somethingSelected = true;
                selected = hit.collider.gameObject;
                //Debug.Log(hit.collider.gameObject.ToString());
            }
        }

        //cursor marker timer
        if (gameObject.activeInHierarchy)
        {
            if (Time.time - cursorTime > .5f)
            {
                gameObject.SetActive(false);
            }
        }

        return somethingSelected;
    }

    public GameObject GetSelected()
    {
        return selected;
    }
}
