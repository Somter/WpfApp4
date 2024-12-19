using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace Task2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  
        }

        private ContactL ContactsCollection => (ContactL)Resources["contacts"];

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            if (!Validation.ValidateCheck(ContactsCollection, nameTextBox, surnameTextBox, phoneTextBox))
                return;

            if (ContactsCollection.SelectedContact != null)
            {
                var existingContact = ContactsCollection.Contacts.Contains(ContactsCollection.SelectedContact);
                if (!existingContact)
                {
                    ContactsCollection.Contacts.Add(new Contact
                    {
                        Name = ContactsCollection.SelectedContact.Name,
                        Surname = ContactsCollection.SelectedContact.Surname,
                        Address = ContactsCollection.SelectedContact.Address,
                        Phone = ContactsCollection.SelectedContact.Phone
                    });
                }
                else
                {
                    contactsDataGrid.Items.Refresh();
                }
                addEditButton.Content = "Добавить";
            }

            ClearFields();
        }

        private void EditContact_Click(object sender, RoutedEventArgs e)
        {
            if (contactsDataGrid.SelectedItem is Contact contact)
            {
                ContactsCollection.SelectedContact = contact;
                addEditButton.Content = "Изменить";
            }
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            if (contactsDataGrid.SelectedItem is Contact contact)
            {
                ContactsCollection.Contacts.Remove(contact);
                if (ContactsCollection.SelectedContact == contact)
                    ContactsCollection.SelectedContact = new Contact();
            }
        }

        private void SaveContacts_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { Filter = "Text files (*.txt)|*.txt" };
            if (saveFileDialog.ShowDialog() == true)
            {
                using var writer = new StreamWriter(saveFileDialog.FileName);
                foreach (var contact in ContactsCollection.Contacts)
                {
                    writer.WriteLine($"{contact.Name};{contact.Surname};{contact.Address};{contact.Phone}");
                }
            }
        }

        private void LoadContacts_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = "Text files (*.txt)|*.txt" };
            if (openFileDialog.ShowDialog() == true)
            {
                using var reader = new StreamReader(openFileDialog.FileName);
                ContactsCollection.Contacts.Clear();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 4)
                    {
                        ContactsCollection.Contacts.Add(new Contact
                        {
                            Name = parts[0],
                            Surname = parts[1],
                            Address = parts[2],
                            Phone = parts[3]
                        });
                    }
                }
                ContactsCollection.SelectedContact = new Contact();
            }
        }

        private void ClearFields()
        {
            ContactsCollection.SelectedContact = new Contact();
            Validation.ChangingFields(nameTextBox, surnameTextBox, addressTextBox, phoneTextBox);
            addEditButton.Content = "Добавить";
        }

        private void nameTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            ClearErrors();
            ValidateName(nameTextBox.Text);
        }

        private void surnameTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            ClearErrors();
            ValidateSurname(surnameTextBox.Text);
        }

        private void phoneTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            ClearErrors();
            ValidatePhone(phoneTextBox.Text);
        }

        private bool ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name) || !Regex.IsMatch(name, @"^[^\d]{3,}$"))
            {
                SetError(nameTextBox, "Имя должно содержать не меньше 3 букв и не должно содержать цифры.");
                return false;
            }
            return true;
        }

        private bool ValidateSurname(string surname)
        {
            if (string.IsNullOrEmpty(surname) || !Regex.IsMatch(surname, @"^[^\d]{3,}$"))
            {
                SetError(surnameTextBox, "Фамилия должна содержать не меньше 3 букв и не должна содержать цифры.");
                return false;
            }
            return true;
        }

        private bool ValidatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || !Regex.IsMatch(phone, @"^[\d()+\- ]+$"))
            {
                SetError(phoneTextBox, "Телефон может содержать цифры, +, (), и пробелы.");
                return false;
            }
            return true;
        }

        private void SetError(TextBox textBox, string message)
        {
            textBox.BorderBrush = Brushes.Red;
            textBox.ToolTip = message;
        }

        private void ClearErrors()
        {
            nameTextBox.BorderBrush = Brushes.Gray;
            surnameTextBox.BorderBrush = Brushes.Gray;
            phoneTextBox.BorderBrush = Brushes.Gray;

            nameTextBox.ToolTip = null;
            surnameTextBox.ToolTip = null;
            phoneTextBox.ToolTip = null;
        }
    }
}