namespace OOP_LAB_2 {
    public static class Utils {
        public static string Capitalize(string str) => 
            str.Length switch {
                0 => str,
                1 => char.ToUpper(str[0]).ToString(),
                _ => char.ToUpper(str[0]) + str[1..],
        };
    }
}