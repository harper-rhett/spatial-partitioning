using System.Numerics;
using System.Drawing;

namespace SpatialPartitioning;

public class Quadtree
{
	private class Node
	{
		// Children
		public bool IsLeafNode { get; private set; } = true;
		private List<Vector2> points = new();
		private Node northWest;
		private Node northEast;
		private Node southWest;
		private Node southEast;

		// Boundaries
		private float left;
		private float top;
		private float right;
		private float bottom;

		private RectangleF Quadrant => new RectangleF(left, top, right - left, bottom - top);

		public Node(float left, float top, float right, float bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		public void AddPoint(Vector2 point)
		{
			if (IsLeafNode)
			{
				points.Add(point);
				if (points.Count == 4) Split();
			}
			else SortAndAddPoint(point);
		}

		private void Split()
		{
			// Split into quadrants
			float verticalSplit = left + ((right - left) / 2.0f);
			float horizontalSplit = top + ((bottom - top) / 2.0f);
			northWest = new(left, top, verticalSplit, horizontalSplit);
			northEast = new(verticalSplit, top, right, horizontalSplit);
			southWest = new(left, horizontalSplit, verticalSplit, bottom);
			southEast = new(verticalSplit, horizontalSplit, right, bottom);

			// Divide positions into respective quadrants
			foreach (Vector2 point in points) SortAndAddPoint(point);

			// No longer a leaf node
			IsLeafNode = false;
		}

		private void SortAndAddPoint(Vector2 point)
		{
			Node node = SortPosition(point);
			node.AddPoint(point);
		}

		private Node SortPosition(Vector2 position)
		{
			if (northWest.InBounds(position)) return northWest;
			else if (northEast.InBounds(position)) return northEast;
			else if (southWest.InBounds(position)) return southWest;
			else if (southEast.InBounds(position)) return southEast;
			else throw new Exception("Position out of bounds.");
		}

		private bool InBounds(Vector2 position)
		{
			return position.X >= left
				&& position.X < right
				&& position.Y >= top
				&& position.Y < bottom;
		}

		public void CollectQuadrants(List<RectangleF> quadrants)
		{
			if (IsLeafNode) quadrants.Add(Quadrant);
			else CollectChildQuadrants(quadrants);
		}

		private void CollectChildQuadrants(List<RectangleF> quadrants)
		{
			northWest.CollectQuadrants(quadrants);
			northEast.CollectQuadrants(quadrants);
			southWest.CollectQuadrants(quadrants);
			southEast.CollectQuadrants(quadrants);
		}
	}

	private Node rootNode;

	public Quadtree(int size)
	{
		rootNode = new(0, 0, size, size);
	}

	public void AddPosition(Vector2 position)
	{
		rootNode.AddPoint(position);
	}

	public List<RectangleF> GetQuadrants()
	{
		List<RectangleF> quadrants = new();
		rootNode.CollectQuadrants(quadrants);
		return quadrants;
	}
}
