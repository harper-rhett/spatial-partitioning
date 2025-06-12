using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace SpatialPartitioning;

public class Quadtree
{
	private class Node
	{
		// Children
		private bool isLeafNode = true;
		private List<Vector2> positions = new();
		private Node northWest;
		private Node northEast;
		private Node southWest;
		private Node southEast;

		// Boundaries
		private float left;
		private float top;
		private float right;
		private float bottom;

		private Rectangle Quadrant => new Rectangle(left, top, right - left, bottom - top);

		public Node(float left, float top, float right, float bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		public void AddPosition(Vector2 position)
		{
			if (isLeafNode)
			{
				positions.Add(position);
				if (positions.Count == 4) Split();
			}
			else
			{
				SortAndAdd(position);
			}
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
			foreach (Vector2 position in positions) SortAndAdd(position);

			// No longer a leaf node
			isLeafNode = false;
		}

		private void SortAndAdd(Vector2 position)
		{
			Node parentNode = Sort(position);
			parentNode.AddPosition(position);
		}

		private Node Sort(Vector2 position)
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

		public void CollectQuadrants(List<Rectangle> quadrants)
		{
			if (isLeafNode) quadrants.Add(Quadrant);
			else CollectChildQuadrants(quadrants);
		}

		private void CollectChildQuadrants(List<Rectangle> quadrants)
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
		rootNode.AddPosition(position);
	}

	public List<Rectangle> GetQuadrants()
	{
		List<Rectangle> quadrants = new();
		rootNode.CollectQuadrants(quadrants);
		return quadrants;
	}

	public Vector2 GetClosestPoint(Vector2 point)
	{
		return Vector2.Zero;
	}
}
