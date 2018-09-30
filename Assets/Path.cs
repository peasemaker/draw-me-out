using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

	public LineRenderer PathRenderer;

	private List<Vector2> points;

	public void UpdatePath(Vector2 mousePos)
	{
		if (points == null)
		{
			points = new List<Vector2>();
			AddPoint(mousePos);
			return;
		}
		
		AddPoint(mousePos);
		
	}

	private void AddPoint(Vector2 pointPos)
	{
		points.Add(pointPos);

		PathRenderer.positionCount = points.Count;
		PathRenderer.SetPosition(points.Count - 1, pointPos);
	}
}
