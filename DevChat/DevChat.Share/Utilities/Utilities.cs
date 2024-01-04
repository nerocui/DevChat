using System.Security.Cryptography;
using System.Text;

namespace DevChat.Share.Utilities;

public class Utilities
{
    public static string HashEmails(List<string> emails)
    {
        // Sort the emails in ascending order
        var sortedEmails = emails.Select(email => email.ToLower().Trim()).ToList();
        sortedEmails.Sort();

        // Join the emails into a single string with a comma separator
        string emailString = string.Join(",", sortedEmails);

        // Create a SHA256 hash object
        SHA256 sha256 = SHA256.Create();

        // Compute the hash of the email string
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(emailString));

        // Convert the hash bytes to a base64url string
        string hashString = Base64UrlEncode(hashBytes);

        // Return the hash string
        return hashString;
    }

    // A custom method to perform base64url encoding
    public static string Base64UrlEncode(byte[] data)
    {
        // Encode the data using base64
        string encoded = Convert.ToBase64String(data);

        // Replace characters that are not URL safe with URL safe characters
        string urlSafeEncoded = encoded.Replace('+', '-').Replace('/', '_').TrimEnd('=');

        return urlSafeEncoded;
    }


    public static string GenerateMessageSource(string html, string js, string css)
    {
        // Return the html page as a string
        return $@"
            <!DOCTYPE html>
            <html>
                <head>
                    <style type='text/css'>
                        {css}
                    </style>
                </head>
                <body>
                    <div id='message-body'>
                        {html}
                    </div>
                    <script>
                        {js}
                    </script>
                </body>
            </html>
        ";
    }
}
