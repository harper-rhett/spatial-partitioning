using Raylib_cs;
using System.Numerics;
using System;
using System.Collections.Generic;
using SpatialPartitioning;

// Constants
const int windowSize = 1200;
const int pointCount = 25;

// Initialize
Random random = new Random();
List<Vector2> points = new();
Quadtree quadtree = new(windowSize);

// Generate random points
for (int pointIndex = 0; pointIndex < pointCount; pointIndex++)
{
	int x = random.Next(windowSize);
	int y = random.Next(windowSize);
	Vector2 point = new(x, y);
	points.Add(point);
	quadtree.AddPosition(point);
}

// Game loop
Raylib.InitWindow(windowSize, windowSize, "Tree Tests");
while (!Raylib.WindowShouldClose())
{
	Raylib.BeginDrawing();
	Draw();
	Raylib.EndDrawing();
}

void Draw()
{
	// Clear screen
	Raylib.ClearBackground(Color.Black);

	// Draw points
	foreach (Vector2 point in points)
		Raylib.DrawCircle((int)point.X, (int)point.Y, 2, Color.White);

	// Draw quadrants
	List<Rectangle> quadrants = quadtree.GetQuadrants();
	foreach (Rectangle quadrant in quadrants)
		Raylib.DrawRectangleLinesEx(quadrant, 1, Color.Green);
}

Raylib.CloseWindow();