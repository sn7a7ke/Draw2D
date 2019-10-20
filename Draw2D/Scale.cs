using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw2D
{
    public class Scale
    {
        private readonly double[] _scale = { 0.1, 0.25, 0.5, 1, 2, 5, 10 };
        private int _index = 3;

        public int Length => _scale.Length;

        public double Value => _scale[_index];

        public Scale()
        {
        }

        public Scale(double[] scale, int index)
        {
            _scale = scale ?? throw new ArgumentNullException(nameof(scale));
            if (index < 0 || index >= scale.Length)
                throw new ArgumentOutOfRangeException(nameof(scale));
            _index = index;
        }

        public double Up()
        {
            if (_index < Length - 1)
                _index++;
            return Value;
        }

        public double Down()
        {
            if (_index > 0)
                _index--;
            return Value;
        }
    }
}
