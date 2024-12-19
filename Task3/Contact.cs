using System.ComponentModel;

namespace Task3
{
    public class Contact : INotifyPropertyChanged
    {
        private readonly Dictionary<string, string> fields = new Dictionary<string, string>();

        public string Name
        {
            get => GetField(nameof(Name));
            set => SetField(nameof(Name), value);
        }

        public string Surname
        {
            get => GetField(nameof(Surname));
            set => SetField(nameof(Surname), value);
        }

        public string Address
        {
            get => GetField(nameof(Address));
            set => SetField(nameof(Address), value);
        }

        public string Phone
        {
            get => GetField(nameof(Phone));
            set => SetField(nameof(Phone), value);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string GetField(string propertyName)
        {
            return fields.TryGetValue(propertyName, out var value) ? value : string.Empty;
        }

        private void SetField(string propertyName, string value)
        {
            if (fields.TryGetValue(propertyName, out var currentValue) && currentValue == value)
                return;

            fields[propertyName] = value;
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}