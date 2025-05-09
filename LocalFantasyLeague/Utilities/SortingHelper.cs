using Microsoft.AspNetCore.Components;

namespace LocalFantasyLeague.Utilities
{
    public static class SortingHelper
    {
        public static void SortTable<T>(ref List<T> items, string columnName, ref string currentSortColumn, ref bool isSortAscending)
        {
            if (currentSortColumn == columnName)
            {
                // Toggle sort direction
                isSortAscending = !isSortAscending;
            }
            else
            {
                // Set new sort column and default to ascending
                currentSortColumn = columnName;
                isSortAscending = true;
            }

            // Sort the items
            items = isSortAscending
                ? items.OrderByDescending(item => GetPropertyValue(item, columnName)).ToList()
                : items.OrderBy(item => GetPropertyValue(item, columnName)).ToList();
        }

        public static RenderFragment RenderSortIcon(string columnName, string currentSortColumn, bool isSortAscending) => builder =>
        {
            if (currentSortColumn == columnName)
            {
                builder.OpenElement(0, "i");
                builder.AddAttribute(1, "class", isSortAscending ? "bi bi-arrow-down" : "bi bi-arrow-up");
                builder.CloseElement();
            }
        };

        // Helper method to get property value dynamically
        private static object GetPropertyValue<T>(T item, string propertyName)
        {
            return typeof(T).GetProperty(propertyName)?.GetValue(item, null) ?? 0;
        }

    }
}
