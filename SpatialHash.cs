using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpatialPartitioning;

internal class SpatialHash
{
	private struct Coordinate()
	{
		public int X;
		public int Y;
	}

	private Dictionary<Coordinate, List<Vector2>> pointMap = new();
	private float hashSize;

	public SpatialHash(float hashSize)
	{
		this.hashSize = hashSize;
	}

	private Coordinate GetCoordinate(Vector2 point)
	{
		return new Coordinate
		{
			X = (int)MathF.Floor(point.X / hashSize),
			Y = (int)MathF.Floor(point.Y / hashSize)
		};
	}

	public void AddPoint(Vector2 point)
	{
		List<Vector2> points = GetPoints(GetCoordinate(point));
		points.Add(point);
	}

	private List<Vector2> GetPoints(Coordinate coordinate)
	{
		List<Vector2> points;
		if (pointMap.ContainsKey(coordinate))
		{
			points = pointMap[coordinate];
		}
		else
		{
			points = new List<Vector2>();
			pointMap[coordinate] = points;
		}
		return points;
	}

	private Vector2? GetClosestLocalPoint(Coordinate coordinate, Vector2 point, out float closestDistance)
	{
		// Get local space
		List<Vector2> points = GetPoints(coordinate);

		// Initialize search
		closestDistance = float.MaxValue;
		Vector2? closestPoint = null;
		
		// Find closest local point
		foreach (Vector2 localPoint in points)
		{
			float distance = Vector2.Distance(point, localPoint);
			if (distance < closestDistance)
			{
				closestDistance = distance;
				closestPoint = localPoint;
			}
		}

		// Return closest local point
		return closestPoint;
	}

	public Vector2? GetClosestPoint(Vector2 point)
	{
		// Set up coordinates
		Coordinate pointCoordinate = GetCoordinate(point);
		HashSet<Coordinate> visitedCoordinates = new();

		// Initialize search
		float closestDistance = float.MaxValue;
		float closestCoordinateDistance = float.MaxValue;
		Vector2? closestPoint = null;

		// Search for closest point
		int reach = 0;

		// We must search until the closest point found is the closest possible point searched
		while (reach < closestCoordinateDistance)
		{
			// Loop coordinates in a square
			for (int x = pointCoordinate.X - reach; x <= pointCoordinate.X + reach; x++)
				for (int y = pointCoordinate.Y - reach; y <= pointCoordinate.Y + reach; y++)
				{
					// Check if coordinate has already been searched
					Coordinate searchCoordinate = new Coordinate { X = x, Y = y };
					if (visitedCoordinates.Contains(searchCoordinate)) continue;

					// Check closest local point
					Vector2? closestLocalPoint = GetClosestLocalPoint(searchCoordinate, point, out float closestLocalDistance);
					if (closestLocalDistance < closestDistance)
					{
						closestDistance = closestLocalDistance;
						closestCoordinateDistance = closestDistance / hashSize;
						closestPoint = closestLocalPoint.Value;
					}
				
					// Mark the coordinate as visited
					visitedCoordinates.Add(searchCoordinate);
				}

			// Expand our reach
			reach++;
		}

		// Return the closest point
		return closestPoint;
	}
}
