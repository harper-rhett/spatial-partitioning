using Raylib_cs;
using System.Numerics;
using System;
using System.Collections.Generic;
using SpatialPartitioning;

// Constants
const int windowSize = 1200;

// Initialize
QuadtreeTest quadtreeTest = new(windowSize, 500, 50);

// Game loop
Raylib.InitWindow(windowSize, windowSize, "Quadtree Test");
while (!Raylib.WindowShouldClose())
{
	Raylib.BeginDrawing();
	Raylib.ClearBackground(Color.Black);
	quadtreeTest.Draw();
	Raylib.EndDrawing();
}

Raylib.CloseWindow();