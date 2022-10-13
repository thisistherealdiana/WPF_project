# WPF project
In this work, types are defined for storing measurement data of complex values of the electromagnetic field on a two-dimensional grid:
• struct Grid2D for 2D grid parameters;
• abstract base class V4Data and two derived classes V4DataOnGrid and V4DataCollection:
• class V4DataOnGrid for field values on a uniform grid, which are stored in a two-dimensional array;
• class V4DataCollection for field values on a non-uniform grid that are stored in a collection
Dictionary< System.Numerics.Vector2, System.Numerics.Complex>;
• class V4MainCollection for a collection of objects of type V4DataOnGrid and V4DataCollection.

The Grid2D structure for a 2D grid contains public auto-implemented properties
• the step along the Ox axis;
• the number of grid nodes along the Ox axis;
• the step along the Oy axis;
• the number of grid nodes along the Oy axis;
It is assumed that the coordinates of the first points along the axes Ox and Oy are equal to zero.

Abstract base class V4Data has public auto-implemented properties
• information about measurements and identification of the data set;
• electromagnetic field frequency.

The V4DataOnGrid class derives from the V4Data class and has public auto-implemented properties
• type Grid2D for the grid;
• of type Complex[,] – two-dimensional array for field values in grid nodes.


**The implementation can be seen below**

https://user-images.githubusercontent.com/74499545/195572792-5f30362a-0c60-439d-b98f-187475ed399b.mp4

