using System.Security.Cryptography;
using System.Text;

namespace DevChat.Share.Utilities;

public class Utilities
{
    public static string HashEmails(List<string> emails)
    {
        // Sort the emails in ascending order
        emails.Sort();

        // Join the emails into a single string with a comma separator
        string emailString = string.Join(",", emails);

        // Create a SHA256 hash object
        SHA256 sha256 = SHA256.Create();

        // Compute the hash of the email string
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(emailString));

        // Convert the hash bytes to a hexadecimal string
        string hashString = BitConverter.ToString(hashBytes);

        // Return the hash string
        return hashString;
    }
}
