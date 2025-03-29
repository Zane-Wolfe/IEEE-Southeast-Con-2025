using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LineRendererHUD : Graphic
{

    public Vector2Int gridSize;
    public float thickness;

    public List<Vector2> points;

    float width;
    float height;
    float unitWidth;
    float unitHeight;

    public bool generatedData = false;

    [SerializeField] float scalarX = 0.0010f;
    [SerializeField] float scalarY = 0.0012f;
    void GeneratePoints()
    {
        float spacing = 10f;
        float halfWidth = 480f /2;
        float halfHeight = 640f /2;
        for (int i = 0; i < 24; i++)
        {
            float x = (i * spacing) - halfWidth;
            float y = Random.Range(-halfHeight, halfHeight);
            Vector2 newPoint = new Vector2(x*scalarX, y*scalarY);
            Debug.Log(newPoint);
            points.Add(newPoint);
            

            // Instantiate and place points on the panel
            //GameObject point = Instantiate(pointPrefab, panel);
            //point.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        }
    }


    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if(!generatedData) { 
            generatedData = true;
            points.Clear();
            GeneratePoints();
        }

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        unitWidth = width / gridSize.x;
        unitHeight = height / gridSize.y;

        if (points.Count < 2) return;


        float angle = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {

            Vector2 point = points[i];
            Vector2 point2 = points[i + 1];

            if (i < points.Count - 1)
            {
                angle = GetAngle(points[i], points[i + 1]) + 90f;
            }

            DrawVerticesForPoint(point, point2, angle, vh);
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            int index = i * 4;
            vh.AddTriangle(index + 0, index + 1, index + 2);
            vh.AddTriangle(index + 1, index + 2, index + 3);
        }
    }
    public float GetAngle(Vector2 me, Vector2 target)
    {
        //panel resolution go there in place of 9 and 16
        return (float)(Mathf.Atan2(9f * (target.y - me.y), 16f * (target.x - me.x)) * Mathf.Rad2Deg);
    }
    void DrawVerticesForPoint(Vector2 point, Vector2 point2, float angle, VertexHelper vh)
    {
        UIVertex vertex = UIVertex.simpleVert;

        // Set color based on direction
        Color lineColor = point2.y > point.y ? Color.green : Color.red;

        vertex.color = lineColor;

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point2.x, unitHeight * point2.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point2.x, unitHeight * point2.y);
        vh.AddVert(vertex);
    }
}

