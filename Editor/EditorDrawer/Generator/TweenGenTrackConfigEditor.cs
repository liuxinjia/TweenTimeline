using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using Cr7Sund.TweenTimeLine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(TweenGenTrackConfig))]
public class TweenGenTrackConfigEditor : Editor
{
    private ListView _listView;
    private VisualElement _container;
    private List<TweenComponentData> _filteredComponentValuePairs = new();
    private string listPropertyName = "componentDatas";

    public override VisualElement CreateInspectorGUI()
    {
        _container = new VisualElement();

        StyleColor styleColor = new StyleColor(UnityEngine.Color.white);
        // _container.style.backgroundColor = new StyleColor(UnityEngine.Color.white * 0.9f);

        var _searchField = new ToolbarSearchField();
        _searchField.tooltip = "Search by component type or value type";
        _searchField.RegisterValueChangedCallback(evt => OnSearchChanged(evt.newValue));
        _container.Add(_searchField);

        CreateListView();
        return _container;
    }

    private void CreateListView()
    {
        _listView = new ListView();
        _listView.selectionType = SelectionType.Single;
        _listView.style.flexGrow = 1.0f;
        _listView.itemsSource = _filteredComponentValuePairs;
        _listView.makeItem = CreateListItem;
        _listView.bindItem = BindListItem;
        _listView.reorderable = true;
        _listView.allowAdd = true;
        _listView.allowRemove = true;
        _listView.fixedItemHeight = 125;
        _listView.style.maxHeight = 9 * _listView.fixedItemHeight;
        _container.Add(_listView);

        var config = (TweenGenTrackConfig)target;
        _filteredComponentValuePairs = new List<TweenComponentData>(config.componentDatas);
        RefreshListView();
    }


    private void OnSearchChanged(string searchText)
    {
        var config = (TweenGenTrackConfig)target;

        if (string.IsNullOrEmpty(searchText))
        {
            return;
        }
        
        _filteredComponentValuePairs = config.componentDatas
            .FindAll(pair =>
                FuzzyMatch(pair.GetPropertyMethod, searchText)
                || FuzzyMatch(pair.ComponentType, searchText)
            );

        RefreshListView();
    }

    public static bool FuzzyMatch(string target, string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return true;
        }
        target = target.ToLower();
        searchTerm = searchTerm.ToLower();
        return target.Contains(searchTerm);
    }

    private void RefreshListView()
    {

        _listView.itemsSource = _filteredComponentValuePairs;
        _listView.RefreshItems();
        _listView.Rebuild();
    }

    private VisualElement CreateListItem()
    {
        var container = new VisualElement();
        var propField = new PropertyField();
        container.Add(propField);
        return container;
    }

    private void BindListItem(VisualElement element, int index)
    {
        element.Clear();

        if (index >= _filteredComponentValuePairs.Count) return;

        var componentValuePair = _filteredComponentValuePairs[index];
        var config = (TweenGenTrackConfig)target;
        int realIndex = config.componentDatas.FindIndex(pair => pair.Equals(componentValuePair));

        if (realIndex < 0 || realIndex >= config.componentDatas.Count) return;

        SerializedProperty componentValuePairProperty =
            serializedObject.FindProperty(listPropertyName).GetArrayElementAtIndex(realIndex);

        PropertyField propField = new PropertyField();
        element.Add(propField);
        propField.BindProperty(componentValuePairProperty);
    }
}
