using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using SpatialPartitioning;

internal class QuadtreeTest
{
	private List<Vector2> points = new();
	private Dictionary<Vector2, Vector2> comparisons = new();
	private Quadtree quadtree;

	public QuadtreeTest(int windowSize, int pointCount, int comparisons)
	{
		// Initialize
		quadtree = new(windowSize);
		Random random = new Random();

		// Generate random points
		for (int pointIndex = 0; pointIndex < pointCount; pointIndex++)
		{
			int x = random.Next(windowSize);
			int y = random.Next(windowSize);
			Vector2 point = new(x, y);
			points.Add(point);
			quadtree.AddPosition(point);
		}

		// Generate random comparisons
		for (int comparisonIndex = 0; comparisonIndex < comparisons; comparisons++)
		{
			int x = random.Next(windowSize);
			int y = random.Next(windowSize);
			Vector2 comparisonOrigin = new(x, y);
			Vector2 closestPoint = quadtree.FindClosestPoint(comparisonOrigin).Value;
			this.comparisons[comparisonOrigin] = closestPoint;
		}

		// Need to test distance method
		Console.WriteLine("Test");
	}

	public void Draw()
	{
		// Draw quadrants
		List<Rectangle> quadrants = quadtree.GetQuadrants();
		foreach (Rectangle quadrant in quadrants)
			Raylib.DrawRectangleLinesEx(quadrant, 1, Color.Green);

		// Draw points
		foreach (Vector2 point in points)
			Raylib.DrawCircleV(point, 2, Color.White);

		// Draw comparisons
		foreach (KeyValuePair<Vector2, Vector2> comparison in comparisons)
		{
			Raylib.DrawCircleV(comparison.Key, 2, Color.SkyBlue);
			Raylib.DrawLineEx(comparison.Key, comparison.Value, 1, Color.Orange);
		}
	}
}
