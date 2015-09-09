using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IDT.ManualShovel
{
    public class SortComparer<T> : IComparer<T>
    {
        private PropertyDescriptor m_PropDesc = null;
        private ListSortDirection m_Direction = ListSortDirection.Ascending;

        public SortComparer(PropertyDescriptor propDesc, ListSortDirection direction)
        {
            m_PropDesc = propDesc;
            m_Direction = direction;
        }

        int IComparer<T>.Compare(T x, T y)
        {
            object xValue = m_PropDesc.GetValue(x);
            object yValue = m_PropDesc.GetValue(y);
            return CompareValues(xValue, yValue, m_Direction);
        }

        private int CompareValues(object xValue, object yValue, ListSortDirection direction)
        {
            int retValue = 0;
            if (xValue is IComparable) //can ask the x value
            {
                retValue = ((IComparable)xValue).CompareTo(yValue);
            }
            else if (yValue is IComparable) //can ask the y value
            {
                retValue = ((IComparable)yValue).CompareTo(xValue);
            }
                //not comparable, compare string representations
            else if (xValue != null && !xValue.Equals(yValue))
            {
                retValue = xValue.ToString().CompareTo(yValue.ToString());
            }
            if (direction == ListSortDirection.Ascending)
                return retValue;
            else
                return retValue * -1;
        }
    }
}