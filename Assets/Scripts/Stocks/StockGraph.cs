using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockGraph : MonoBehaviour
{
    private RectTransform panel;
    [SerializeField] private GameObject pointPrefab;
    private LineRenderer lineRenderer;

    private Vector2[] points = new Vector2[48];
    private Vector3[] worldPoints = new Vector3[48];  // World space points

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        panel = gameObject.GetComponent<RectTransform>();
        GeneratePoints();
        DrawGraph();
    }

    void GeneratePoints()
    {
        float panelWidth = panel.rect.width;
        float halfWidth = panelWidth / 2;
        float halfHeight = panel.rect.height / 2;
        float spacing = panelWidth / (points.Length - 1);

        for (int i = 0; i < points.Length; i++)
        {
            float x = (i * spacing) - halfWidth;
            float y = Random.Range(-halfHeight, halfHeight);
            points[i] = new Vector2(x, y);

            // Instantiate and place points on the panel
            GameObject point = Instantiate(pointPrefab, panel);
            point.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

            // Convert UI position to world space
            Vector3 uiPos = new Vector3(x, y, 0);
            worldPoints[i] = panel.TransformPoint(uiPos);

            // Ensure the line is slightly in front of the canvas
            worldPoints[i].z = -100f;
        }
    }

    void DrawGraph()
    {
        lineRenderer.positionCount = worldPoints.Length;
        lineRenderer.useWorldSpace = true;  // Important for Screen Space - Overlay

        for (int i = 0; i < worldPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, worldPoints[i]);
        }
    }
}
