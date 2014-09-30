using System.Windows;
using System.Windows.Controls;


namespace DevCor
{
    class WaterMark : DependencyObject
    {
        public static readonly DependencyProperty TextLengthProperty = DependencyProperty.RegisterAttached("TextLength", typeof(int), typeof(WaterMark), new UIPropertyMetadata(0));
        private static readonly DependencyProperty HasTextProperty = DependencyProperty.RegisterAttached("HasText", typeof(bool), typeof(WaterMark), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsMonitoringProperty = DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(WaterMark), new UIPropertyMetadata(false, OnIsMonitoringChanged));
        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMonitoringProperty);
        }

        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
            set { SetValue(HasTextProperty, value); }
        }

        static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passB = d as PasswordBox;
            if ((bool)e.NewValue)
                passB.PasswordChanged += PasswordChanged;
            else
                passB.PasswordChanged -= PasswordChanged;

        }

        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passB = sender as PasswordBox;
            if (passB == null)
                return;
            SetTextLength(passB, passB.Password.Length);
        }
        public static void SetTextLength(DependencyObject obj, int value)
        {
            obj.SetValue(TextLengthProperty, value);

            if (value >= 1)
                obj.SetValue(HasTextProperty, true);
            else
                obj.SetValue(HasTextProperty, false);
        }

        public static int GetTextLength(DependencyObject obj)
        {
            return (int)obj.GetValue(TextLengthProperty);
        }

    }
}
