namespace OpenLibrary.Service.Helpers;

public class Generator
{
    /// <summary>
    /// To generate ISBN code for books.
    /// This method automatically generate ISBN code for book that added
    /// </summary>
    /// <returns></returns>
    public static string GenerateISBN13()
    {
        Random rnd = new Random();
        int[] digits = new int[12];
        digits[0] = 9;
        digits[1] = 7;
        digits[2] = 8;
        for (int i = 3; i < 12; i++)
        {
            digits[i] = rnd.Next(0, 10);
        }

        int checksum = 0;
        for (int i = 0; i < 12; i++)
        {
            checksum += (i % 2 == 0) ? digits[i] : digits[i] * 3;
        }
        checksum %= 10;
        if (checksum != 0)
        {
            checksum = 10 - checksum;
        }

        return string.Join("", digits) + checksum.ToString();
    }
}
