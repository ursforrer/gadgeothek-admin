﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ch.hsr.wpf.gadgeothek.main
{
    class convert : IValueConverter
    {
        // For the Convert of the Condition
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is domain.Condition ? value.ToString() : domain.Condition.New.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string ? (domain.Condition)Enum.Parse(typeof(domain.Condition), value as string) : domain.Condition.New;
        }
    }
}
