using Raylib_cs;
using System.Numerics;
using System;
using System.Collections.Generic;
using SpatialPartitioning;

// Constants
const int windowSize = 800;
const int points = 10;
const int comparisons = 50;
const float hashSize = 200;

// Initialize
//QuadtreeTest quadtreeTest = new(windowSize, points);
SpatialHashTest spatialHashTest = new(windowSize, hashSize, points, comparisons);
