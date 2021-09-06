using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] public GameObject linePrefab;
    public GameObject currLine;

    public LineRenderer lineRender;
    public EdgeCollider2D edgeColl;

    public List<UnityEngine.LineRenderer> allLines;
    public List<UnityEngine.EdgeCollider2D> allEdges;

    [SerializeField] public List<UnityEngine.Vector2> mousePos;


    public static Draw instance;
    public float lineLength;
    [SerializeField] public float maxLength;
    private int currPoint;

    private float temp;
    private bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        allLines = new List<UnityEngine.LineRenderer>();
        allEdges = new List<UnityEngine.EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lineLength < maxLength)
        {
            if (Input.GetMouseButtonDown(0)) //left mouse button
            {
                CreateLine();
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(temp, mousePos[mousePos.Count - 1]) > .1f)
                {
                    UpdateLine(temp);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            allLines.Add(lineRender);
            allEdges.Add(edgeColl);
        }

        if (Input.GetMouseButton(1))
        {
            lineLength = 0;
            for (int i = 0; i < allLines.Count; i++)
            {
                allLines[i].positionCount = 0;
            }
            for (int j = 0; j < allEdges.Count; j++)
            {
                Destroy(allEdges[j]);
            }
            allLines.Clear();
            allEdges.Clear();
        }

        if (mousePos.Count > 2)
        {
            clicked = false;
        }
        else if (mousePos.Count != 0 && !clicked)
        {
            temp = Time.time;
            clicked = true;
        }

        if (Time.time - temp > 0.2f && clicked && mousePos.Count != 0 && allLines.Count != 0 && allEdges.Count != 0)
        {
            clicked = false;
            lineLength = 0;
            allLines[allLines.Count - 1].positionCount = 0;
            Destroy(allEdges[allEdges.Count - 1]);
        }
    }

    void CreateLine()
    {
        currLine = Instantiate(linePrefab, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity); //new line
        lineRender = currLine.GetComponent<LineRenderer>();
        edgeColl = currLine.GetComponent<EdgeCollider2D>();
        mousePos.Clear();

        mousePos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mousePos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRender.SetPosition(0, mousePos[0]);
        lineRender.SetPosition(1, mousePos[1]);
        edgeColl.points = mousePos.ToArray();

        currPoint = 2;
    }

    void UpdateLine(UnityEngine.Vector2 newPos)
    {
        mousePos.Add(newPos);
        lineRender.positionCount++;
        lineRender.SetPosition(lineRender.positionCount - 1, newPos);
        edgeColl.points = mousePos.ToArray();

        lineLength += Vector2.Distance(mousePos[currPoint], mousePos[currPoint - 1]);
    }
}
