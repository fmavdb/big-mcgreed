using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.utility
{
    public static class ArrayUtilities
    {
        public static byte[] primitive(byte[] bytes)
        {
            byte[] pBytes = new byte[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                pBytes[i] = bytes[i];
            }
            return pBytes;
        }

        public static double[] primitive(double[] doubles)
        {
            double[] pDoubles = new double[doubles.Length];
            for (int i = 0; i < doubles.Length; i++)
            {
                pDoubles[i] = doubles[i];
            }
            return pDoubles;
        }

        public static int[] primitive(int[] integers)
        {
            int[] pIntegers = new int[integers.Length];
            for (int i = 0; i < integers.Length; i++)
            {
                pIntegers[i] = integers[i];
            }
            return pIntegers;
        }

        public static short[] primitive(short[] shorts)
        {
            short[] pShorts = new short[shorts.Length];
            for (int i = 0; i < shorts.Length; i++)
            {
                pShorts[i] = shorts[i];
            }
            return pShorts;
        }

        public static int[] StringToIntArray(string toParse, char seperator)
        {
            List<int> integers = new List<int>();
            string[] strings = toParse.Split(seperator);
            foreach (string s in strings)
            {
                int i;
                if (int.TryParse(s.Trim(), out i))
                {
                    integers.Add(i);
                }
            }
            return integers.ToArray();
        }
    }
}
