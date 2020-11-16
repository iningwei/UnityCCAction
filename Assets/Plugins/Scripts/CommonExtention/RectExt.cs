using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectExt
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="outRect">intersect rect of a and b</param>
    /// <returns>If return true,it means really intersect,otherwise have no intersect</returns>
    public static bool Intersect(this Rect a, Rect b, out Rect outRect)
    {
        float x = Math.Max(a.x, b.x);
        var num2 = Math.Min(a.x + a.width, b.x + b.width);
        float y = Math.Max(a.y, b.y);
        var num4 = Math.Min(a.y + a.height, b.y + b.height);
        if ((num2 >= x) && (num4 >= y))
        {
            outRect = new Rect(x, y, num2 - x, num4 - y);
            return true;
        }

        outRect = new Rect();
        return false;

    }
    /// <summary>
    /// Returns a third Rect structure that represents the intersection of two other Rect structures. 
    /// If there is no intersection, an empty Rect is returned.
    /// </summary>
    /// <param name="a">
    /// A rectangle to intersect.   
    /// </param>
    /// <param name="b">
    /// B rectangle to intersect.  
    /// </param>
    /// <returns>
    /// A Rect that represents the intersection of a and b.
    /// </returns>
    public static Rect Intersect(this Rect a, Rect b)
    {
        float x = Math.Max(a.x, b.x);
        var num2 = Math.Min(a.x + a.width, b.x + b.width);
        float y = Math.Max(a.y, b.y);
        var num4 = Math.Min(a.y + a.height, b.y + b.height);
        if ((num2 >= x) && (num4 >= y))
        {
            return new Rect(x, y, num2 - x, num4 - y);
        }

        return new Rect();
    }

    /// <summary>
    /// Determines if this rectangle intersects with rect.
    /// </summary>
    /// <param name="source">The source rectangle from which the intersection will be tested.</param>
    /// <param name="rect">
    /// The rectangle to test.
    /// </param>
    /// <returns>
    /// This method returns true if there is any intersection, otherwise false.
    /// </returns>
    public static bool Intersects(this Rect source, Rect rect)
    {
        return !((source.x > rect.xMax) || (source.xMax < rect.x) || (source.y > rect.yMax) || (source.yMax < rect.y));
    }
}
