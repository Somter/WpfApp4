using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Xml.Linq;

namespace Task3
{
    public partial class MainWindow : Window
    {
        private Contact selecеContElements = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddOrUpdateContact()
        {
            var contacts = (ContactList)Resources["contacts"];

            if (!Validation.ValidateCheck(contacts.Name, contacts.Surname, contacts.Phone, NameText, SurnameText, PhoneText))
                return;

            if (selecеContElements == null)
            {
                contacts.Contacts.Add(new Contact
                {
                    Name = contacts.Name,
                    Surname = contacts.Surname,
                    Address = contacts.Address,
                    Phone = contacts.Phone
                });
            }
            else
            {
                selecеContElements.Name = contacts.Name;
                selecеContElements.Surname = contacts.Surname;
                selecеContElements.Address = contacts.Address;
                selecеContElements.Phone = contacts.Phone;

                contactDataGrid.Items.Refresh();
                selecеContElements = null;
            }

            btnAddEdit.Content = "Добавить";
            ClearFields();
        }

        private void AddContactExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            AddOrUpdateContact();
        }

        private void AddContactCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsValidContactInput();
        }

        private void EditContactExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (selecеContElements == null && contactDataGrid.SelectedItem is Contact contact)
            {
                selecеContElements = contact;
                BindContactToFields(contact);
                btnAddEdit.Content = "Изменить (Ctrl+E)";
            }
            else
            {
                AddOrUpdateContact();
            }
        }

        private void EditContactCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = selecеContElements == null ? contactDataGrid.SelectedItem is Contact : IsValidContactInput();
        }

        private void DeleteContactExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (contactDataGrid.SelectedItem is Contact contact)
            {
                var contacts = (ContactList)Resources["contacts"];
                contacts.Contacts.Remove(contact);
            }
        }

        private void DeleteContactCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = contactDataGrid.SelectedItem is Contact;
        }

        private void SaveContactsExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var contacts = (ContactList)Resources["contacts"];
            SaveToFile(contacts.Contacts);
        }

        private void SaveContactsCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var contacts = (ContactList)Resources["contacts"];
            e.CanExecute = contacts.Contacts.Any();
        }

        private void LoadContactsExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var contacts = (ContactList)Resources["contacts"];
            var loadedContacts = LoadFromFile();
            if (loadedContacts != null)
            {
                contacts.Contacts.Clear();
                foreach (var contact in loadedContacts)
                {
                    contacts.Contacts.Add(contact);
                }
            }
        }

        private void LoadContactsCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ClearFields()
        {
            var contacts = (ContactList)Resources["contacts"];
            contacts.Name = contacts.Surname = contacts.Address = contacts.Phone = string.Empty;
            Validation.ResetFieldBorders(NameText, SurnameText, txtAddress, PhoneText);
        }

        private void BindContactToFields(Contact contact)
        {
            var contacts = (ContactList)Resources["contacts"];
            contacts.Name = contact.Name;
            contacts.Surname = contact.Surname;
            contacts.Address = contact.Address;
            contacts.Phone = contact.Phone;
        }

        private bool IsValidContactInput()
        {
            var contacts = (ContactList)Resources["contacts"];
            return Regex.IsMatch(contacts.Name, "^[^\\d]{3,}$") && Regex.IsMatch(contacts.Phone, "^[\\d()+\\- ]+$");
        }

        private void SaveToFile(ObservableCollection<Contact> contacts)
        {
            var saveFileDialog = new SaveFileDialog { Filter = "JSON files (*.json)|*.json" };
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, JsonSerializer.Serialize(contacts));
            }
        }

        private ObservableCollection<Contact> LoadFromFile()
        {
            var openFileDialog = new OpenFileDialog { Filter = "JSON files (*.json)|*.json" };
            if (openFileDialog.ShowDialog() == true)
            {
                return JsonSerializer.Deserialize<ObservableCollection<Contact>>(File.ReadAllText(openFileDialog.FileName));
            }
            return null;
        }

        private void elemNameChanged(object sender, TextChangedEventArgs e)
        {
            ValidateField(NameText, "^[^\\d]{3,}$", "Имя должно содержать не меньше 3 букв и не должно содержать цифры.");
        }

        private void elemSurnameChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(((ContactList)Resources["contacts"]).Surname))
            {
                ValidateField(SurnameText, "^[^\\d]{3,}$", "Фамилия должна содержать не меньеш 3 букв и не должна содержать цифры.");
            }
        }

        private void elemPhoneChanged(object sender, TextChangedEventArgs e)
        {
            ValidateField(PhoneText, "^[\\d()+\\- ]+$", "Телефон может содержать цифры, +, (), и пробелы.");
        }

        private void ValidateField(TextBox field, string pattern, string errorMessage)
        {
            var contacts = (ContactList)Resources["contacts"];
            Validation.ValidateSingleInput(field.Text, pattern, errorMessage, field);
            CommandManager.InvalidateRequerySuggested();
        }
    }
}