using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpatialPartitioning;

internal class SpatialHash3D
{
	private struct Coordinate
	{
		public int X;
		public int Y;
		public int Z;
	}

	private Dictionary<Coordinate, List<Vector3>> pointMap = new();
	private float hashSize;

	public SpatialHash3D(float hashSize)
	{
		this.hashSize = hashSize;
	}

	private Coordinate GetCoordinate(Vector3 point)
	{
		return new Coordinate
		{
			X = (int)MathF.Floor(point.X / hashSize),
			Y = (int)MathF.Floor(point.Y / hashSize),
			Z = (int)MathF.Floor(point.Z / hashSize)
		};
	}

	public void AddPoint(Vector3 point)
	{
		List<Vector3> points = GetPoints(GetCoordinate(point));
		points.Add(point);
	}

	private List<Vector3> GetPoints(Coordinate coordinate)
	{
		List<Vector3> points;
		if (pointMap.ContainsKey(coordinate))
		{
			points = pointMap[coordinate];
		}
		else
		{
			points = new List<Vector3>();
			pointMap[coordinate] = points;
		}
		return points;
	}

	private Vector3? GetClosestLocalPoint(Coordinate coordinate, Vector3 point, out float closestDistance)
	{
		// Get local space
		List<Vector3> points = GetPoints(coordinate);

		// Initialize search
		closestDistance = float.MaxValue;
		Vector3? closestPoint = null;

		// Find closest local point
		foreach (Vector3 localPoint in points)
		{
			float distance = Vector3.Distance(point, localPoint);
			if (distance < closestDistance)
			{
				closestDistance = distance;
				closestPoint = localPoint;
			}
		}

		// Return closest local point
		return closestPoint;
	}

	public Vector3? GetClosestPoint(Vector3 point)
	{
		// Set up coordinates
		Coordinate pointCoordinate = GetCoordinate(point);
		HashSet<Coordinate> visitedCoordinates = new();

		// Initialize search
		float closestDistance = float.MaxValue;
		float closestCoordinateDistance = float.MaxValue;
		Vector3? closestPoint = null;

		// Search for closest point
		int reach = 0;

		// We must search until the closest point found is the closest possible point searched
		while (reach - 1 < closestCoordinateDistance)
		{
			// Loop coordinates in a square
			for (int x = pointCoordinate.X - reach; x <= pointCoordinate.X + reach; x++)
				for (int y = pointCoordinate.Y - reach; y <= pointCoordinate.Y + reach; y++)
					for (int z = pointCoordinate.Z - reach; z <= pointCoordinate.Z + reach; z++)
					{
						// Check if coordinate has already been searched
						Coordinate searchCoordinate = new Coordinate { X = x, Y = y, Z = z };
						if (visitedCoordinates.Contains(searchCoordinate)) continue;

						// Check closest local point
						Vector3? closestLocalPoint = GetClosestLocalPoint(searchCoordinate, point, out float closestLocalDistance);
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
