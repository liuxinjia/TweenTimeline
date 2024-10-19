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
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using Assert = UnityEngine.Assertions.Assert;

namespace Cr7Sund.TweenTimeLine
{
    public class TweenActionEditorWindow : EditorWindow
    {
        private AnimTokenPresets _animTokenPresets;
        private TweenActionLibrary _tweenActionContainer;
        private EasingTokenPresetLibrary _easingTokenPresetLibrary;
        private TweenActionEffect _selectTweenAction;
        private Sequence _curSequence;
        private string _updateSequenceID;
        private VisualElement _selectGridItem;

        private string _curRestID;

        [MenuItem("GameObject/TweenAction Editor %T", priority = 1)]
        public static void ShowWindow()
        {
            TweenTimelineManager.InitTimeline();
            Type windowType = System.Type.GetType("UnityEditor.InspectorWindow,UnityEditor.dll");
            EditorWindow inspectorWindow = EditorWindow.GetWindow(windowType);

            var window = EditorWindow.GetWindow<TweenActionEditorWindow>(
                title: "TweenActionEditor"
            //  nameof(TweenActionEditorWindow)
            //     , desiredDockNextTo: new[]
            // {
            // System.Type.GetType("UnityEditor.InspectorWindow,UnityEditor.dll")
            // }
            );
            window.maxSize = TweenTimelineDefine.windowMaxSize;
            window.minSize = TweenTimelineDefine.windowMaxSize;
            if (inspectorWindow)
            {
                Rect position = inspectorWindow.position;
                window.position = position;
            }
        }

        [ContextMenu("GameObject/TweenAction Editor %T")]
        public static void OpenAnimationEditorWindow()
        {
            TweenTimelineManager.InitTimeline();

            var window = GetWindow<TweenActionEditorWindow>();
            window.maxSize = TweenTimelineDefine.windowMaxSize;
            window.minSize = TweenTimelineDefine.windowMaxSize;
        }
        #region LifeTime
        public void OnEnable()
        {
            if (_selectTweenAction == null)
            {
                _selectTweenAction = new TweenActionEffect();
            }
            this.maxSize = TweenTimelineDefine.windowMaxSize;
            this.minSize = TweenTimelineDefine.windowMaxSize;
            TweenTimeLineDataModel.RefreshViewAction = this.RefreshBtns;

            // AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
            // AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;

            LoadAssets();
        }


        public void OnDestroy()
        {
            TweenTimeLineDataModel.RefreshViewAction = null;

            TweenTimelineManager.ResetDefaultAllClip();
            TweenTimelineManager.TryRemoveTweenManager();
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
                UpdateTargetComponents();
            };
            UpdateTargetComponents();
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
                CancelTween();
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

            var trackInfoDict = new Dictionary<string, TrackInfoContext>();
            var trackRoot = BindUtility.GetAttachRoot(selectTweenAction.target.transform);
            Assert.IsNotNull(trackRoot, $"{selectTweenAction.target} must be child of panel or composite");

            foreach (var animUnit in selectTweenAction.animationSteps)
            {
                animUnit.GetAnimUnitClipInfo(selectTweenAction, _easingTokenPresetLibrary,
                out var clipInfo, out var component);
                Type componentType = animUnit.GetComponentType();
                string animUnitTweenMethod = animUnit.tweenMethod.Replace("ControlBehaviour", "");
                var trackName = $"Cr7Sund.{componentType.Name}Tween.{animUnitTweenMethod}ControlTrack";
                Assembly assembly = TweenActionStep.GetTweenTrackAssembly(trackName);
                Type trackType = assembly.GetType(trackName);
                var assetName = $"Cr7Sund.{componentType.Name}Tween.{animUnitTweenMethod}ControlAsset";
                var trackAssetType = assembly.GetType(assetName);
                if (!trackInfoDict.TryGetValue(animUnit.tweenMethod, out var trackInfo))
                {
                    trackInfo = new TrackInfoContext();
                    trackInfo.component = component;
                    trackInfo.trackType = trackType;
                    trackInfo.clipInfos = new List<ClipInfoContext>();
                    trackInfoDict.Add(animUnit.tweenMethod, trackInfo);
                }
                clipInfo.trackAssetType = trackAssetType;

                trackInfo.clipInfos.Add(clipInfo);

                // var behaviourName = $"Cr7Sund.TweenTimeLine.{animUnit.tweenMethod}ControlBehaviour";

                var rooTabView = rootVisualElement.Q<TabView>("rooTabView");

                bool createNewTrack = false;
                bool alwaysCreateNewTrack = TweenTimelinePreferencesProvider.GetBool(ActionEditorSettings.AlwaysCreateTrack);
                if (!alwaysCreateNewTrack &&
                    TweenTimelineManager.FindExistTrackAsset(component, trackType) != null)
                {
                    createNewTrack = EditorUtility.DisplayDialog($"Create New Track",
                    $"A same track of{component.name} {trackType.Name}  is already exists. \n (Tip: You can toggle the behavior to allowed always create a new track)", "Create New Track", "Attend on the existed");
                }

                TweenTimelineManager.AddTrack(
                    trackRoot,
                    trackInfo,
                    rooTabView.selectedTabIndex != 1,
                    createNewTrack
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
                PlayTimeline();
            });

