using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{

	public GameObject PathPrefab;
	public Transform Player;

	private Path activePath;
	private List<Vector2> points; 
	static Vector2 currentPos;
	private int currentIndex;
	private GameObject pathGO;
	private float minDist;

	void Start()
	{
		points = new List<Vector2>();
		currentIndex = 0;
		minDist = .1f;
	}

	void FixedUpdate()
	{
		if (!Input.GetMouseButton(0) && points.Count != 0)
		{
			if (currentIndex == 0)
			{
				Player.position = Vector2.Lerp(Player.position, points[0], 1);
				currentPos = points[1];
				currentIndex += 30;
			}
			if ((Vector2) Player.position != currentPos)
			{
				Player.position = Vector2.Lerp(Player.position, currentPos, 1);
			}
			else
			{
				if (currentIndex < points.Count - 1)
				{
					currentPos = points[currentIndex];
					currentIndex += 30;
				}
				else
				{
					Object.Destroy(pathGO);
				}
				
			}
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			pathGO = Instantiate(PathPrefab);
			activePath = pathGO.GetComponent<Path>();
			
			points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			activePath = null;
		}

		if (activePath != null)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 lastPoint = points.Last();

			float distNew = Vector2.Distance(lastPoint, mousePos);

			if (distNew > .1f)
			{
				int amountPoints = (int)(distNew / minDist);

				if (amountPoints > 0)
				{
					float diffY = (mousePos.y - lastPoint.y) / amountPoints;
					float diffX = (mousePos.x - lastPoint.x) / amountPoints;

					for (int i = 1; i <= amountPoints; i++)
					{
						float newY = lastPoint.y + i * diffY;
						float newX = lastPoint.x + i * diffX;

						Vector2 newPos = new Vector2(newX, newY);
						
						points.Add(newPos);
						activePath.UpdatePath(newPos);
					}
				}
			}
		}
	}
}
