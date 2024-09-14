using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Cr7Sund.Timeline.Extension;
using PrimeTween;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    public class TweenActionEditorWindow : EditorWindow
    {
        private AnimTokenPresets animTokenPresets;
        private TweenActionLibrary _tweenActionContainer;
        private EasingTokenPresetLibrary _easingTokenPresetLibrary;
        private TweenActionEffect _selectTweenAction = new();
        private Sequence _curSequence;
        private VisualElement selecGridItem;


        // [ContextMenu("GameObject/AnimationEditor")]
        [MenuItem("GameObject/Animation Editor %T", priority = 1)]
        public static void ShowWindow()
        {
            var window = GetWindow<TweenActionEditorWindow>();
            window.maxSize = TweenTimelineDefine.windowMaxSize;
            window.minSize = TweenTimelineDefine.windowMaxSize;
        }

        #region LifeTime
        public void OnEnable()
        {
            this.maxSize = TweenTimelineDefine.windowMaxSize;
            this.minSize = TweenTimelineDefine.windowMaxSize;

            // AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
            // AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;

            LoadAssets();
            InitTimeline();
        }

        private void InitTimeline()
        {
            TweenTimelineManager.InitTimeline();
        }

        public void OnDestroy()
        {
            TweenTimelineManager.ResetDefaultAllClip();
            TweenTimelineManager.TryRemoveTweenManager();
        }

        private void OnBeforeAssemblyReload()
        {
            Close();
        }
        #endregion

        public void CreateGUI()
        {
            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(TweenTimelineDefine.windowVisualTreeAssetGUID));
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(TweenTimelineDefine.windowStyleGUID));
            rootVisualElement.styleSheets.Add(styleSheet);
            TemplateContainer root = visualTreeAsset.Instantiate();
            rootVisualElement.Add(root);

            UpdateConfigUI(_selectTweenAction);
            CreateAnimUnits(_selectTweenAction);
            BindActionBtns();
            CreateTab();
        }

        #region UI
        private void CreateTab()
        {
            var rootTabView = rootVisualElement.Q<TabView>("rooTabView");
            // var actionCategoryList = animContainer.GetAnimActionCategory(_selectAnimationAction.target);

            for (int i = 0; i < _tweenActionContainer.animationContainers.Count; i++)
            {
                TweenCollection container = _tweenActionContainer.animationContainers[i];
                var tab = new Tab(container.category);
                rootTabView.Add(tab);
                var tabContainer = new VisualElement();
                tabContainer.name = $"tabContainer_{i}";
                tab.Add(tabContainer);

                var toolbarSearchField = new ToolbarSearchField();
                toolbarSearchField.RegisterValueChangedCallback(OnSearchTextChanged);
                tabContainer.Add(toolbarSearchField);

                var grid = CreateGrid(tabContainer);
            }
            rootTabView.activeTabChanged += (t1, t2) =>
            {
                UpdateSelectCollectionContainer();
            };
            UpdateSelectCollectionContainer();
        }

        private void BindActionBtns()
        {
            var container = rootVisualElement.Q<VisualElement>("ConfigContainer");
            Button previewTweenBtn = container.Query<Button>("previewTweenBtn");
            Button addTrackBtn = container.Query<Button>("addTrackBtn");
            Button createEffectBtn = container.Query<Button>("createEffectBtn");

            previewTweenBtn.clicked += () =>
            {
                CheckValidTarget();
                PlayTween(_selectTweenAction);
            };

            addTrackBtn.clicked += () =>
            {
                AssertTimelineOpen();
                CheckValidTarget();
                AddTracks(_selectTweenAction);
            };
            createEffectBtn.clicked += CreateNewAnimEffect;
            DrawToolBtns();
        }


        private void AddTracks(TweenActionEffect selectTweenAction)
        {
            if (selectTweenAction == null)
            {
                return;
            }
            foreach (var animUnit in selectTweenAction.animationSteps)
            {
                TrackInfo trackInfo;
                Component component;
                animUnit.GetAnimUnitTrackInfo(selectTweenAction, _easingTokenPresetLibrary, out trackInfo, out component);
                Type componentType = animUnit.GetComponentType();
                component = selectTweenAction.target.GetComponent(componentType);
                // var behaviourName = $"Cr7Sund.TweenTimeLine.{animUnit.tweenMethod}ControlBehaviour";
                string animUnitTweenMethod = animUnit.tweenMethod.Replace("ControlBehaviour", "");
                var trackName = $"Cr7Sund.{componentType.Name}Tween.{animUnitTweenMethod}ControlTrack";
                var assetName = $"Cr7Sund.{componentType.Name}Tween.{animUnitTweenMethod}ControlAsset";
                var rooTabView = rootVisualElement.Q<TabView>("rooTabView");

                Assembly assembly = TweenActionStep.GetTweenTrackAssembly(trackName);
                TweenTimelineManager.AddTrack(component,
                    assembly.GetType(trackName),
                    assembly.GetType(assetName),
                    trackInfo,
                    rooTabView.selectedTabIndex == 0
                );
            }

            TimelineEditor.Refresh(RefreshReason.ContentsAddedOrRemoved);
        }

        private void CheckValidTarget()
        {
            if (_selectTweenAction.target == null)
            {
                throw new Exception("Please select a game object first");
            }
        }

        #region Toolbar Buttons
        private void DrawToolBtns()
        {
            var previewBtn = rootVisualElement.Q<Button>("preViewBtn");
            var recordBtn = rootVisualElement.Q<Button>("recordBtn");
            Button playBtn = rootVisualElement.Q<Button>("playBtn");
            previewBtn.SetEnabled(false);

            playBtn.RegisterCallback<ClickEvent>(_ =>
            {
                AssertTimelineOpen();
                var previousState = TweenTimeLineDataModel.StateInfo.BehaviourState;
                TweenTimelineManager.Play();
                if (previousState == TweenTimeLineDataModel.StateInfo.BehaviourState)
                {
                    Debug.Log("please check the timeline is valid");
                }
            });

            recordBtn.RegisterCallback<ClickEvent>(_ =>
            {
                AssertTimelineOpen();
                TweenTimelineManager.ToggleRecordAllSelectClip();
            });
            this.titleContent = new GUIContent("Animation Editor");

            RefreshBtns();
        }

        private void AssertTimelineOpen()
        {
            if (!TimelineWindowExposer.IsValidTimelineWindow())
            {
                throw new Exception("Please Open the timeline window first");
            }
            else
            {
                // InitTimeline();
            }
        }

        private void RefreshBtnImges()
        {
            Button playBtn = rootVisualElement.Q<Button>("playBtn");
            var recordBtn = rootVisualElement.Q<Button>("recordBtn");

            playBtn.iconImage = TweenTimeLineDataModel.StateInfo.IsPlaying ? TweenTimelineDefine.plaBackground : TweenTimelineDefine.stopBackground;
            recordBtn.iconImage = TweenTimeLineDataModel.StateInfo.IsRecording ? TweenTimelineDefine.recordOnBackground : TweenTimelineDefine.recordOffBackground;
        }

        public void LoadBtnsAssets()
        {
            if (TweenTimelineDefine.recordOnBackground != null)
            {
                return;
            }
            TweenTimelineDefine.recordOnBackground = new Background()
            {
                texture = (Texture2D)EditorGUIUtility.IconContent("d_Record On@2x").image
            };
            TweenTimelineDefine.recordOffBackground = new Background()
            {
                texture = (Texture2D)EditorGUIUtility.IconContent("d_Record Off@2x").image
            };
            TweenTimelineDefine.plaBackground = new Background()
            {
                texture = (Texture2D)EditorGUIUtility.IconContent("PauseButton On@2x").image
            };
            TweenTimelineDefine.stopBackground = new Background()
            {
                texture = (Texture2D)EditorGUIUtility.IconContent("PlayButton@2x").image
            };
        }

        public void RefreshBtns()
        {
            var previewBtn = rootVisualElement.Q<Button>("preViewBtn");
            var recordBtn = rootVisualElement.Q<Button>("recordBtn");
            Button playBtn = rootVisualElement.Q<Button>("playBtn");

            RefreshBtnImges();

            OnUpdateRunBtn(previewBtn, () => TweenTimeLineDataModel.StateInfo.IsPreview);
            OnUpdateRunBtn(recordBtn, () => TweenTimeLineDataModel.StateInfo.IsRecording);
            OnUpdateRunBtn(playBtn, () => TweenTimeLineDataModel.StateInfo.IsPlaying);
        }

        private void OnUpdateRunBtn(Button recordBtn, Func<bool> func)
        {
            bool result = func.Invoke();
            if (result)
            {
                recordBtn.EnableInClassList("DisActiveBtn", false);
                recordBtn.EnableInClassList("ActiveBtn", true);
            }
            else
            {
                recordBtn.EnableInClassList("DisActiveBtn", true);
                recordBtn.EnableInClassList("ActiveBtn", false);
            }
        }


        #endregion

        private void UpdateConfigUI(TweenActionEffect selectTweenAction)
        {
            var container = rootVisualElement.Q<VisualElement>("ConfigContainer");

            var animSettingType = animTokenPresets.GetAnimationSettingType(selectTweenAction.durationToken, selectTweenAction.easeToken);
            CreateSettingParisField(container);

            // Update Duration Field
            var durationField = container.Q<EnumField>("CustomDurationField");
            durationField.value = selectTweenAction.durationToken;
            durationField.RegisterValueChangedCallback(evt =>
            {
                selectTweenAction.durationToken = (DurationToken)evt.newValue;
            });

            // Update Ease Field
            var easeField = container.Q<EnumField>("CustomEasingField");

            easeField.value = selectTweenAction.easeToken;
            easeField.RegisterValueChangedCallback(evt =>
            {
                selectTweenAction.easeToken = (MaterialEasingToken)evt.newValue;
            });

            // Update Target Field
            var componentField = container.Q<ObjectField>("TargetField");
            if (componentField.value == null)
            {
                componentField.value = Selection.activeGameObject;
                selectTweenAction.target = (GameObject)componentField.value;
            }
            componentField.RegisterValueChangedCallback(evt =>
            {
                selectTweenAction.target = (GameObject)evt.newValue;
                UpdateSelectCollectionContainer();
            });

            ToggleTweenAnimParis(durationField, easeField, animSettingType);
        }

        private void UpdateSelectCollectionContainer()
        {
            var tabView = rootVisualElement.Q<TabView>("rooTabView");
            var container = tabView.Q<VisualElement>($"tabContainer_{tabView.selectedTabIndex}");
            var grid = container.Q<VisualElement>("grid");
            // for some reason , active tab is not work
            var animContainer = _tweenActionContainer.animationContainers[tabView.selectedTabIndex];
            var actionCategoryList = animContainer.GetAnimActionCategory(_selectTweenAction.target);
            UpdateActionCollectionContainer(actionCategoryList, animContainer, grid);
        }

        private void CreateAnimUnits(TweenActionEffect selectTweenAction)
        {
            VisualElement animUnitContainer = rootVisualElement.Q("BottomParamsPart");
            CreateAnimUnits(animUnitContainer, selectTweenAction);
        }

        public static void CreateAnimUnits(VisualElement animUnitContainer, TweenActionEffect selectTweenAction)
        {
            if (selectTweenAction == null)
            {
                return;
            }
            animUnitContainer.Clear();
            foreach (var item in selectTweenAction.animationSteps)
            {
                CreateAnimUnit(animUnitContainer, item, selectTweenAction.animationSteps.Count > 1);
            }
        }

        private void CreateSettingParisField(VisualElement container)
        {
            var animationSettingField = container.Q<EnumField>("AnimTokenPreset");
            var animSettingType = animTokenPresets.GetAnimationSettingType(_selectTweenAction.durationToken, _selectTweenAction.easeToken);
            animationSettingField.value = animSettingType;
            animationSettingField.RegisterValueChangedCallback(evt =>
            {
                var selectedSettingType = (TimeEasePairs)evt.newValue;
                var easeField = container.Q<EnumField>("CustomEasingField");
                var durationField = container.Q<EnumField>("CustomDurationField");
                ToggleTweenAnimParis(durationField, easeField, selectedSettingType);

                if (selectedSettingType == TimeEasePairs.Custom)
                {
                    return;
                }
                var settings = animTokenPresets.GetSettings(selectedSettingType);
                _selectTweenAction.durationToken = settings.duration;
                _selectTweenAction.easeToken = settings.easing;

                easeField.value = settings.easing;
                durationField.value = settings.duration;
            });
        }

        private static void ToggleTweenAnimParis(EnumField durationField, EnumField easeField, TimeEasePairs selectedSettingType)
        {
            if (selectedSettingType == TimeEasePairs.Custom)
            {
                durationField.SetEnabled(true);
                easeField.SetEnabled(true);
            }
            else
            {
                durationField.SetEnabled(false);
                easeField.SetEnabled(false);
            }
        }

        private static void CreateAnimUnit(VisualElement container, TweenActionStep animActionUnit, bool showLabel)
        {
            // Instantiate the UXML prefab
            var animUnitPrefab = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(TweenTimelineDefine.unitItemVisualTreeAssetGUID));
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(TweenTimelineDefine.unitItemStyleGUID));
            var animUnit = animUnitPrefab.CloneTree();
            animUnit.styleSheets.Add(styleSheet);
            animUnit.name = "animUnitPrefab";

            // Set the label
            var label = animUnit.Q<Label>("label");
            if (showLabel)
            {
                label.text = animActionUnit.label;
            }
            else
            {
                label.style.display = DisplayStyle.None;
            }

            // Destination Position Fields
            var componentValueType = animActionUnit.GetComponentValueType();
            var endPosField = AniActionEditToolHelper.CreateValueField("EndValue", componentValueType, animActionUnit.EndPos, (newValue) =>
            {
                animActionUnit.EndPos = newValue;
            });
            endPosField.name = "EndPosField";
            var startPosField = AniActionEditToolHelper.CreateValueField("StartValue", componentValueType, animActionUnit.StartPos, (newValue) =>
            {
                animActionUnit.StartPos = newValue;
            });
            startPosField.name = "StartPosField";
            startPosField.style.display = (animActionUnit.isRelative) ? DisplayStyle.None : DisplayStyle.Flex;
            var paramsPart = animUnit.Q<VisualElement>("paramsPart");

            paramsPart.Add(endPosField);
            paramsPart.Add(startPosField);

            // Set the toggle
            var toggle = animUnit.Q<Toggle>("relative");
            toggle.value = animActionUnit.isRelative;
            // toggle.style.display = animActionUnit.isRelative ? DisplayStyle.Flex : DisplayStyle.None;
            toggle.RegisterValueChangedCallback(evt =>
            {
                animActionUnit.isRelative = evt.newValue;
                startPosField.style.display = (animActionUnit.isRelative) ? DisplayStyle.None : DisplayStyle.Flex;
            });

            // Add the animUnit to the container
            container.Add(animUnit);
        }

        private void UpdateActionCollectionContainer(Dictionary<string, List<TweenActionEffect>> actionCategoryList, TweenCollection animCollections, VisualElement grid)
        {
            grid.Clear();


            foreach (var kvp in actionCategoryList)
            {
                if (kvp.Value.Count > 0)
                {
                    CreateCategoryTitle(kvp.Key, grid);
                }
                foreach (var item in kvp.Value)
                {
                    CreateGridItem(grid, item);
                }
            }
        }

        private VisualElement CreateGrid(VisualElement container)
        {
            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(TweenTimelineDefine.gridVisualTreeAssetGUID));
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(TweenTimelineDefine.gridItemStyleGUID));
            rootVisualElement.styleSheets.Add(styleSheet);
            var gridContainer = visualTreeAsset.Instantiate();
            container.Add(gridContainer);
            return gridContainer;
        }

        private void CreateCategoryTitle(string category, VisualElement container)
        {
            var categoryLabel = new Label(category)
            {
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    marginTop = 10,
                    marginBottom = 5,
                    width = new Length(100, LengthUnit.Percent) // 确保CategoryTitle独占一行
                }
            };

            container.Add(categoryLabel);
        }

        private void CreateGridItem(VisualElement grid, TweenActionEffect animAction)
        {
            // Load the UXML file
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(TweenTimelineDefine.gridItemVisualTreeAssetGUID));
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(TweenTimelineDefine.gridItemItemStyleGUID));

            // Clone the UXML and apply styles
            var item = visualTree.CloneTree().Q<VisualElement>("gridItem");
            item.styleSheets.Add(styleSheet);
            item.EnableInClassList("Select", false);

            // Set the label text
            var label = item.Q<Label>("label");
            label.text = animAction.label;

            // Set the image icon
            var img = item.Q<Image>("icon");
            var extension = Path.GetExtension(animAction.image);
            if (extension == ".gif"
            && File.Exists(animAction.image))
            {
                GifUIControl gif = new GifUIControl(animAction.image);
                gif.name = "gif";
                img.Add(gif);
            }
            else
            {
                img.style.backgroundImage = AniActionEditToolHelper.LoadImageFromPath(animAction.image);
            }


            // Add the item to the grid
            grid.Add(item);
            // Register click event
            item.RegisterCallback<MouseDownEvent>(ev =>
            {
                // right Click 
                if (ev.button == 1)
                {
                    var menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Add to Current AnimEffect"), false, () => OnCombineTrack(animAction));
                    // menu.AddItem(new GUIContent("Delete"), false, () => DeleteAnimationEffect(animAction));
                    menu.ShowAsContext();
                }
                // left Click
                else if (ev.button == 0)
                {
                    if (selecGridItem != null)
                    {
                        selecGridItem.EnableInClassList("Select", false);
                    }
                    selecGridItem = item;
                    selecGridItem.EnableInClassList("Select", true);
                    OnAnimationEffectSelect(animAction);
                }
            });
        }

        private void LoadAssets()
        {
            LoadBtnsAssets();
            _easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(TweenTimelineDefine.easingTokenPresetsPath);
            _tweenActionContainer = AssetDatabase.LoadAssetAtPath<TweenActionLibrary>(TweenTimelineDefine.tweenLibraryPath);
            animTokenPresets = AssetDatabase.LoadAssetAtPath<AnimTokenPresets>(TweenTimelineDefine.animTokenPresetsPath);
        }
        #endregion

        #region Actions
        void OnSearchTextChanged(ChangeEvent<string> evt)
        {

            var tabView = rootVisualElement.Q<TabView>("rooTabView");
            var container = tabView.Q<VisualElement>($"tabContainer_{tabView.selectedTabIndex}");
            var grid = container.Q<VisualElement>("grid");
            var animContainer = _tweenActionContainer.animationContainers[tabView.selectedTabIndex];
            Dictionary<string, List<TweenActionEffect>> actionCategoryList = null;
            if (!string.IsNullOrEmpty(evt.newValue))
            {
                actionCategoryList = animContainer.FilterActionCategory(evt.newValue, _selectTweenAction.target);
            }
            else
            {
                actionCategoryList = animContainer.GetAnimActionCategory(_selectTweenAction.target);
            }
            UpdateActionCollectionContainer(actionCategoryList, animContainer, grid);
        }

        private void OnAnimationEffectSelect(TweenActionEffect animAction)
        {
            if (_selectTweenAction.target == null)
            {
                Debug.LogError("Please select a game object first");
            }
            _selectTweenAction.CopyFrom(animAction);
            UpdateConfigUI(_selectTweenAction);
            CreateAnimUnits(_selectTweenAction);
        }

        private void OnCombineTrack(TweenActionEffect animAction)
        {
            if (animAction == null || animAction.target == null)
            {
                return;
            }
            foreach (var item in animAction.animationSteps)
            {
                if (!_selectTweenAction.animationSteps.Exists(step => step.label.Equals(item.label)
                 && step.tweenMethod.Equals(step.tweenMethod)))
                {
                    _selectTweenAction.animationSteps.Add(item);
                }
            }
            UpdateConfigUI(_selectTweenAction);
            CreateAnimUnits(_selectTweenAction);
        }

        public void CreateNewAnimEffect()
        {
            var window = EditorWindow.GetWindow<CreateTweenEffectPopupWindow>();
            window.editEffect = _selectTweenAction;
            window.Refresh();
        }

        private void PlayTween(TweenActionEffect animAction)
        {
            if (animAction == null || animAction.target == null)
            {
                return;
            }

            if (_curSequence.isAlive)
            {
                _curSequence.Complete();
                _curSequence.Stop();
            }

            _curSequence = Sequence.Create();
            var onResetActions = new List<Action>();
            foreach (var animUnit in animAction.animationSteps)
            {
                var tween = animUnit.GenerateTween(animAction, _easingTokenPresetLibrary, out var onResetAction);

                if (animUnit.startTimeOffset < 0)
                {
                    _curSequence.Group(Sequence.Create().ChainDelay(AniActionEditToolHelper.ConvertDuration(animUnit.startTimeOffset)).Chain(tween));
                }
                else
                {
                    _curSequence.Group(tween);
                }
                onResetActions.Add(onResetAction);
            }

            _curSequence.ChainDelay(0.8f).OnComplete(() =>
            {
                foreach (var item in onResetActions)
                {
                    item?.Invoke();
                }
            });

            EditorTweenCenter.RegisterSequence(_curSequence, animAction.target, animAction.ConvertDuration() + 1f);
        }
        #endregion
    }
}
