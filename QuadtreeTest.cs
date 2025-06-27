using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using SpatialPartitioning;

using Color = Raylib_cs.Color;

internal class QuadtreeTest
{
	private List<Vector2> points = new();
	private Quadtree quadtree;

	public QuadtreeTest(int windowSize, int pointCount, int comparisonCount)
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
			quadtree.AddPoint(point);
		}
	}

	public void Draw()
	{
		// Draw quadrants
		List<RectangleF> quadrants = quadtree.GetQuadrants();
		foreach (RectangleF quadrant in quadrants)
		{
			Raylib.DrawRectangleLines((int)quadrant.X, (int)quadrant.Y, (int)quadrant.Width, (int)quadrant.Height, Color.Green);
		}

		// Draw points
		foreach (Vector2 point in points)
		{
			Raylib.DrawCircleV(point, 2, Color.White);
		}
	}
}
