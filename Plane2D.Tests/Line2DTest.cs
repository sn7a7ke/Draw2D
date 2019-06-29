using NUnit.Framework;
using System;


namespace Plane2D.Tests
{
    [TestFixture]
    class Line2DTest
    {
        Line2D l1;
        Line2D l2;
        Line2D l3;

        [SetUp]
        public void Init()
        {
            l1 = new Line2D(2, 1, -4);
            l2 = new Line2D(-1, 2, 2);
            l3 = new Line2D(2, 0, 5);
        }

        [Test]
        public void Ctor_ABC_True()
        {
            Assert.IsTrue(l1.Equals(new Line2D(2, 1, -4)));
        }
        [Test]
        public void Ctor_kb_True()
        {
            Line2D lin3 = new Line2D(-2, 4);
            Assert.IsTrue(l1.Equals(lin3));
        }

        [Test]
        public void Ctor_TwoPoint_True()
        {
            Assert.IsTrue(l1.Equals(new Line2D(new Point2D(2, 0), new Point2D(0, 4))));
        }

        [Test]
        public void Ctor_VectorPoint_True()
        {
            Assert.IsTrue(l1.Equals(new Line2D(new Vector2D(2, 1), new Point2D(1, 2))));
        }

        [Test]
        public void Ctor_Exception()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Line2D(0, 0, 3), "A and B should not be zero at the same time");
        }

        [Test]
        public void IsLineInKB_True()
        {
            Assert.IsTrue(l1.IsLineInKB);
        }

        [Test]
        public void IsLineInKB_False()
        {
            Assert.IsFalse(l3.IsLineInKB);
        }

        [Test]
        public void Kk_True()
        {
            Assert.IsNotNull(l2.Kk);
            double res = l2.Kk ?? 0;
            Assert.IsTrue(res.Equal(0.5));
        }

        [Test]
        public void Kk_IsNull()
        {
            Assert.IsNull(l3.Kk);
        }

        [Test]
        public void Kb_True()
        {
            Assert.IsNotNull(l2.Kb);
            double res = l2.Kb ?? 0;
            Assert.IsTrue(res.Equal(-1));
        }

        [Test]
        public void Kb_IsNull()
        {
            Assert.IsNull(l3.Kb);
        }

        [Test]
        public void IsParallel_True()
        {
            Assert.IsTrue(l1.IsParallel(new Line2D(4, 2, -8)));
        }

        [Test]
        public void IsParallel_False()
        {
            Assert.IsFalse(l1.IsParallel(l2));
        }

        [Test]
        public void IsParallelAxisX_True()
        {
            Assert.IsTrue(new Line2D(0, 2, -2).IsParallelAxisX());
        }

        [Test]
        public void IsParallelAxisX_False()
        {
            Assert.IsFalse(l1.IsParallelAxisX());
        }

        [Test]
        public void IsParallelAxisY_True()
        {
            Assert.IsTrue(l3.IsParallelAxisY());
        }

        [Test]
        public void IsParallelAxisY_False()
        {
            Assert.IsFalse(l1.IsParallelAxisY());
        }

        [Test]
        public void IsPerpendicular_True()
        {
            Assert.IsTrue(l1.IsPerpendicular(l2));
        }

        [Test]
        public void IsPerpendicular_False()
        {
            Assert.IsFalse(l1.IsPerpendicular(l3));
        }

        [Test]
        public void IsOnLine_True()
        {
            Assert.IsTrue(l1.IsOnLine(new Point2D(1, 2)));
        }

        [Test]
        public void IsOnLine_False()
        {
            Assert.IsFalse(l1.IsOnLine(new Point2D(1, 3)));
        }

        [Test]
        public void DistanceFromPointToLine()
        {
            Assert.IsTrue(l1.DistanceFromPointToLine(new Point2D(6, 2)).Equal(Math.Sqrt(20)));
        }

        [Test]
        public void GetNormal()
        {
            Assert.IsTrue(l1.GetNormal().Equals(new Point2D(2, 1)));
        }

        [Test]
        public void MaxX_Infinity()
        {
            var res = l1.MaxX;

            Assert.IsTrue(double.IsPositiveInfinity(res));
        }

        [Test]
        public void MaxX_Number()
        {
            Assert.IsTrue(l3.MaxX.Equal(-2.5));
        }

        [Test]
        public void MaxY_Infinity()
        {
            var res = l1.MaxY;

            Assert.IsTrue(double.IsPositiveInfinity(res));
        }

        [Test]
        public void MaxY_Number()
        {
            Assert.IsTrue(new Line2D(0, 2, -2).MaxY.Equal(1));
        }

        [Test]
        public void MinX_Infinity()
        {
            var res = l1.MinX;

            Assert.IsTrue(double.IsNegativeInfinity(res));
        }

        [Test]
        public void MinX_Number()
        {
            Assert.IsTrue(l3.MinX.Equal(-2.5));
        }

        [Test]
        public void MinY_Infinity()
        {
            var res = l1.MinY;

            Assert.IsTrue(double.IsNegativeInfinity(res));
        }

        [Test]
        public void MinY_Number()
        {
            Assert.IsTrue(new Line2D(0, 2, -2).MinY.Equal(1));
        }

        [Test]
        public void FuncYFromX_OneNumber()
        {
            var list = l1.FuncYFromX(1);
            Assert.IsTrue(list.Count == 1);
            Assert.IsTrue(list[0] == 2);
        }

        [Test]
        public void FuncYFromX_Null()
        {
            var list = l3.FuncYFromX(1);
            Assert.IsNull(list);
        }

        [Test]
        public void InverseFuncXFromY_OneNumber()
        {
            var list = l1.InverseFuncXFromY(2);
            Assert.IsTrue(list.Count == 1);
            Assert.IsTrue(list[0] == 1);
        }

        [Test]
        public void InverseFuncXFromY_Null()
        {
            var list = new Line2D(0, 2, 3).InverseFuncXFromY(1);
            Assert.IsNull(list);
        }

        [Test]
        public void GetTangent()
        {
            Assert.IsTrue(l1.GetTangent(new Point2D(2, 0)).Equals(l1));
        }

        [Test]
        public void GetTangent_Null()
        {
            Assert.IsNull(l1.GetTangent(new Point2D(4, 0)));
        }

        [Test]
        public void PerpendicularFromPoint()
        {
            Assert.IsTrue(l1.PerpendicularFromPoint(new Point2D(6, 2)).Equals(l2));
        }

        [Test]
        public void PerpendicularFromPoint2()
        {
            Line2D l1 = new Line2D(1, 0, -2);
            Assert.IsTrue(l1.PerpendicularFromPoint(new Point2D(4, 1)).Equals(new Line2D(0, 1, -1)));
        }

        [Test]
        public void PerpendicularFromPoint3()
        {
            Line2D l1 = new Line2D(0, 1, -2);
            Assert.IsTrue(l1.PerpendicularFromPoint(new Point2D(1, 4)).Equals(new Line2D(1, 0, -1)));
        }

        [Test]
        public void PerpendicularFromPoint_PointOnLine()
        {
            Line2D l1 = new Line2D(0, 1, -2);
            Assert.IsTrue(l1.PerpendicularFromPoint(new Point2D(0, 2)).Equals(l2));
        }

        [Test]
        public void PerpendicularFromPointToPointOnLine()
        {
            Assert.IsTrue(l1.IntersectPerpendicularFromPointWithLine(new Point2D(6, 2)).Equals(new Point2D(2, 0)));
        }

        [Test]
        public void Intersect()
        {
            Assert.IsTrue(l1.Intersect(l2).Equals(new Point2D(2, 0)));
        }

        [Test]
        public void Intersect_Null()
        {
            Assert.IsNull(l1.Intersect(l1));
        }

        [Test]
        public void AngleBetweenLines()
        {
            Assert.IsTrue(l1.AngleBetweenLines(new Line2D(1, -2, 4)).Equals(Math.PI / 2));
        }

        [Test]
        public void CheckGetHashCode_AIsNotNull()
        {
            Assert.IsTrue(l1.GetHashCode() == (int)(l1.B / l1.A * 101 + l1.C / l1.A));
        }

        [Test]
        public void CheckGetHashCode_AIsNull()
        {
            Assert.IsTrue(new Line2D(0, 2, -2).GetHashCode() == (int)(0 / 2 * 101 + (-2) / 2));
        }





        [Test]
        public void Equals_True()
        {
            Assert.IsTrue(l1.Equals(new Line2D(4, 2, -8)));
        }

        [Test]
        public void Equals_False()
        {
            Assert.IsFalse(l1.Equals(new Line2D(4, 2, -6)));
        }

        [Test]
        public void IsEquals_True()
        {
            Assert.IsTrue(l1 == new Line2D(4, 2, -8));
        }

        [Test]
        public void IsEquals_Null_False()
        {
            Assert.IsFalse(Equals(l1, null));
        }

        [Test]
        public void IsEqualsOneParam_Null_False()
        {
            Assert.IsFalse(l1.Equals(null));
        }

        [Test]
        public void IsEqualsOneParam_Object_False()
        {
            object obj = null;
            Assert.IsFalse(l1.Equals(obj));
        }

        [Test]
        public void IsNotEquals()
        {
            Assert.IsTrue(l1 != l2);
        }
    }
}
