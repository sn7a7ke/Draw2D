using System.Collections.Generic;

namespace Plane2D
{
    //интегрирование, экстремумы, касательные
    interface IFunction2D : IScope
    {
        List<double> FuncYFromX(double x);

        List<double> InverseFuncXFromY(double y);

        Line2D GetTangent(Point2D p);

        //List<double> Intersection(IFunction func);
    }
}
