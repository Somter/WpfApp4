using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Task3
{
    public static class Commands
    {
        public static RoutedUICommand AddContact { get; } = new RoutedUICommand(
            "Add Contact", "AddContact", typeof(Commands),
            new InputGestureCollection { new KeyGesture(Key.N, ModifierKeys.Control) });
        public static RoutedUICommand EditContact { get; } = new RoutedUICommand(
            "Edit Contact", "EditContact", typeof(Commands),
            new InputGestureCollection { new KeyGesture(Key.E, ModifierKeys.Control) });
        public static RoutedUICommand DeleteContact { get; } = new RoutedUICommand(
            "Delete Contact", "DeleteContact", typeof(Commands),
            new InputGestureCollection { new KeyGesture(Key.Delete) });
        public static RoutedUICommand SaveContacts { get; } = new RoutedUICommand(
            "Save Contacts", "SaveContacts", typeof(Commands),
            new InputGestureCollection { new KeyGesture(Key.S, ModifierKeys.Control) });
        public static RoutedUICommand LoadContacts { get; } = new RoutedUICommand(
            "Load Contacts", "LoadContacts", typeof(Commands),
            new InputGestureCollection { new KeyGesture(Key.O, ModifierKeys.Control) });
    }
}