using System;


public abstract class GeometricFigure{

    public abstract double SquareFigures {get;}
    public abstract double PerimeterFigures {get;}
}

public interface ProstoyNYholnik
{
    double height { get; }
    double base_ { get; }
    double angleBetweenBaseAndAdjacentSide { get; } 
    int numberOfSides { get; }
    double[] sideLengths { get; }
    double area { get; }
    double perimeter { get; }
}


public class Triangle : GeometricFigure, ProstoyNYholnik
{
    public double a { get; }
    public double b { get; }
    public double c { get; }
    public double height => 2 * area / base_ ;
}

public class Square : GeometricFigure, ProstoyNYholnik
{

}

public class Rhombus : GeometricFigure, ProstoyNYholnik
{

}

public class Rectangle : GeometricFigure, ProstoyNYholnik
{

}

public class Parallelogram : GeometricFigure, ProstoyNYholnik
{

}

public class Trapezoid : GeometricFigure, ProstoyNYholnik
{

}

public class Circle : GeometricFigure
{

}

public class Ellipse : GeometricFigure
{

}

class CompoundFigure
{
    
}


