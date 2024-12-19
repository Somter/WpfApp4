using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task3
{
    public static class Validation
    {
        public static bool ValidateCheck(string name, string surname, string phone, TextBox NameText, TextBox SurnameText, TextBox PhoneText)
        {
            var validations = new[]
            {
                new { Field = NameText, Value = name, Pattern = "^[^\\d]{3,}$", ErrorMessage = "Имя должно содержать не меньше 3 буквы и не должно содержать цифры." },
                new { Field = SurnameText, Value = surname, Pattern = "^[^\\d]{3,}$", ErrorMessage = "Фамилия должна содержать не меньше 3 буквы и не должна содержать цифры." },
                new { Field = PhoneText, Value = phone, Pattern = "^[\\d()+\\- ]+$", ErrorMessage = "Телефон может содержать цифры, +, (), и пробелы." }
            };

            bool isValid = true;
            ResetFieldBorders(NameText, SurnameText, PhoneText);

            foreach (var validation in validations)
            {
                if (!string.IsNullOrEmpty(validation.Value) && !Regex.IsMatch(validation.Value, validation.Pattern))
                {
                    SetError(validation.Field, validation.ErrorMessage);
                    isValid = false;
                }
            }

            return isValid;
        }

        public static void ValidateSingleInput(string value, string pattern, string errorMessage, TextBox textBox)
        {
            if (!Regex.IsMatch(value, pattern))
            {
                SetError(textBox, errorMessage);
            }
            else
            {
                ResetFieldBorders(textBox);
            }
        }

        public static void SetError(TextBox textBox, string message)
        {
            textBox.BorderBrush = Brushes.Red;
            textBox.ToolTip = message;
        }

        public static void ResetFieldBorders(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.BorderBrush = Brushes.Gray;
                textBox.ToolTip = null;
            }
        }
    }
}