using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSharp.Framework.Model
{
    public class Personality : DependencyObject
    {
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(Personality), new PropertyMetadata(0, null, CoerceValue));

        private static object CoerceValue(DependencyObject d, object baseValue)
        {
            return (int)baseValue >= 3 ? 3 : baseValue;
        }
    }
}
