using System;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Visuals
    {
        internal static string Gradient(float min, float max, float value, string text = "")
        {
            var color = GetGradientColor(min, max, value, false);

            var colorName = GetGradientStringName(color);

            return Colorize(text.Length == 0 ? value.ToString() : text, colorName);

            //return $"<color={colorName}>{()}</color>";
        }

        // TODO mockup
        static string GetGradientStringName(Color color)
        {
            return Colors.COLOR_POSITIVE;
        }

        public static Color GetGradientColor(float min, float max, float val, bool reversed = false)
        {
            float percent = (val - min) / (max - min);

            if (percent < 0)
                percent = 0;

            if (percent > 1)
                percent = 1;

            if (reversed)
                percent = 1 - percent;

            float r = 1f - percent;
            float g = percent;

            return new Color(r, g, 0, 1);
        }

    }
}