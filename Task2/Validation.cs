using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task2
{
    public static class Validation
    {
        public static bool ValidateCheck(ContactL contacts, TextBox txtName, TextBox txtSurname, TextBox txtPhone)
        {
            bool isValid = true;
            ChangingFields(txtName, txtSurname, txtPhone);

            string name = contacts.SelectedContact.Name;
            string surname = contacts.SelectedContact.Surname;
            string phone = contacts.SelectedContact.Phone;

            if (!Regex.IsMatch(name ?? "", @"^[^\d]{3,}$"))
            {
                ErrorColor(txtName, "Имя должно содержать не меньше 3 буквы и не должно содержать цифры.");
                isValid = false;
            }

            if (!string.IsNullOrEmpty(surname) && !Regex.IsMatch(surname, @"^[^\d]{3,}$"))
            {
                ErrorColor(txtSurname, "Фамилия должна не меньше 3 буквы и не должна содержать цифры.");
                isValid = false;
            }

            if (!Regex.IsMatch(phone ?? "", @"^[\d()+\- ]+$"))
            {
                ErrorColor(txtPhone, "Телефон может содержать только цифры, +, (), и пробелы.");
                isValid = false;
            }

            return isValid;
        }

        public static void ErrorColor(TextBox textBox, string message)
        {
            textBox.BorderBrush = Brushes.Red;
            textBox.ToolTip = message;
        }

        public static void ChangingFields(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.BorderBrush = Brushes.Gray;
                textBox.ToolTip = null;
            }
        }


    }
}
