using System;
using System.Drawing;

namespace Misp.Drawing
{
    internal static class ColorTranslator
    {

        public static string ToHtml(Color c)
        {
            string colorString = String.Empty;

            if (c.IsEmpty)
                return colorString;

            if (c.IsNamedColor)
            {
                if (c == Color.LightGray)
                {
                    // special case due to mismatch between Html and enum spelling
                    colorString = "LightGrey";
                }
                else
                {
                    colorString = c.Name;
                }
            }
            else
            {
                colorString = "#" + c.R.ToString("X2", null) +
                                    c.G.ToString("X2", null) +
                                    c.B.ToString("X2", null);
            }

            return colorString;
        }
    }
}