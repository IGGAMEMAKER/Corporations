using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core
{
    public static class Rendering
    {
        public static Vector3 GetPointPositionOnCircle(int index, int length, float radius, float order, float offset = 0)
        {
            var angle = offset + (index) * (Mathf.PI * 2 - offset) * order / length;

            return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }
    }
}
