using System.Windows;


namespace WpfApp4
{
    public class Contact : DependencyObject
    {
        public Contact() { }

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(Contact));

        public static readonly DependencyProperty SurnameProperty =
            DependencyProperty.Register("Surname", typeof(string), typeof(Contact));

        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register("Address", typeof(string), typeof(Contact));

        public static readonly DependencyProperty PhoneProperty =
            DependencyProperty.Register("Phone", typeof(string), typeof(Contact));

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public string Surname
        {
            get => (string)GetValue(SurnameProperty);
            set => SetValue(SurnameProperty, value);
        }

        public string Address
        {
            get => (string)GetValue(AddressProperty);
            set => SetValue(AddressProperty, value);
        }

        public string Phone
        {
            get => (string)GetValue(PhoneProperty);
            set => SetValue(PhoneProperty, value);
        }
    }
}

