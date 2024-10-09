using System;
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

        VisualElement listView = CreateListView();

        var createCfgContainer = new VisualElement();
        createCfgContainer.style.flexDirection = FlexDirection.Column;
        TextField componentTextField = new TextField("Component");
        TextField filedTextField = new TextField("FieldName");
        var btn = new Button(() =>
        {
            var config = (TweenGenTrackConfig)target;
            var componentType = GetComponentType(componentTextField.value);
            Type valueType = null;
            string fieldName = filedTextField.value;
            var fieldInfo = componentType.GetField(fieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (fieldInfo != null)
                valueType = fieldInfo.FieldType;
            var propInfo = componentType.GetProperty(fieldName,
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (propInfo != null)
                valueType = propInfo.PropertyType;
            string valueTypeName = valueType?.Name;

            TweenComponentData item = new TweenComponentData();
            item.ComponentType = componentType.FullName;
            item.ValueType = valueTypeName;
            item.GetPropertyMethod = fieldName;
            item.SetPropertyMethod = $"target.{fieldName} = updateValue; ";
            item.PreTweenMethod = "Custom";
            config.componentDatas.Add(item);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(config);
            AssetDatabase.SaveAssetIfDirty(config);
        });
        createCfgContainer.Add(componentTextField);
        createCfgContainer.Add(filedTextField);
        createCfgContainer.Add(btn);
        btn.text = "Add new Cfg";

        _container.Add(createCfgContainer);
        _container.Add(_searchField);
        _container.Add(listView);

        return _container;
    }

    public Type GetComponentType(string componentTypeFullName)
    {
        Type type = null;
        string typeName = componentTypeFullName;

        if (!componentTypeFullName.StartsWith("UnityEngine."))
        {
            typeName = $"UnityEngine.{componentTypeFullName}";
        }
        type = typeof(UnityEngine.Rigidbody).Assembly.GetType(typeName);

        if (type == null)
        {
            if (!componentTypeFullName.StartsWith("UnityEngine.UI"))
            {
                typeName = $"UnityEngine.UI.{componentTypeFullName}";
            }
            type = typeof(UnityEngine.UI.Graphic).Assembly.GetType(typeName);
        }
        if (type == null)
        {
            if (!componentTypeFullName.StartsWith("TMPro."))
            {
                typeName = $"TMPro.{componentTypeFullName}";
            }
            type = typeof(TMPro.TMP_Text).Assembly.GetType(componentTypeFullName);
        }

        return type;
    }

    private VisualElement CreateListView()
    {
        _listView = new ListView();
        // _listView.itemsSource = _filteredComponentValuePairs;
        _listView.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
        _listView.makeItem = CreateListItem;
        _listView.bindItem = BindListItem;
        _listView.reorderable = true;
        _listView.allowAdd = true;
        _listView.allowRemove = true;
        _listView.showAddRemoveFooter = true;
        // _listView.fixedItemHeight = 125;
        // _listView.style.maxHeight = 9 * _listView.fixedItemHeight;

        var config = (TweenGenTrackConfig)target;
        _filteredComponentValuePairs = new List<TweenComponentData>(config.componentDatas);
        RefreshListView();

        return _listView;
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
                || FuzzyMatch(pair.PreTweenMethod, searchText)
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
        // container.Add(propField);
        return propField;
    }

    private void BindListItem(VisualElement element, int index)
    {

        if (index >= _filteredComponentValuePairs.Count) return;

        var componentValuePair = _filteredComponentValuePairs[index];
        var config = (TweenGenTrackConfig)target;
        int realIndex = config.componentDatas.FindIndex(pair => pair.Equals(componentValuePair));

        if (realIndex < 0 || realIndex >= config.componentDatas.Count) return;

        SerializedProperty componentValuePairProperty =
            serializedObject.FindProperty(listPropertyName).GetArrayElementAtIndex(realIndex);

        // PropertyField propField = new PropertyField();
        PropertyField propField = element as PropertyField;
        // element.Add(propField);
        propField.BindProperty(componentValuePairProperty);
    }
}