            recordBtn.RegisterCallback<ClickEvent>(_ =>
            {
                AssertTimelineOpen();
                TweenTimelineManager.ToggleRecordAllSelectClip();
            });
            this.titleContent = new GUIContent("Animation Editor");

            RefreshBtns();
        }

        private void PlayTimeline()
        {
            AssertTimelineOpen();
            var previousState = TweenTimeLineDataModel.StateInfo.BehaviourState;

            // https://discussions.unity.com/t/animation-events-on-last-frame-arent-fired-in-timeline-when-its-the-last-frame-of-the-timeline/768636/9
            // Timeline is inclusive of the first frame of clips and exclusive of the last, meaning that an active clip that starts exactly at 0 and ends at exactly frame N, will be disabled at frame N.
            if (Application.isPlaying)
            {
                TweenTimelineManager.ToggleAllPLayClips();
            }
            else
            {
                if (!string.IsNullOrEmpty(_curRestID))
                {
                    EditorTweenCenter.UnRegisterEditorTimer(_curRestID);
                    TweenTimelineManager.ToggleAllPLayClips();
                }

                float delayResetTime = TweenTimelinePreferencesProvider.GetFloat(ActionEditorSettings.DelayResetTime);
                TweenTimelineManager.ToggleAllPLayClips();

                _curRestID = EditorTweenCenter.RegisterDelayCallback(this,
                  (float)TimelineWindowExposer.GetPlayDuration() + delayResetTime,
                 (t, elapsedTime) =>
                 {
                     TweenTimelineManager.ToggleAllPLayClips();
                     _curRestID = string.Empty;
                 });

                // TweenTimelineManager.Play();
            }

            // if (previousState == TweenTimeLineDataModel.StateInfo.BehaviourState)
            // {
            //     Debug.Log($"try to change into the same state {previousState}, please check the timeline is valid");
            // }
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

        private void RefreshBtnImages()
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

            RefreshBtnImages();

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

            CreateSettingParisField(container);
            RefreshEasePairs(container);

            // Update Duration Field
            var durationField = container.Q<EnumField>("CustomDurationField");
            durationField.Init(DurationToken.ExtraLong1);
            durationField.RegisterValueChangedCallback(evt =>
            {
                selectTweenAction.durationToken = (DurationToken)evt.newValue;
            });

            // Update Ease Field
            EnumField easeField = container.Q<EnumField>("CustomEasingField");
            easeField.Init(MaterialEasingToken.Standard);
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
                UpdateTargetComponents();
            });

        }

        private void UpdateTargetComponents()
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
            var animSettingType = _selectTweenAction.timeEasePairs;
            animationSettingField.Init(animSettingType);
            animationSettingField.RegisterValueChangedCallback(evt =>
            {
                _selectTweenAction.timeEasePairs = (TimeEasePairs)evt.newValue;

                var easeField = container.Q<EnumField>("CustomEasingField");
                var durationField = container.Q<EnumField>("CustomDurationField");
                RefreshEasePairs(container);

                easeField.value = _selectTweenAction.easeToken;
                durationField.value = _selectTweenAction.durationToken;
            });
        }

        private void RefreshEasePairs(VisualElement container)
        {
            var selectedSettingType = _selectTweenAction.timeEasePairs;
            var easeField = container.Q<EnumField>("CustomEasingField");
            var durationField = container.Q<EnumField>("CustomDurationField");
            ToggleTweenAnimParis(durationField, easeField, selectedSettingType);

            if (selectedSettingType == TimeEasePairs.Custom)
            {
                return;
            }
            var settings = _animTokenPresets.GetSettings(selectedSettingType);

            _selectTweenAction.durationToken = settings.duration;
            _selectTweenAction.easeToken = settings.easing;
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
            endPosField.Q<Label>().text = animActionUnit.tweenOperationType == TweenActionStep.TweenOperationType.Additive
            ? "Delta" : "EndValue";
            var startPosField = AniActionEditToolHelper.CreateValueField("StartValue", componentValueType, animActionUnit.StartPos, (newValue) =>
            {
                animActionUnit.StartPos = newValue;
            });
            startPosField.name = "StartPosField";
            startPosField.style.display = animActionUnit.tweenOperationType != TweenActionStep.TweenOperationType.Default
            ? DisplayStyle.None : DisplayStyle.Flex;
            var paramsPart = animUnit.Q<VisualElement>("paramsPart");

            paramsPart.Add(endPosField);
            paramsPart.Add(startPosField);

            // Set the toggle
            var toggle = animUnit.Q<EnumField>("relative");
            toggle.Init(animActionUnit.tweenOperationType);
            // toggle.style.display = animActionUnit.isRelative ? DisplayStyle.Flex : DisplayStyle.None;
            toggle.RegisterValueChangedCallback(evt =>
            {
                animActionUnit.tweenOperationType = (TweenActionStep.TweenOperationType)evt.newValue;
                startPosField.style.display = (animActionUnit.tweenOperationType != TweenActionStep.TweenOperationType.Default)
                ? DisplayStyle.None : DisplayStyle.Flex;
                endPosField.Q<Label>().text = animActionUnit.tweenOperationType == TweenActionStep.TweenOperationType.Additive
                 ? "Delta" : "EndValue";
            });

            // Add the animUnit to the container
            container.Add(animUnit);
        }

        private void UpdateActionCollectionContainer(Dictionary<string, List<TweenActionEffect>> actionCategoryList, TweenCollection animCollections, VisualElement grid)
        {
            grid.Clear();


            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(TweenTimelineDefine.gridItemVisualTreeAssetGUID));
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(TweenTimelineDefine.gridItemItemStyleGUID));

            foreach (var kvp in actionCategoryList)
            {
                if (kvp.Value.Count > 0)
                {
                    CreateCategoryTitle(kvp.Key, grid);
                }
                foreach (var item in kvp.Value)
                {
                    CreateGridItem(grid, item,
                    visualTree, styleSheet);
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

        private void CreateGridItem(VisualElement grid, TweenActionEffect animAction,
         VisualTreeAsset visualTree, StyleSheet styleSheet)
        {
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
            bool showGUI = TweenTimelinePreferencesProvider.GetBool(ActionEditorSettings.EnableGifPreview);

            if (showGUI &&
                 extension == ".gif"
                 && File.Exists(animAction.image))
            {
                img.Clear();
                GifUIControl gif = new GifUIControl(animAction.image);
                gif.name = "gif";
                img.Add(gif);
            }
            else
            {
                img.Clear();
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
                    if (_selectGridItem != null)
                    {
                        _selectGridItem.EnableInClassList("Select", false);
                    }
                    _selectGridItem = item;
                    _selectGridItem.EnableInClassList("Select", true);
                    OnAnimationEffectSelect(animAction);
                }
            });
        }

        private void LoadAssets()
        {
            LoadBtnsAssets();
            _easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(TweenTimelineDefine.easingTokenPresetsPath);
            _tweenActionContainer = AssetDatabase.LoadAssetAtPath<TweenActionLibrary>(TweenTimelineDefine.tweenLibraryPath);
            _animTokenPresets = AssetDatabase.LoadAssetAtPath<AnimTokenPresets>(TweenTimelineDefine.animTokenPresetsPath);
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
            if (animAction == null)
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
            CancelTween();

            _curSequence = Sequence.Create();
            var onResetActions = new List<Action>();
            foreach (var animUnit in animAction.animationSteps)
            {
                var tween = animUnit.GenerateTween(animAction, _easingTokenPresetLibrary, out var onResetAction);

                if (animUnit.startTimeOffset < 0)
                {
                    _curSequence.Group(Sequence.Create().
                    ChainDelay(AniActionEditToolHelper.ConvertDuration(animUnit.startTimeOffset)).Chain(tween));
                }
                else
                {
                    _curSequence.Group(tween);
                }
                onResetActions.Add(onResetAction);
            }

            float delayResetTime = TweenTimelinePreferencesProvider.GetFloat(ActionEditorSettings.DelayResetTime);
            _curSequence.ChainDelay(delayResetTime).OnComplete(() =>
            {
                foreach (var item in onResetActions)
                {
                    item?.Invoke();
                }
            });

            _updateSequenceID = EditorTweenCenter.RegisterSequence(_curSequence, animAction.target,
            animAction.ConvertDuration() + delayResetTime);
        }
        private void CancelTween()
        {
            if (_curSequence.isAlive)
            {
                _curSequence.Complete();
                _curSequence.Stop();
            }
            EditorTweenCenter.UnRegisterEditorTimer(_updateSequenceID);
        }
        #endregion
    }
}
