using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace SpatialPartitioning;

internal class SpatialHash3DTest
{
	private List<Vector3> points = new();
	private Dictionary<Vector3, Vector3> closestPoints = new();
	private SpatialHash3D spatialHash;
	private float hashSize;
	private float spaceSize;
	private Camera3D camera;
	private Vector3 cameraDirection = Vector3.One;

	private const float movementSpeed = 45;

	public SpatialHash3DTest(int windowSize, float spaceSize, float hashSize, int pointCount, int comparisonCount)
	{
		// Initialize
		this.hashSize = hashSize;
		this.spaceSize = spaceSize;
		camera = new(-Vector3.One * spaceSize, Vector3.Zero, Vector3.UnitY, 45, CameraProjection.Perspective);
		GenerateData(pointCount, comparisonCount);

		// Generate window
		InitWindow(windowSize, windowSize, "Spatial Hash 3D Test");

		// Render to window
		int gridSlices = (int)(hashSize / spaceSize);
		while (!WindowShouldClose())
		{
			Input();
			Draw();
		}

		// Close window
		CloseWindow();
	}

	private void GenerateData(int pointCount, int comparisonCount)
	{
		// Initialize
		spatialHash = new(hashSize);
		Random random = new Random();

		// Generate random points
		for (int pointIndex = 0; pointIndex < pointCount; pointIndex++)
		{
			float x = (float)random.NextDouble() * spaceSize;
			float y = (float)random.NextDouble() * spaceSize;
			float z = (float)random.NextDouble() * spaceSize;
			Vector3 point = new(x, y, z);
			points.Add(point);
			spatialHash.AddPoint(point);
		}

		// Generate random comparisons
		for (int comparisonIndex = 1; comparisonIndex <= comparisonCount; comparisonIndex++)
		{
			float x = (float)random.NextDouble() * spaceSize;
			float y = (float)random.NextDouble() * spaceSize;
			float z = (float)random.NextDouble() * spaceSize;
			Vector3 comparison = new(x, y, z);
			Vector3? closestPoint = spatialHash.GetClosestPoint(comparison, out float closestDistance);
			closestPoints[comparison] = closestPoint.Value;
		}
	}

	private void Input()
	{
		float deltaTime = GetFrameTime();
		if (IsKeyDown(KeyboardKey.W))
		{
			camera.Position += cameraDirection * movementSpeed * deltaTime;
		}
		if (IsKeyDown(KeyboardKey.S))
		{
			camera.Position += -cameraDirection * movementSpeed * deltaTime;
		}
		if (IsKeyDown(KeyboardKey.A))
		{
			camera.Position += Vector3.Cross(cameraDirection, -camera.Up) * movementSpeed * deltaTime;
		}
		if (IsKeyDown(KeyboardKey.D))
		{
			camera.Position += Vector3.Cross(cameraDirection, camera.Up) * movementSpeed * deltaTime;
		}
		if (IsKeyDown(KeyboardKey.Space))
		{
			camera.Position += camera.Up * movementSpeed * deltaTime;
		}
		if (IsKeyDown(KeyboardKey.LeftControl))
		{
			camera.Position += -camera.Up * movementSpeed * deltaTime;
		}

		camera.Target = camera.Position + cameraDirection;
	}

	private void Draw()
	{
		BeginDrawing();
		ClearBackground(Black);
		BeginMode3D(camera);

		// Draw grid
		for (float x = 0; x < spaceSize; x += hashSize)
			for (float y = 0; y < spaceSize; y += hashSize)
				for (float z = 0; z < spaceSize; z += hashSize)
				{
					DrawCubeWires(new Vector3(x, y, z), hashSize, hashSize, hashSize, Green);
				}

		// Draw comparisons
		foreach (Vector3 comparison in closestPoints.Keys)
		{
			Vector3 closetPoint = closestPoints[comparison];
			DrawLine3D(comparison, closetPoint, Blue);
			DrawSphere(comparison, 1, Blue);
		}

		// Draw points
		foreach (Vector3 point in points)
		{
			DrawSphere(point, 1, White);
		}

		EndMode3D();
		EndDrawing();
	}
}
