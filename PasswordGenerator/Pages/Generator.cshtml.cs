using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator.Pages
{
    public class GeneratorModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            if(!IncludeNumbers && !IncludeLowercase && !IncludeUppercase && !IncludeSpecial)
            {
                ErrorMessage = "Select at least one character type";
                GeneratedPassword = string.Empty;
                return;
            }
            GeneratedPassword = Generate();
            ErrorMessage = string.Empty;
        }

        [BindProperty]
        public string GeneratedPassword { get; set; }
        [BindProperty]
        public int PasswordSize { get; set; }

        [BindProperty]
        public bool IncludeNumbers { get; set; }

        [BindProperty]
        public bool IncludeLowercase { get; set; }

        [BindProperty]
        public bool IncludeUppercase { get; set; }

        [BindProperty]
        public bool IncludeSpecial { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public List<char> CharPool { get; set; } = new List<char>();

        public string Generate()
        {
            CharPool.Clear();
            if (IncludeNumbers) CharPool.AddRange("0123456789");
            if (IncludeLowercase) CharPool.AddRange("abcdefghijklmnopqrstuvwxyz");
            if (IncludeUppercase) CharPool.AddRange("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            if (IncludeSpecial) CharPool.AddRange("!@#$%^&*()-_=+[]{}|;:,.<>?/");
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < PasswordSize; i++)
            {
                int validPos = RandomNumberGenerator.GetInt32(CharPool.Count);
                password.Append(CharPool[validPos]);
            }
            return password.ToString();
        }
    }
}
