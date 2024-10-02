using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    public class CreateTweenEffectPopupWindow : EditorWindow
    {
        public TweenActionEffect editEffect;

        private void OnEnable()
        {
            this.minSize = new Vector2(624, 200);
            this.minSize = new Vector2(624, 400);
        }

        public void CreateGUI()
        {
            var animEffectUnits = new ScrollView();
            animEffectUnits.name = "animEffectUnits";

            var labelElement = new TextField("Label");
            labelElement.name = "label";
            var collectionCategoryElement = new TextField("Collection Category");
            collectionCategoryElement.name = "collectionCategory";
            var effectCategoryElement = new TextField("Effect Category");
            effectCategoryElement.name = "effectCategory";


            var saveBtn = new Button(() =>
            {
                editEffect.label = labelElement.text;
                editEffect.collectionCategory = collectionCategoryElement.text;
                editEffect.effectCategory = effectCategoryElement.text;
                Save();
            });
            saveBtn.text = "Save";

            rootVisualElement.Add(animEffectUnits);
            rootVisualElement.Add(labelElement);
            rootVisualElement.Add(collectionCategoryElement);
            rootVisualElement.Add(effectCategoryElement);
            rootVisualElement.Add(saveBtn);

            var path = AssetDatabase.GUIDToAssetPath("757c620178e724f419ab8f4269cca9da");
            string absolutePath = PathUtility.ConvertToAbsolutePath(path);

            var gifContains = new VisualElement();
            gifContains.style.flexDirection = FlexDirection.Column;
            VisualElement gifField1 = CreateGifField(absolutePath);
            VisualElement gifField2 = CreateGifField(absolutePath);

            gifContains.Add(gifField1);
            gifContains.Add(gifField2);

            rootVisualElement.Add(gifContains);

            Refresh();
        }

        private VisualElement CreateGifField(string path)
        {
            var gifField = new GifUIControl(path);
            gifField.style.minHeight = 100;
            gifField.style.minWidth = 100;
            return gifField;
        }

        public void Refresh()
        {
            if (editEffect == null)
            {
                return;
            }

            var labelElement = rootVisualElement.Q<TextField>("label");
            var collectionCategoryElement = rootVisualElement.Q<TextField>("collectionCategory");
            var effectCategoryElement = rootVisualElement.Q<TextField>("effectCategory");

            if (labelElement != null) labelElement.value = editEffect.label;
            if (collectionCategoryElement != null) collectionCategoryElement.value = editEffect.collectionCategory;
            if (effectCategoryElement != null) effectCategoryElement.value = editEffect.effectCategory;
            ScrollView animUnitContainer = rootVisualElement.Q<ScrollView>("animEffectUnits");

            TweenActionEditorWindow.CreateAnimUnits(animUnitContainer, editEffect);
        }

        public void Save()
        {
            var libraries = AssetDatabase.FindAssets("t:Object", new[]
                {
                    TweenTimelineDefine.CustomConfigPath
                })
                .Select(guid => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid)))
                .OfType<ITweenActionLibrary>()
                .ToList();

            Assert.IsTrue(libraries.Count == 1); // PLAN To supprot specific library, such as save as
            foreach (var library in libraries)
            {
                library.AddEffect(editEffect);
                library.ApplyToSettings();
                EditorUtility.SetDirty(library as Object);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
