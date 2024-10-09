using System.Collections.ObjectModel;

internal static class ExtensionMethods
{
    internal static bool IsEmptyOrNull<T>(this ObservableCollection<T> coll)
    {
        if (coll != null && coll.Count > 0) return false;
        else return true;
    }
}
