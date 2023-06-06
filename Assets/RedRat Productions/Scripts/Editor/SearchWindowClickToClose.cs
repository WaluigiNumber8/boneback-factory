using UnityEditor;
using UnityEditor.Search;

namespace UnityEditorExtensions
{
    public class SearchWindowClickToClose
    {
        [MenuItem("Window/Search/New Window (close by clicking)", priority = 1)]
        public static void OpenPopupWindow()
        {
            SearchService.ShowWindow(defaultWidth: 851, defaultHeight: 539, dockable: false);
        }
    }
}