using System.Collections.Generic;
using System.ComponentModel;

namespace IDT.ManualShovel
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool m_Sorted = false;
        private ListSortDirection m_SortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor m_SortProperty = null;

        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        protected override bool IsSortedCore
        {
            get
            {
                return m_Sorted;
            }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return m_SortDirection;
            }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return m_SortProperty;
            }
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            m_SortDirection = direction;
            m_SortProperty = prop;
            var listRef = this.Items as List<T>;
            if (listRef == null)
                return;
            var comparer = new SortComparer<T>(prop, direction);

            listRef.Sort(comparer);

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }
}
