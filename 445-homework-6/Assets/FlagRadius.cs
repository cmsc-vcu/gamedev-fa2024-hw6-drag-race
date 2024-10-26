using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    public float radius = 2f; // Radius of the circle
    public int numPoints = 50; // Number of points to create the circle
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints + 1; // Plus one to close the circle

        DrawCircle();
    }

    void DrawCircle()
    {
        float angle = 0f;
        float angleStep = 360f / numPoints;

        for (int i = 0; i <= numPoints; i++)
        {
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += angleStep;
        }
    }
}
