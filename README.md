# Electromagnetic Field on a two-dimensional Grid (WPF project)
In this WPF project we are storing measurement data of complex values of the electromagnetic field on a two-dimensional grid.

V4DataOnGrid holds values on a uniform grid, which are stored in a two-dimensional array.

V4DataCollection holds values on a non-uniform grid that are stored in a dictionary collection of pairs as <Vector, Complex Number>.

MainCollection holds a collection of objects of type V4DataOnGrid and V4DataCollection.

V4DataOnGrid holds:

- the step along the Ox axis;
- the number of grid nodes along the Ox axis;
- the step along the Oy axis;
- the number of grid nodes along the Oy axis;
- the text information about this point on the grid;
- the frequency of the electromagnetic field at such a point.

Each dictionary in V4DataCollection holds:

- the text information about the dictionary;
- the frequency of the electromagnetic field;
- number of elements in the dictionary;
- information about each element: coordinates, complex value, and absolute value.

Automatically after having at least one collection in Main Collection the program calculates the complex value with maximum absolute value.

After clicking on a particular DataOnGrid we can see the calculated maximum absolute value and minimum absolute value.

After clicking on a particular DataCollection we can see each element in the dictionary with detailed information.

We can also add custom dictionaries to the DataCollection that are going to be automatically filled with random values depending on the preference of the user. After adding new elements the information about complex value with maximum absolute value in Main Collection will be updated depending on the new calculation.

**The implementation can be seen below**

https://user-images.githubusercontent.com/74499545/195572792-5f30362a-0c60-439d-b98f-187475ed399b.mp4

