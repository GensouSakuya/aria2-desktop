using System;

namespace GensouSakuya.Aria2.Desktop.Shell.Helper
{
    public static class Tools
    {
        private static string[] units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };

        public static string ToStringWithUnit(decimal size)
        {
            var index = 0;
            while (size >= 1024)
            {
                size /= 1024;
                index++;
            }

            return $"{Math.Round(size, 2).ToString("N2")} {units[index]}";
        }
    }
}
