using System;

namespace CarFactory.Classes {
    public static class HelperClass {
        public static dynamic Input<T>(string txtToShow) {
            Console.Write(txtToShow);
            string input = Console.ReadLine();
            if (typeof(T) == typeof(int)) {
                return Convert.ToInt32(input);
            }
            if (typeof(T) == typeof(string)) {
                return input;
            }
            if (typeof(T) == typeof(uint)) {
                return Convert.ToUInt32(input);
            }
            if (typeof(T) == typeof(bool)) {
                return Convert.ToBoolean(input);
            }

            return null;
        }
    }
}