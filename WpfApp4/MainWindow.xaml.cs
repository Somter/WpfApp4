using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Contact> Contacts { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Contacts = new ObservableCollection<Contact>();
            listBoxContacts.ItemsSource = Contacts;
        }

        private void ClearInputFields()
        {
            textBoxFullName.Clear();
            textBoxAddress.Clear();
            textBoxPhone.Clear();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFullName.Text) ||
                string.IsNullOrWhiteSpace(textBoxAddress.Text) ||
                string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля перед добавлением!", "Ошибка");
                return;
            }

            var contact = new Contact
            {
                FullName = textBoxFullName.Text,
                Address = textBoxAddress.Text,
                Phone = textBoxPhone.Text
            };

            Contacts.Add(contact);

            ClearInputFields();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxContacts.SelectedItem is Contact selectedContact)
            {
                selectedContact.FullName = textBoxFullName.Text;
                selectedContact.Address = textBoxAddress.Text;
                selectedContact.Phone = textBoxPhone.Text;

                MessageBox.Show("Изменения применены!", "Успех");
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите контакт из списка.", "Ошибка");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxContacts.SelectedItem is Contact selectedContact)
            {
                Contacts.Remove(selectedContact);
                MessageBox.Show("Контакт удалён из записной книжки.", "Успех");
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите контакт из списка для удаления.", "Ошибка");
            }
        }

        private void listBoxContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxContacts.SelectedItem is Contact contact)
            {
                textBoxFullName.Text = contact.FullName;
                textBoxAddress.Text = contact.Address;
                textBoxPhone.Text = contact.Phone;
            }
            else
            {
                ClearInputFields();
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dataToSave = Contacts.Select(c => new ContactL
                {
                    FullName = c.FullName,
                    Address = c.Address,
                    Phone = c.Phone
                }).ToList();

                string jsonString = JsonSerializer.Serialize(dataToSave);
                File.WriteAllText("contacts.json", jsonString);

                MessageBox.Show("Данные успешно сохранены!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists("contacts.json"))
                {
                    string jsonString = File.ReadAllText("contacts.json");
                    var loadedData = JsonSerializer.Deserialize<List<ContactL>>(jsonString);

                    Contacts.Clear();
                    foreach (var data in loadedData)
                    {
                        var contact = new Contact
                        {
                            FullName = data.FullName,
                            Address = data.Address,
                            Phone = data.Phone,
                        };
                        Contacts.Add(contact);
                    }

                    MessageBox.Show("Данные успешно загружены!", "Успех");
                }
                else
                {
                    MessageBox.Show("Файл с контактами не найден.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}