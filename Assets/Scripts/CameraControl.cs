using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class CameraControl : MonoBehaviour
{

    public GameObject rightClickCursor;
    public GameObject leftClickCursor;
    public Canvas canvas;
    public Cursor rightCursor;
    public Cursor leftCursor;
    Ray start;
    Ray end;
    float startX;
    float startY;
    float endX;
    float endY;
    bool drawingBox = false;

    public List<GameObject> selected = new List<GameObject>();

    float cursorTime = 0;

    float CameraX = 0;
    float CameraY = 0;
    float CameraZ = 0;

    public Vector3 targetPoint;

    float CameraSpeed = 5;

    // Use this for initialization
    void Start()
    {
        CameraX = gameObject.transform.position.x;
        CameraY = gameObject.transform.position.y;
        CameraZ = gameObject.transform.position.z;
        drawingBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CameraX += CameraSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CameraX -= CameraSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CameraZ += CameraSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CameraZ -= CameraSpeed;
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            CameraY -= CameraSpeed;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            CameraY += CameraSpeed;
        }

        if (rightCursor.Select(KeyCode.Mouse1) && selected.Count > 0)
        {
            GameObject rightSelected = rightCursor.GetSelected();
            //Check for team tag later. This is techdebt that needs to be added later on.
            if (rightSelected != null && rightSelected.CompareTag("Unit"))
            {
                foreach (GameObject selectedObject in selected)
                {
                    Unit selectedUnit = selectedObject.GetComponent(typeof(Unit)) as Unit;
                    selectedUnit.target = rightSelected;
                    selectedUnit.state = (int)Unit.Mode.MoveToTarget;
                }
            }
            else
            {
                foreach (GameObject selectedObject in selected)
                {
                    Unit selectedUnit = selectedObject.GetComponent(typeof(Unit)) as Unit;
                    selectedUnit.Stop();
                    selectedUnit.targetPoint = new Vector3(rightClickCursor.transform.position.x, 0, rightClickCursor.transform.position.z);
                    selectedUnit.state = (int)Unit.Mode.MoveToPoint;
                }
            }
        }

        transform.position = new Vector3(CameraX, CameraY, CameraZ);
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            start = Camera.main.ScreenPointToRay(Input.mousePosition);
            startX = Input.mousePosition.x;
            startY = Input.mousePosition.y;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            endX = Input.mousePosition.x;
            endY = Input.mousePosition.y;
            drawingBox = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            end = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!start.Equals(null))
            {
                selected = BoxSelect.GetSelected(start, end);
            }
            drawingBox = false;
        }

        if (leftCursor.Select(KeyCode.Mouse0))
        {
            GameObject objectSelected = leftCursor.GetSelected();
            if (objectSelected.tag.Equals("Unit"))
            {
                selected.Add(objectSelected);
            }
            Debug.Log(objectSelected);
        }
    }

    void OnGUI()
    {
        if (drawingBox)
        {
            drawRect(startX, startY, endX, endY);
        }
    }

    void drawRect(float x, float y, float x1, float y1)
    {
        float width = x1 - x;
        float height = y1 - y;

        var topX = x;
        var topY = y;

        if (width < 0)
        {
            width *= -1;
            topX = x-width;
        }

        if (height < 0)
        {
            height *= -1;
        }else
        {
            topY = y + height;
        }

        Debug.Log("x: " + x + ", y: " + y + ", width: " + width + ", height: " + height);
        Rect r = new Rect(topX, Screen.height - topY, width, height);
        DrawQuad(r, new Color(0, 0, 255, .5F));

    }

    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
}
