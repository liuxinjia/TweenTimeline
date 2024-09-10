using System.Collections.Generic;
using Cr7Sund.TweenTimeLine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(TweenGenConfig))]
public class TweenGenConfigEditor : Editor
{
    private TextField _searchField;
    private ListView _listView;
    private VisualElement _root;
    private List<ComponentValuePair> _filteredComponentValuePairs = new();

    public override VisualElement CreateInspectorGUI()
    {
        var config = (TweenGenConfig)target;
        _root = new VisualElement();
        _filteredComponentValuePairs = new List<ComponentValuePair>(config.componentValuePairs);
        serializedObject.Update();

        _searchField = new TextField("Search");
        _searchField.tooltip = "Search by component type or value type";
        _searchField.RegisterValueChangedCallback(evt => OnSearchChanged(evt.newValue));
        _root.Add(_searchField);

        Button genBtn = new Button();
        genBtn.RegisterCallback<ClickEvent>(_ =>
        {
            TweenTimeLineCodeGenerator.GenerateCode(config.componentValuePairs);
        });
        genBtn.text = "Generate";
        _root.Add(genBtn);

        _listView = new ListView();
        _listView.selectionType = SelectionType.Single;
        _listView.style.flexGrow = 1.0f;
        _listView.itemsSource = _filteredComponentValuePairs;
        _listView.makeItem = CreateListItem;
        _listView.bindItem = BindListItem;
        _listView.fixedItemHeight = 125;  // 设置每个项的固定高度
        _listView.style.maxHeight = 9 * _listView.fixedItemHeight; // 限制可视数量为9个
        _root.Add(_listView);

        // 初始化列表
        RefreshListView();
        return _root;
    }

    private void OnSearchChanged(string searchText)
    {
        var config = (TweenGenConfig)target;

        _filteredComponentValuePairs = config.componentValuePairs
            .FindAll(pair =>
                FuzzyMatch(pair.GetPropertyMethod, searchText)
                || FuzzyMatch(pair.SetPropertyMethod, searchText)
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
        var config = (TweenGenConfig)target;
        // _filteredComponentValuePairs = new List<ComponentValuePair>(config.componentValuePairs);
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
        var config = (TweenGenConfig)target;
        int realIndex = config.componentValuePairs.FindIndex(pair => pair.Equals(componentValuePair));

        if (realIndex < 0 || realIndex >= config.componentValuePairs.Count) return;

        SerializedProperty componentValuePairProperty =
            serializedObject.FindProperty("componentValuePairs").GetArrayElementAtIndex(realIndex);

        PropertyField propField = new PropertyField();
        element.Add(propField);
        propField.BindProperty(componentValuePairProperty);
    }
}
