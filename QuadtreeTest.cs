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
	private Quadtree quadtree;

	public QuadtreeTest(int windowSize, int pointCount)
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

		// Need to test distance method
		Console.WriteLine("Test");
	}

	public void Draw()
	{
		// Draw points
		foreach (Vector2 point in points)
			Raylib.DrawCircle((int)point.X, (int)point.Y, 2, Color.White);

		// Draw quadrants
		List<Rectangle> quadrants = quadtree.GetQuadrants();
		foreach (Rectangle quadrant in quadrants)
			Raylib.DrawRectangleLinesEx(quadrant, 1, Color.Green);
	}
}
