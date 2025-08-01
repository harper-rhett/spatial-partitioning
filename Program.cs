using Raylib_cs;
using System.Numerics;
using System;
using System.Collections.Generic;
using SpatialPartitioning;

// Constants
const int windowSize = 800;
const int points = 100;
const int comparisons = 500;
const float hashSize = 100;

// Initialize
QuadtreeTest quadtreeTest = new(windowSize, points);
SpatialHashTest spatialHashTest = new(windowSize, hashSize, points, comparisons);
