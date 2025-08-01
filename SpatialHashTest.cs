using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Color = Raylib_cs.Color;

namespace SpatialPartitioning;

internal class SpatialHashTest
{
	private List<Vector2> points = new();
	private Dictionary<Vector2, Vector2> closestPoints = new();
	private SpatialHash2D spatialHash;
	private int windowSize;
	private float hashSize;

	public SpatialHashTest(int windowSize, float hashSize, int pointCount, int comparisonCount)
	{
		// Initialize
		this.windowSize = windowSize;
		this.hashSize = hashSize;
		spatialHash = new(hashSize);
		Random random = new Random();

		// Generate random points
		for (int pointIndex = 0; pointIndex < pointCount; pointIndex++)
		{
			int x = random.Next(windowSize);
			int y = random.Next(windowSize);
			Vector2 point = new(x, y);
			points.Add(point);
			spatialHash.AddPoint(point);
		}

		// Generate random comparisons
		for (int comparisonIndex = 1; comparisonIndex <= comparisonCount; comparisonIndex++)
		{
			int x = random.Next(windowSize);
			int y = random.Next(windowSize);
			Vector2 comparison = new(x, y);
			Vector2? closestPoint = spatialHash.GetClosestPoint(comparison);
			closestPoints[comparison] = closestPoint.Value;
		}

		// Generate window
		Raylib.InitWindow(windowSize, windowSize, "Spatial Hash Test");

		// Render to window
		int gridSlices = (int)(hashSize / windowSize);
		while (!Raylib.WindowShouldClose())
		{
			Raylib.BeginDrawing();
			Draw();
			Raylib.EndDrawing();
		}

		// Close window
		Raylib.CloseWindow();
	}

	private void Draw()
	{
		Raylib.ClearBackground(Color.Black);

		// Draw grid
		for (float x = 0; x < windowSize; x += hashSize)
			for (float y = 0; y < windowSize; y += hashSize)
			{
				Raylib.DrawRectangleLines((int)x, (int)y, (int)hashSize, (int)hashSize, Color.Green);
			}

		// Draw comparisons
		foreach (Vector2 comparison in closestPoints.Keys)
		{
			Vector2 closetPoint = closestPoints[comparison];
			Raylib.DrawLineV(comparison, closetPoint, Color.Blue);
			Raylib.DrawCircleV(comparison, 3, Color.Blue);
		}

		// Draw points
		foreach (Vector2 point in points)
		{
			Raylib.DrawCircleV(point, 3, Color.White);
		}
	}
}
