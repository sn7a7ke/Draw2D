using System;
using System.Collections.Generic;

namespace Plane2D
{
    //интегрирование, экстремумы, касательные
    interface IFunction2D
    {
        double MaxX { get; }
        double MaxY { get; }
        double MinX { get; }
        double MinY { get; }
        List<double> FuncYFromX(double x);
        List<double> InverseFuncXFromY(double y);
        //List<double> Intersection(IFunction func);
    }
}
