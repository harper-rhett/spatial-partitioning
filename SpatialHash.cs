using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpatialPartitioning;

// Divide the space into squares of equal size with a hash function
// When comparing a point, just look at adjacent squares

internal class SpatialHash
{
	private struct Coordinate()
	{
		public int X;
		public int Y;
	}

	public void AddPoint(Vector2 point)
	{

	}
}
