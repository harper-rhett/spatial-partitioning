using Raylib_cs;
using System.Numerics;
using System;
using System.Collections.Generic;
using SpatialPartitioning;

const int windowSize = 800;

new QuadtreeTest(windowSize, 500);
new SpatialHash2DTest(windowSize, 100, 100, 500);
new SpatialHash3DTest(windowSize, 100, 25, 100, 200);