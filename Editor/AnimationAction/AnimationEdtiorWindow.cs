using System;
using System.Collections.Generic;
using Cr7Sund.Timeline.Extension;
using PrimeTween;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine.Editor
{
    public class AnimationEditorWindow : EditorWindow
    {
        private AnimationEffect _selectAnimationAction = new();
        private Sequence _sequence;
        private AnimTokenPresets animTokenPresets;
        private AnimationContainer animationContainer;
        private EasingTokenPresets easingTokenPreset;
        private const string animContainerGuid = "d53959b9eaf99df44bac415705d1187e";
        public const string easingTokenPresetGuid = "c44b58ffe59d7424aa756ffb11fc39aa";
        private const string animTokenPresetsGuid = "162544b970c4f84498947d47bb4d06e3";
        private const string windowVisualTreeAssetGUID = "5d602cb17b57b46439b7bd1be265e07a";
        private const string windowStyleGUID = "fb42482ea3524b845aae708f4c63ffc7";
        private const string unitItemVisualTreeAssetGUID = "c0db593d4dafda847be427f46256930f";
        private const string unitItemStyleGUID = "64e80b7565634e3469b5214321257731";
        private const string gridVisualTreeAssetGUID = "f0b1d90a7f1c4214be7f11bb316bf762";
        private const string gridItemStyleGUID = "bcf8978c76b6418ca38f4a57fe81056c";
        private const string gridItemVisualTreeAssetGUID = "815c36bee7924d8dae6f349c09ca0b26";
        private const string gridItemItemStyleGUID = "409d5890bb0d4c05acd7025f26b8d165";
        public static Background recordOnBackground;
        public static Background recordOffBackground;
        public static Background plaBackground;
        public static Background stopBackground;
        [MenuItem("Tools/Animation Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<AnimationEditorWindow>();
            window.maxSize = new Vector2(560f, 400f); // Limit the window height to 400f
            window.minSize = new Vector2(560f, 400f); // Limit the window height to 400f
        }

        #region LifeTime
        public void OnEnable()
        {
            this.maxSize = new Vector2(560f, 400f); // Limit the window height to 400f
            this.minSize = new Vector2(560f, 400f); // Limit the window height to 400f

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
            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(windowVisualTreeAssetGUID));
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(windowStyleGUID));
            rootVisualElement.styleSheets.Add(styleSheet);
            rootVisualElement.Add(visualTreeAsset.Instantiate());

            UpdateConfigUI();
            CreateAnimUnits();
            BindActionBtns();
            CreateTab();
        }

        #region UI
        private void CreateTab()
        {
            var rooTabView = rootVisualElement.Q<TabView>("rooTabView");
            foreach (var container in animationContainer.animationContainers)
            {
                var tab = new Tab(container.category);
                rooTabView.Add(tab);

                var actionCategoryList = UpdateActionCollectionContainer(container, tab);

                foreach (var categoryFoldout in actionCategoryList)
                {
                    tab.Add(categoryFoldout);
                }
            }
        }

        private void BindActionBtns()
        {
            var container = rootVisualElement.Q<VisualElement>("ConfigContainer");
            Button previewTweenBtn = container.Query<Button>("previewTweenBtn");
            Button addTrackBtn = container.Query<Button>("addTrackBtn");

            previewTweenBtn.clicked += () =>
            {
                CheckValidTarget();
                PlayTween(_selectAnimationAction);
            };

            addTrackBtn.clicked += () =>
            {
                AssertTimelineOpen();
                CheckValidTarget();
                AddTracks();
            };
            DrawToolBtns();
        }

        private void AddTracks()
        {
            foreach (var animUnit in _selectAnimationAction.animationSteps)
            {
                TrackInfo trackInfo;
                Component component;
                animUnit.GetAnimUnitTrackInfo(_selectAnimationAction, easingTokenPreset, out trackInfo, out component);
                Type componentType = animUnit.GetComponentType();
                component = _selectAnimationAction.target.GetComponent(componentType);
                // var behaviourName = $"Cr7Sund.TweenTimeLine.{animUnit.tweenMethod}ControlBehaviour";
                var trackName = $"Cr7Sund.TweenTimeLine.{animUnit.tweenMethod}ControlTrack";
                var assetName = $"Cr7Sund.TweenTimeLine.{animUnit.tweenMethod}ControlAsset";
                TabView rooTabView = rootVisualElement.Q<TabView>("rooTabView");
                TweenTimelineManager.AddTrack(component,
                    typeof(GraphicColorAControlBehaviour).Assembly.GetType(trackName),
                    typeof(GraphicColorAControlBehaviour).Assembly.GetType(assetName),
                    trackInfo,
                   rooTabView.selectedTabIndex == 0
                 );
            }

            TimelineEditor.Refresh(RefreshReason.ContentsAddedOrRemoved);
            // var playableDirector = GameObject.FindFirstObjectByType<PlayableDirector>();
            // EditorUtility.SetDirty(playableDirector.playableAsset);
            // AssetDatabase.SaveAssetIfDirty(playableDirector.playableAsset);
            // AssetDatabase.Refresh();
            // TimelineWindowExposer.RepaintTimelineWindow();
        }

        private void CheckValidTarget()
        {
            if (_selectAnimationAction == null ||
                _selectAnimationAction.target == null)
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

            playBtn.iconImage = TweenTimeLineDataModel.StateInfo.IsPlaying ? plaBackground : stopBackground;
            recordBtn.iconImage = TweenTimeLineDataModel.StateInfo.IsRecording ? recordOnBackground : recordOffBackground;
        }

        public void LoadBtnsAssets()
        {
            if (recordOnBackground != null)
            {
                return;
            }
            recordOnBackground = new Background()
            {
                texture = (Texture2D)EditorGUIUtility.IconContent("d_Record On@2x").image
            };
            recordOffBackground = new Background()
            {
                texture = (Texture2D)EditorGUIUtility.IconContent("d_Record Off@2x").image
            };
            plaBackground = new Background()
            {
                texture = (Texture2D)EditorGUIUtility.IconContent("PauseButton On@2x").image
            };
            stopBackground = new Background()
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

        private void UpdateConfigUI()
        {
            var container = rootVisualElement.Q<VisualElement>("ConfigContainer");

            var animSettingType = animTokenPresets.GetAnimationSettingType(_selectAnimationAction.durationToken, _selectAnimationAction.easeToken);
            CreateSettingParisField(container);

            // Update Duration Field
            var durationField = container.Q<EnumField>("CustomDurationField");
            durationField.value = _selectAnimationAction.durationToken;
            durationField.RegisterValueChangedCallback(evt =>
            {
                _selectAnimationAction.durationToken = (DurationToken)evt.newValue;
            });

            // Update Ease Field
            var easeField = container.Q<EnumField>("CustomEasingField");

            easeField.value = _selectAnimationAction.easeToken;
            easeField.RegisterValueChangedCallback(evt =>
            {
                _selectAnimationAction.easeToken = (MaterialEasingToken)evt.newValue;
            });

            // Update Target Field
            var componentField = container.Q<ObjectField>("TargetField");

            componentField.RegisterValueChangedCallback(evt =>
            {
                _selectAnimationAction.target = (GameObject)evt.newValue;
                UpdateSelectCollectionContainer();
            });

            ToggleTweenAnimParis(durationField, easeField, animSettingType);
        }

        private void UpdateSelectCollectionContainer()
        {
            var tabView = rootVisualElement.Q<TabView>("rooTabView");
            var activeTab = tabView.activeTab;
            activeTab.Clear();
            UpdateActionCollectionContainer(animationContainer.animationContainers[activeTab.tabIndex], activeTab);
        }

        private void CreateAnimUnits()
        {
            VisualElement animUnitContainer = rootVisualElement.Q("BottomParamsPart");
            animUnitContainer.Clear();
            foreach (var item in _selectAnimationAction.animationSteps)
            {
                CreateAnimUnit(item, _selectAnimationAction.animationSteps.Count > 1);
            }
        }

        private void CreateSettingParisField(VisualElement container)
        {
            var animationSettingField = container.Q<EnumField>("AnimTokenPreset");
            var animSettingType = animTokenPresets.GetAnimationSettingType(_selectAnimationAction.durationToken, _selectAnimationAction.easeToken);
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
                _selectAnimationAction.durationToken = settings.duration;
                _selectAnimationAction.easeToken = settings.easing;

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

        private void CreateAnimUnit(AnimationStep animActionUnit, bool showLabel)
        {
            var container = rootVisualElement.Q<VisualElement>("BottomParamsPart");

            // Instantiate the UXML prefab
            var animUnitPrefab = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(unitItemVisualTreeAssetGUID));
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(unitItemStyleGUID));
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
            var endPosField = AniActionEditToolHelper.CreateValueField("StartValue", componentValueType, animActionUnit.EndPos, (newValue) =>
            {
                animActionUnit.EndPos = newValue;
            });
            endPosField.name = "EndPosField";
            var startPosField = AniActionEditToolHelper.CreateValueField("EndValue", componentValueType, animActionUnit.StartPos, (newValue) =>
            {
                animActionUnit.StartPos = newValue;
            });
            startPosField.name = "StartPosField";
            var paramsPart = animUnit.Q<VisualElement>("paramsPart");
            paramsPart.Add(startPosField);
            paramsPart.Add(endPosField);

            // Set the toggle
            var toggle = animUnit.Q<Toggle>("useCurPos");
            toggle.value = animActionUnit.useCurPos;
            toggle.style.display = animActionUnit.useCurPos ? DisplayStyle.Flex : DisplayStyle.None;
            toggle.RegisterValueChangedCallback(evt =>
            {
                animActionUnit.useCurPos = evt.newValue;
            });

            // Add the animUnit to the container
            container.Add(animUnit);
        }

        private List<VisualElement> UpdateActionCollectionContainer(AnimationCollection animContainer, VisualElement container)
        {
            var actionCategoryList = animContainer.GetAnimActionCategory();

            var foldouts = new List<VisualElement>();
            container.Clear();
            var grid = CreateGrid(container);
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

            return foldouts;
        }

        private VisualElement CreateGrid(VisualElement container)
        {
            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(gridVisualTreeAssetGUID));
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(gridItemStyleGUID));
            rootVisualElement.styleSheets.Add(styleSheet);
            var gridContainer = visualTreeAsset.Instantiate();
            container.Add(gridContainer);
            return gridContainer.Q<VisualElement>("grid");
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

        private void CreateGridItem(VisualElement grid, AnimationEffect animAction)
        {
            // Load the UXML file
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(gridItemVisualTreeAssetGUID));
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(gridItemItemStyleGUID));

            // Clone the UXML and apply styles
            var item = visualTree.CloneTree().Q<VisualElement>("gridItem");
            item.styleSheets.Add(styleSheet);

            // Set the label text
            var label = item.Q<Label>("label");
            label.text = animAction.label;

            // Set the image icon
            var img = item.Q<Image>("icon");
            img.image = AniActionEditToolHelper.LoadImageFromPath(animAction.image);

            // Add the item to the grid
            grid.Add(item);

            // Register click event
            item.RegisterCallback<ClickEvent>(ev => OnAnimationEffectSelect(animAction));
        }


        private void LoadAssets()
        {
            LoadBtnsAssets();
            easingTokenPreset = AssetDatabase.LoadAssetAtPath<EasingTokenPresets>(AssetDatabase.GUIDToAssetPath(easingTokenPresetGuid));
            animationContainer = AssetDatabase.LoadAssetAtPath<AnimationContainer>(AssetDatabase.GUIDToAssetPath(animContainerGuid));
            animTokenPresets = AssetDatabase.LoadAssetAtPath<AnimTokenPresets>(AssetDatabase.GUIDToAssetPath(animTokenPresetsGuid));
        }
        #endregion

        #region Actions
        private void OnAnimationEffectSelect(AnimationEffect animAction)
        {
            if (_selectAnimationAction.target == null)
            {
                Debug.LogError("Please select a game object first");
            }
            _selectAnimationAction.CopyFrom(animAction);
            UpdateConfigUI();
            CreateAnimUnits();
        }

        private void OpenMenu()
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Play Tween"), false, () => PlayTween(_selectAnimationAction));
            menu.ShowAsContext();
        }

        private void PlayTween(AnimationEffect animAction)
        {
            if (_sequence.isAlive)
            {
                _sequence.Complete();
                _sequence.Stop();
            }

            _sequence = Sequence.Create();
            var onResetActions = new List<Action>();
            foreach (var animUnit in animAction.animationSteps)
            {
                var tween = animUnit.GenerateTween(animAction, easingTokenPreset, out var onResetAction);

                if (animUnit.startTimeOffset < 0)
                {
                    _sequence.Group(Sequence.Create().ChainDelay(AniActionEditToolHelper.ConvertDuration(animUnit.startTimeOffset)).Chain(tween));
                }
                else
                {
                    _sequence.Group(tween);
                }
                onResetActions.Add(onResetAction);
            }

            _sequence.ChainDelay(0.8f).OnComplete(() =>
            {
                foreach (var item in onResetActions)
                {
                    item?.Invoke();
                }
            });

            EditorTweenCenter.RegisterSequence(_sequence, animAction.target, animAction.ConvertDuration() + 1f);
        }
        #endregion
    }
}
