public class InputHandler
{
    public static bool ValidateR(int r) => r >= 1 && r <= 10;

    public static bool ValidateN(int n) => n >= 1 && n <= 99;

    public static bool ValidateID(int id) => id >=1 && id <= 4;

    public static bool ValidateM(int m) => m >= 0 && m <= 999;

    public static string ValidateInput(int r, int n, int id, int m)
    {
        if(!ValidateR(r)) return "R must between 1 and 10";
        if(!ValidateN(n)) return "N must between 1 and 99";
        if(!ValidateR(id)) return "ID must between 1 and 4";
        if(!ValidateR(m)) return "Drachma(M) must between 0 and 999";
        return string.Empty;
    }
}