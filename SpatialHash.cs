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

	public List<Vector2> GetPoints(int x, int y)
	{
		return GetPoints(new Coordinate { X = x, Y = y });
	}

	//public Vector2 FindClosestPoint(int x, int y)
	//{

	//}

	//public Vector2 FindClosestLocalPoint(int x, int y)
	//{
	//	List<Vector2> points = GetPoints(x, y);
	//}
}
