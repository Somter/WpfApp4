using System.Windows.Input;

namespace Task3
{
    public static class AppCommands
    {
        public static readonly RoutedUICommand Add = new RoutedUICommand("Добавить", "Add", typeof(AppCommands));
        public static readonly RoutedUICommand Edit = new RoutedUICommand("Изменить", "Edit", typeof(AppCommands));
        public static readonly RoutedUICommand Delete = new RoutedUICommand("Удалить", "Delete", typeof(AppCommands));
        public static readonly RoutedUICommand Save = new RoutedUICommand("Сохранить", "Save", typeof(AppCommands));
        public static readonly RoutedUICommand Load = new RoutedUICommand("Загрузить", "Load", typeof(AppCommands));
    }
}
