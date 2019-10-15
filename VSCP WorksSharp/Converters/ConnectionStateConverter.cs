using System;
using System.Windows;
using System.Windows.Data;
using VscpHelperLibWrapper.Enums;

namespace VscpWorksSharp.Converters
{
    class ConnectionStateConverter: BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;
            ConnectionStateEnum state = (ConnectionStateEnum)value;

            if (targetType == typeof(bool))
            {
                switch (state)
                {
                    case ConnectionStateEnum.Idle:
                        return true;
                    case ConnectionStateEnum.Connecting:
                        return false;
                    case ConnectionStateEnum.Started:
                        return false;
                    case ConnectionStateEnum.Stopped:
                        return true;
                }
            }
            switch (state)
            {
                case ConnectionStateEnum.Idle:
                    return "Start";
                case ConnectionStateEnum.Connecting:
                    return "Abort";
                case ConnectionStateEnum.Started:
                    return "Stop";
                case ConnectionStateEnum.Stopped:
                    return "Start";
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Enum.ToObject(targetType, value);
        }
    }
}
