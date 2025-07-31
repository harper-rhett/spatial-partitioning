using Raylib_cs;
using System.Numerics;
using System;
using System.Collections.Generic;
using SpatialPartitioning;

// Constants
const int windowSize = 1200;
const int points = 1000;
const int comparisons = 50;

// Initialize
//QuadtreeTest quadtreeTest = new(windowSize, points);
SpatialHashTest spatialHashTest = new(windowSize, 50, points, comparisons);
