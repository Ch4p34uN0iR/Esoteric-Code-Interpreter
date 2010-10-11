/*
 * This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * http://sam.zoy.org/wtfpl/COPYING for more details.
 */

using System;
using System.Collections.Generic;

namespace RoliSoft
{
    public static class Utils
    {
        public static Random Rand = new Random();

        public static T RandomEnum<T>()
        {
            var vals = (T[])Enum.GetValues(typeof(T));
            return vals[Rand.Next(0, vals.Length)];
        }

        public static T RandomItem<T>(List<T> list)
        {
            return list[Rand.Next(0, list.Count)];
        }
    }
}
