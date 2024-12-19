using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp4
{
    public class ContactL : DependencyObject
    {
        public ContactL()
        {
            SelectedContact = new Contact();
        }

        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();

        public static readonly DependencyProperty SelectedContactProperty =
            DependencyProperty.Register(
                nameof(SelectedContact),
                typeof(Contact),
                typeof(ContactL),
                new PropertyMetadata(null));

        public Contact SelectedContact
        {
            get => (Contact)GetValue(SelectedContactProperty);
            set => SetValue(SelectedContactProperty, value);
        }
    }
}
