using System;
using System.Collections.Generic;
using System.Reflection;
using Cr7Sund.Timeline.Extension;
using PrimeTween;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;
using Assert = UnityEngine.Assertions.Assert;

namespace Cr7Sund.TweenTimeLine
{

    public static class TweenTimelineManager
    {
        public static bool isPlay;
        public static double timeLineTime;
        private static bool isInit = false;

        public static bool IsInit
        {
            get
            {
                return isInit;
            }
            set => isInit = value;
        }

        public static bool InitTimeline()
        {
            if (Application.isPlaying)
            {
                return true;
            }

            InitPreTween();

            if (!IsInit && TimelineWindowExposer.IsValidTimelineWindow())
            {
                bool hasTimelineWindow = TryRegisterTimelineEvents();

                if (hasTimelineWindow)
                {
                    Refresh();

                    IsInit = true;
                    return true;
                }
            }

            return false;
        }

        public static void DestroyTimeline()
        {
            IsInit = false;
            isPlay = false;
        }
        public static void LogProfile(string message)
        {
            // Debug.Log("<color=green>" + message + "</color>");
        }

        [InitializeOnLoadMethod]
        static void OnInitTweenLineManager()
        {
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
            EditorApplication.playModeStateChanged -= TweenTimelineManager.OnPlayModeChanged;
            EditorApplication.playModeStateChanged += TweenTimelineManager.OnPlayModeChanged;
        }

        #region Events
        private static bool TryRegisterTimelineEvents()
        {
            return TimelineWindowExposer.Init(OnPlayStateChange, OnRebuildGraphChange, OnTimeChange, onTimelineWindowGUIStart);
        }

        private static void onTimelineWindowGUIStart()
        {
            // in case when change selection since we need to select timeline first
            if (TweenTimeLineDataModel.SelectTimelineClip != null
                && TimelineWindowExposer.IsValidTimelineWindow())
            {
                TimelineWindowExposer.SelectTimelineClip(TweenTimeLineDataModel.SelectTimelineClip);
                TweenTimeLineDataModel.SelectTimelineClip = null;
            }
        }

        private static void OnTimeChange()
        {
            double time = TimelineWindowExposer.GetSequenceTime();
            UpdateTimeCache();

            if (!isPlay && timeLineTime != time)
            {
                PreviewAllClip();
            }
            timeLineTime = time;
        }

        private static void OnRebuildGraphChange()
        {
            if (TimelineWindowExposer.IsFocusTimeLineWindow())
            {
                Refresh();
            }
        }

        private static void OnPlayStateChange(bool isPlay)
        {
            SetPlay(isPlay);
            // Debug.Log("IsPlay " + isPlay);

            if (isPlay)
            {
                TweenTimelineManager.PlayAllClip();
            }
            else
            {
                ResetDefaultAllClipWhenStop();
                IsInit = false;
            }
        }

        private static void SetPlay(bool isPlay)
        {
            TweenTimelineManager.isPlay = isPlay;
        }

        private static void OnAfterAssemblyReload()
        {
            if (GetActionEditorWindow() != null)
            {
                InitTimeline();
            }
        }

        private static void OnBeforeAssemblyReload()
        {
            if (Application.isPlaying)
            {
                return;
            }
            if (IsInit)
            {
                ResetDefaultAllClip();
            }
        }

        public static void OnPlayModeChanged(PlayModeStateChange change)
        {
            // incase of the static don't change when domain reload disable
            IsInit = false;
            if (change == PlayModeStateChange.ExitingEditMode)
            {
                if (GetActionEditorWindow() != null)
                {
                    EditorWindow.GetWindow<TweenActionEditorWindow>().Close();
                }
                TryRemoveTweenManager();
            }
            else if (change == PlayModeStateChange.EnteredEditMode)
            {
                InitPreTween();
            }
            else if (change == PlayModeStateChange.EnteredPlayMode)
            {
                Application.targetFrameRate = 60;
                if (GetActionEditorWindow() != null)
                {
                    InitTimeline();
                }
                // Application.targetFrameRate = (int)TimelineWindowExposer.GetTimelineFrameRate();
            }
        }
        #endregion

        #region BindDatas
        public static bool InitPlayableBindings()
        {
            TweenTimeLineDataModel.TrackObjectDict.Clear();

            var playableDirectors = TimelineWindowExposer.GetPlayableDirectors();
            foreach (var playableDirector in playableDirectors)
            {
                InitDirector(playableDirector);
            }

            return playableDirectors.Count > 0;

            void InitDirector(PlayableDirector playableDirector)
            {
                if (playableDirector == null)
                {
                    return;
                }
                PlayableAsset playableAsset = playableDirector.playableAsset;
                var outputs = playableAsset.outputs;
                foreach (PlayableBinding output in outputs)
                {
                    var trackAsset = output.sourceObject as TrackAsset;
                    if (trackAsset == null) continue;
                    var binding = playableDirector.GetGenericBinding(trackAsset);
                    if (binding == null)
                    {
                        // if (trackAsset is IBaseTrack)
                        Debug.LogWarning($"{trackAsset} don't bind target");
                        continue;
                    }

                    if (!trackAsset.hasClips)
                    {
                        if (trackAsset is AnimationTrack animationTrack
                        && animationTrack.infiniteClip != null)
                        {

                        }
                        else
                        {
                            Debug.LogWarning($"{trackAsset} don't have clips");
                            continue;
                        }

                    }

                    TweenTimeLineDataModel.TrackObjectDict.Add(trackAsset, binding);
                }
            }
        }

        private static void RefreshTimeline()
        {
            foreach (var item in TweenTimeLineDataModel.ClipInfoDicts)
            {
                item.Value.Dispose();
            }

            var validClipStateSet = new List<Tuple<IUniqueBehaviour, ClipBehaviourState>>();
            // TweenTimeLineDataModel.ClipStateDict.Clear();
            TweenTimeLineDataModel.PlayableAssetTrackDict.Clear();
            TweenTimeLineDataModel.PlayBehaviourTrackDict.Clear();
            TweenTimeLineDataModel.TrackBehaviourDict.Clear();
            TweenTimeLineDataModel.ClipInfoDicts.Clear();
            TweenTimeLineDataModel.ClipAssetBehaviourDict.Clear();
            // TweenTimeLineDataModel.NotificationReceiverDict.Clear();
            TweenTimeLineDataModel.groupTracks.Clear();

            TimelineWindowExposer.IteratePlayableAssets((playableAsset, trackAsset) =>
            {
                TweenTimeLineDataModel.PlayableAssetTrackDict.Add(playableAsset, trackAsset);
            });

            TimelineWindowExposer.IterateTrackAssets((trackAsset) =>
            {
                if (typeof(GroupTrack).IsInstanceOfType(trackAsset))
                {
                    TweenTimeLineDataModel.groupTracks.Add(trackAsset as GroupTrack);
                }
            });

            TimelineWindowExposer.IterateClips((clip, trackAsset) =>
            {
                if (trackAsset is not IBaseTrack)
                {
                    if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(trackAsset))
                    {
                        return;
                    }
                    var binComponent = TweenTimeLineDataModel.TrackObjectDict[trackAsset] as Component;
                    var bindTrans = binComponent.transform;

                    Assert.IsNotNull(bindTrans);

                    if (trackAsset is AudioTrack customAudioTrack)
                    {
                        trackAsset.name = binComponent.name;
                    }
                }
            });


            TimelineWindowExposer.IterateClips((value, clip, trackAsset) =>
            {
                if (trackAsset is not IBaseTrack)
                {
                    return;
                }

                var behaviour = value as IUniqueBehaviour;
                if (behaviour == null)
                    return;
                if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(trackAsset))
                {
                    // just skip if the bind component is null
                    return;
                }
                BindTrackAsset(trackAsset, behaviour);
                BindClipAsset(clip, validClipStateSet, behaviour, trackAsset);
            });

            TweenTimeLineDataModel.ClipStateDict.Clear();
            foreach (var item in validClipStateSet)
            {
                TweenTimeLineDataModel.ClipStateDict.Add(item.Item1, item.Item2);
            }

            foreach (var item in TweenTimeLineDataModel.TrackObjectDict)
            {
                var binComponent = item.Value as Component;
                TrackAsset trackAsset = item.Key;
                if (trackAsset is not IBaseTrack)
                {
                    continue;
                }
                if (!TweenTimeLineDataModel.TrackBehaviourDict.TryGetValue(trackAsset, out var behaviourList)) return;
                {
                    foreach (var behaviour in behaviourList)
                    {
                        behaviour.BindTarget = GetTrackName(binComponent.transform);
                        //  binComponent.gameObject.name;
                        behaviour.BindType = binComponent.GetType().FullName;
                    }
                }

            }

            RecreateTween();
        }

        private static void BindClipAsset(TimelineClip clip, List<Tuple<IUniqueBehaviour, ClipBehaviourState>> validClipStateSet, IUniqueBehaviour behaviour, TrackAsset trackAsset)
        {
            TweenTimeLineDataModel.ClipAssetBehaviourDict.Add(clip.asset, behaviour);

            var clipInfo = new ClipInfo()
            {
                delayTime = clip.start,
                duration = clip.duration,
            };
            clipInfo.valueMakers = new();
            foreach (IMarker marker in trackAsset.GetMarkers())
            {
                if (marker is IValueMaker valueMaker
                    && marker.time >= clip.start && marker.time <= clip.end)
                {
                    clipInfo.valueMakers.Add(new MarkInfo(valueMaker));
                }
            }

            TweenTimeLineDataModel.ClipInfoDicts.Add(behaviour, clipInfo);

            // state should keep when refresh
            if (!TweenTimeLineDataModel.ClipStateDict.TryGetValue(behaviour, out var clipBehaviourState))
            {
                clipBehaviourState = new ClipBehaviourState
                {
                    PreViewAction = (uniqueBehaviour, targetState) =>
                    {
                        if (targetState == ClipBehaviourStateEnum.Preview)
                        {
                            ActionCenters.StartPreview(uniqueBehaviour);
                        }
                        else
                        {
                            ActionCenters.Reset(uniqueBehaviour);
                        }
                    },
                    PlayAction = (uniqueBehaviour, targetState) =>
                    {
                        var stateInfo = TweenTimeLineDataModel.ClipStateDict[uniqueBehaviour];
                        if (targetState == ClipBehaviourStateEnum.Playing)
                        {
                            ActionCenters.StartPlay(uniqueBehaviour);
                        }
                        else
                        {
                            ActionCenters.StopPlay(uniqueBehaviour);
                            ActionCenters.Reset(uniqueBehaviour);
                        }
                    },
                    RecordAction = (uniqueBehaviour, targetState) =>
                    {
                        ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[uniqueBehaviour];
                        if (targetState == ClipBehaviourStateEnum.Recording)
                        {
                            ActionCenters.MoveToRecordPos(uniqueBehaviour, stateInfo.IsRecordStart);
                        }
                        else
                        {
                            ActionCenters.EndRecord(uniqueBehaviour, stateInfo.IsRecordStart);
                            ActionCenters.Reset(uniqueBehaviour);
                        }
                    }
                };

                TweenTimeLineDataModel.ClipStateDict.Add(behaviour, clipBehaviourState);
            }

            ResetInitPos(behaviour);
            ResetMarkFieldNames(behaviour);

            var validMarkers = new Dictionary<int, object>();
            foreach (var valueMarker in clipInfo.valueMakers)
            {
                if (clipBehaviourState.markerInitPosDict.TryGetValue(valueMarker.InstanceID, out var v))
                {
                    validMarkers.Add(valueMarker.InstanceID, v);
                }
            }
            clipBehaviourState.markerInitPosDict = validMarkers;

            validClipStateSet.Add(new(behaviour, clipBehaviourState));
        }

        public static void ResetInitPos(IUniqueBehaviour behaviour)
        {
            TrackAsset trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
            ClipBehaviourState clipBehaviourState = TweenTimeLineDataModel.ClipStateDict[behaviour];

            if (!clipBehaviourState.IsPreview
                && !clipBehaviourState.IsPlaying
                && !clipBehaviourState.IsRecording)
            {
                var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
                clipBehaviourState.initPos = behaviour.Get(target);
            }
        }

        private static void ResetMarkFieldNames(IUniqueBehaviour behaviour)
        {
            TrackAsset trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
            ClipBehaviourState clipBehaviourState = TweenTimeLineDataModel.ClipStateDict[behaviour];
            var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];

            if (!clipBehaviourState.IsPreview
                && !clipBehaviourState.IsPlaying
                && !clipBehaviourState.IsRecording)
            {
                clipBehaviourState.markerInitPosDict = new();
                for (int i = 0; i < clipInfo.valueMakers.Count; i++)
                {
                    MarkInfo valueMarker = clipInfo.valueMakers[i];
                    clipBehaviourState.markerInitPosDict[valueMarker.InstanceID] = valueMarker.Get(target);
                }
            }
        }

        private static void BindTrackAsset(TrackAsset asset, IUniqueBehaviour behaviour)
        {
            TweenTimeLineDataModel.PlayBehaviourTrackDict.Add(behaviour, asset);
            if (!TweenTimeLineDataModel.TrackBehaviourDict.TryGetValue(asset, out var clipBehaviors))
            {
                clipBehaviors = new List<IUniqueBehaviour>();
                TweenTimeLineDataModel.TrackBehaviourDict.Add(asset, clipBehaviors);
            }
            clipBehaviors.Add(behaviour);

            // TweenTimeLineDataModel.NotificationReceiverDict.Add(behaviour, new TweenTimelineReceiver());
        }

        private static void RecreateTween()
        {
            ReusablePrimeTweens();
            foreach (var item in TweenTimeLineDataModel.ClipInfoDicts)
            {
                item.Value.CreateTween(item.Key);
            }
        }

        public static void InitPreTween()
        {
            try
            {
                PrimeTweenManagerExposer.Init();
            }
            catch (System.Exception e)
            {
                TryRemoveTweenManager();
                throw new Exception($"InitPreTween Fail! Try to reimport Core again  \nOutput: {e}");
            }
        }

        private static void UpdateTimeCache()
        {
            double time = TimelineWindowExposer.GetSequenceTime();

            if (TweenTimeLineDataModel.ClipAssetBehaviourDict.Count <= 0)
            {
                return;
            }
            TimelineWindowExposer.IterateClips((clip, trackAsset) =>
            {
                if (trackAsset is not IBaseTrack)
                {
                    return;
                }
                if (!TweenTimeLineDataModel.ClipAssetBehaviourDict.TryGetValue(clip.asset, out var behaviour)) return;

                if (clip.end >= time && clip.start <= time)
                {
                    TweenTimeLineDataModel.ClipStateDict[behaviour].IsSelect = true;
                }
                else
                {
                    TweenTimeLineDataModel.ClipStateDict[behaviour].IsSelect = false;
                }
            });
        }


        public static void TryRemoveTweenManager()
        {
            PrimeTweenManagerExposer.Destroy();
        }

        private static void Refresh()
        {
            if (InitPlayableBindings())
            {
                RefreshTimeline();
                UpdateTimeCache();
            }
        }
        #endregion


        #region Actions
        public static void ResetDefaultAllClip()
        {
            ChangeTweenManagerState(ClipBehaviourStateEnum.Default);
            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                ResetDefaultClip(item.Key);
            }
        }

        public static void ResetDefaultAllClipWhenStop()
        {
            if (!TweenTimeLineDataModel.StateInfo.IsRecording)
            {
                ChangeTweenManagerState(ClipBehaviourStateEnum.Default);
            }
            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                // avoid state change when change select object when recording
                if (item.Value.BehaviourState == ClipBehaviourStateEnum.Recording)
                {
                    continue;
                }
                ResetDefaultClip(item.Key);
            }
        }

        public static void PreviewAllClip()
        {
            ReusablePrimeTweens();

            ChangeTweenManagerState(ClipBehaviourStateEnum.Preview);
            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                PreviewClip(item.Key);
            }
        }

        public static void PreviewClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];

            stateInfo.ChangeState(key,
                ClipBehaviourStateEnum.Preview);
            //     stateInfo.IsSelect ?
            //     ClipBehaviourStateEnum.Preview :
            //     ClipBehaviourStateEnum.Default);
        }

        public static void ResetDefaultClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];

            stateInfo.ChangeState(key,
                ClipBehaviourStateEnum.Default);
        }

        public static void PlayClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];
            stateInfo.ChangeState(key, ClipBehaviourStateEnum.Playing);
        }

        public static void TogglePlayClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];
            stateInfo.ToggleState(key, ClipBehaviourStateEnum.Playing);
        }

        public static void ToggleAllPLayClips(bool controlPlay = false)
        {
            ToggleTweenManagerState(ClipBehaviourStateEnum.Playing);
            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                TogglePlayClip(item.Key);
            }
        }

        public static void RecordClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];
            stateInfo.ChangeState(key, ClipBehaviourStateEnum.Recording);
        }

        public static void ToggleRecordClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];
            stateInfo.ToggleState(key, ClipBehaviourStateEnum.Recording);
        }

        public static void PlayAllClip(bool controlPlay = false)
        {
            ReusablePrimeTweens();

            ChangeTweenManagerState(ClipBehaviourStateEnum.Playing);
            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                PlayClip(item.Key);
            }
        }

        public static void ReusablePrimeTweens()
        {
            var primeTweenType = typeof(Tween).Assembly.GetType("PrimeTween.PrimeTweenManager");
            var instanceField = primeTweenType.GetField("Instance", BindingFlags.Static | BindingFlags.NonPublic);
            var instance = instanceField.GetValue(null);
            if (instance != null)
            {
                var updateMethodInfo = primeTweenType.GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);
                updateMethodInfo.Invoke(instance, null);
            }
        }

        private static void ChangeTweenManagerState(ClipBehaviourStateEnum behaviourStateType)
        {
            // Debug.Log(behaviourStateType + "===>" + TweenTimeLineDataModel.StateInfo.BehaviourState);

            if (TweenTimeLineDataModel.StateInfo.ChangeState(null, behaviourStateType))
            {
                TweenTimeLineDataModel.RefreshViewAction?.Invoke();
            }
        }

        private static void ToggleTweenManagerState(ClipBehaviourStateEnum behaviourStateType)
        {
            // Debug.Log(behaviourStateType + "===>" + TweenTimeLineDataModel.StateInfo.BehaviourState);
            if (TweenTimeLineDataModel.StateInfo.ToggleState(null, behaviourStateType))
            {
                TweenTimeLineDataModel.RefreshViewAction?.Invoke();
            }
        }

        public static void RecordAllSelectClip()
        {
            ChangeTweenManagerState(ClipBehaviourStateEnum.Recording);

            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                RecordClip(item.Key);
            }
        }

        public static void ToggleRecordAllSelectClip()
        {
            ToggleTweenManagerState(ClipBehaviourStateEnum.Recording);

            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                ToggleRecordClip(item.Key);
            }
        }
        #endregion


        #region TimeLine Tools
        public static void Play()
        {
            TimelineWindowExposer.PlayEditTimeline();
        }

        public static void AddTrack(Transform groupTrack,
            TrackInfoContext trackInfo, bool isIn, bool createNewTrack)
        {
            var timelineAsset = TimelineWindowExposer.GetCurDirector().playableAsset as TimelineAsset;


            Undo.RecordObject(timelineAsset, "Add Track");

            GroupTrack parentTrack = GetParentGroup(groupTrack, timelineAsset, isIn);

            AdjustStartTime(ref trackInfo);
            AddTrackWithParents(trackInfo, createNewTrack, parentTrack);
        }

        private static bool IsParentTrack(TrackAsset trackAsset, GroupTrack parentTrack)
        {
            if (trackAsset.parent == null) return false;

            GroupTrack selectGroupTrack = trackAsset.parent as GroupTrack;
            while (selectGroupTrack != null)
            {
                if (selectGroupTrack == parentTrack)
                {
                    return true;
                }
                selectGroupTrack = selectGroupTrack.parent as GroupTrack;
            }

            return false;
        }

        public static TrackAsset AddTrackWithParents(TrackInfoContext trackInfo,
            bool createNewTrack, TrackAsset parentTrack)
        {
            PlayableDirector playableDirector = TimelineWindowExposer.GetCurDirector();
            var timelineAsset = playableDirector.playableAsset as TimelineAsset;
            TrackAsset trackAsset = AddTrackToTimeline(trackInfo, timelineAsset, parentTrack, createNewTrack);
            var clipMethod = typeof(TrackAsset).GetMethod("CreateClip", BindingFlags.Instance | BindingFlags.NonPublic);
            var markMethod = typeof(TrackAsset).GetMethod("AddMarker", BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var clipInfo in trackInfo.clipInfos)
            {
                TimelineClip clip = clipMethod.Invoke(trackAsset, new object[]
                {
                    clipInfo.trackAssetType
                }) as TimelineClip;
                AddClip(clipInfo, clip);

                AddMarkers(trackAsset, markMethod, clipInfo);
            }

            TimelineWindowExposer.Bind(playableDirector, trackAsset, trackInfo.component);

            return trackAsset;
        }

        private static void AddMarkers(TrackAsset trackAsset, MethodInfo makrMethod, ClipInfoContext clipInfo)
        {
            foreach (var markInfo in clipInfo.markInfos)
            {
                ValueMaker so = null;

                if (markInfo.UpdateValue.GetType() == typeof(bool))
                    so = ScriptableObject.CreateInstance<BoolNotesMarker>();
                else if (markInfo.UpdateValue.GetType() == typeof(Sprite))
                    so = ScriptableObject.CreateInstance<SpriteNotesMarker>();
                else
                    so = ScriptableObject.CreateInstance<StringNotesMarker>();

                so.Value = markInfo.UpdateValue;
                so.FieldName = markInfo.FieldName;
                makrMethod.Invoke(trackAsset, new[]{so
                    });
                so.time = markInfo.Time;
                ((IMarker)so).Initialize(trackAsset);
            }
        }

        private static void AddClip(ClipInfoContext clipContextInfo, TimelineClip clip)
        {
            clip.start = clipContextInfo.start;
            clip.duration = clipContextInfo.duration;
            clip.displayName = clipContextInfo.GetDisplayName(clip.asset.GetType());

            if (!TimelineWindowExposer.GetBehaviourValue(clip.asset, out var value))
            {
                return;
            }

            var behaviour = value as IUniqueBehaviour;
            if (behaviour is EmptyBehaviour)
            {
                return;
            }
            behaviour.EndPos = clipContextInfo.endPos;
            behaviour.StartPos = clipContextInfo.startPos;
            behaviour.EasePreset = clipContextInfo.easePreset;
        }

        private static void AdjustStartTime(ref TrackInfoContext trackInfo)
        {
            foreach (var item in TweenTimeLineDataModel.TrackObjectDict)
            {
                TrackAsset trackAsset = item.Key;
                if (item.Value.Equals(trackInfo.component)
                    && trackInfo.trackType == trackAsset.GetType())
                {
                    if (TweenTimeLineDataModel.TrackBehaviourDict.TryGetValue(trackAsset, out var behaviourList))
                    {
                        double clipEnd = 0;
                        foreach (var behaviour in behaviourList)
                        {
                            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];
                            clipEnd = Math.Max(clipEnd, clipInfo.delayTime + clipInfo.duration);
                        }

                        foreach (var clipInfoContext in trackInfo.clipInfos)
                        {
                            clipInfoContext.start += clipEnd;
                        }
                    }
                    break;
                }
            }
        }

        public static GroupTrack GetParentGroup(Transform groupTrack, TimelineAsset timelineAsset, bool isIn)
        {
            string groupTrackName = groupTrack.name;
            if (groupTrack.tag == TweenTimelineDefine.PanelTag
            && !groupTrack.name.EndsWith(TweenTimelineDefine.PanelTag))
            {
                groupTrackName = $"{groupTrackName}{TweenTimelineDefine.PanelTag}";
            }
            if (groupTrack.tag == TweenTimelineDefine.CompositeTag)
            {
                // bool contains = false;
                // foreach (var item in TweenTimelineDefine.UIComponentTypeMatch)
                // {
                //     if (groupTrack.name.EndsWith(item.Key))
                //     {
                //         contains = true;
                //         break;
                //     }
                // }

                if (
                    // !contains &&
                    !groupTrack.name.EndsWith(TweenTimelineDefine.CompositeTag))
                {
                    groupTrackName = $"{groupTrackName}{TweenTimelineDefine.CompositeTag}";
                }
            }

            var parentTrack = GetSelectParentTrack(timelineAsset, groupTrack);
            if (parentTrack == null)
            {
                string rootTrackName = isIn ?
                    TweenTimelineDefine.InDefine :
                    TweenTimelineDefine.OutDefine;
                parentTrack = CreateRootTrack(timelineAsset, rootTrackName);
            }
            return CreateGroupAsset(groupTrackName, timelineAsset, parentTrack);
        }

        public static bool CanPlay(TrackAsset trackAsset)
        {
            var timelineAsset = TimelineWindowExposer.GetEditTimeLineAsset();
            if (timelineAsset == null)
            {
                return true;
            }
            var selectTracks = TimelineWindowExposer.GetSelectTracks();
            if (selectTracks == null
            || selectTracks.Count() <= 0)
            {
                return true;
            }

            foreach (var selectTrack in selectTracks)
            {
                if (selectTrack == trackAsset)
                {
                    return true;
                }

                if (selectTrack is GroupTrack selectGroupTrack)
                {
                    if (IsParentTrack(trackAsset, selectGroupTrack))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static GroupTrack GetSelectParentTrack(TimelineAsset timelineAsset, Transform goTrans)
        {
            var selectTracks = TimelineWindowExposer.GetSelectTracks();

            foreach (var selectTrack in selectTracks)
            {
                if (selectTrack is GroupTrack selectGroupTrack)
                {
                    if (goTrans.tag == TweenTimelineDefine.PanelTag)
                    {
                        if (selectTrack.name == TweenTimelineDefine.InDefine
                          || selectTrack.name == TweenTimelineDefine.OutDefine)
                        {
                            return selectGroupTrack;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    // Check for potential infinite loop by keeping track of visited transforms
                    var parentTrans = goTrans;
                    bool isChildOfSelectTrack = false;
                    while (parentTrans != null)
                    {
                        if (parentTrans.name == selectGroupTrack.name)
                        {
                            isChildOfSelectTrack = true;
                            break;
                        }
                        parentTrans = parentTrans.parent;
                    }
                    Assert.IsTrue(isChildOfSelectTrack, $"Try to add {selectGroupTrack.name} into different attach parent: {goTrans.name}");
                    if (isChildOfSelectTrack)
                    {
                        return selectGroupTrack;
                    }
                }
            }

            return null;
        }

        private static GroupTrack CreateRootTrack(TimelineAsset timelineAsset, string grandParentName)
        {
            GroupTrack parentTrack = null;
            var grandParentTrackIndex = TweenTimeLineDataModel.groupTracks.FindIndex(track => track.name == grandParentName);
            parentTrack = AddGroup(grandParentName, timelineAsset, null, grandParentTrackIndex);

            return parentTrack;
        }

        public static GroupTrack CreateGroupAsset(string groupTrackName, TimelineAsset timelineAsset, TrackAsset parentTrack)
        {
            GroupTrack groupTrack = null;

            bool MatchGroup(GroupTrack track)
            {
                if (track.name == groupTrackName)
                {
                    var root = GetTrackRoot(track);
                    if (parentTrack == null)
                    {
                        return true;
                    }
                    var parentRoot = GetTrackRoot(parentTrack);

                    return root.name == parentRoot.name;
                }
                return false;
            }

            var groupTrackIndex = TweenTimeLineDataModel.groupTracks.FindIndex(MatchGroup);
            groupTrack = AddGroup(groupTrackName, timelineAsset, parentTrack, groupTrackIndex);

            return groupTrack;
        }

        private static GroupTrack AddGroup(string groupTrackName, TimelineAsset timelineAsset,
        TrackAsset parentTrack, int groupTrackIndex)
        {
            GroupTrack groupTrack;
            if (groupTrackIndex < 0)
            {
                groupTrack = timelineAsset.CreateTrack<GroupTrack>(parentTrack, groupTrackName);
                TweenTimeLineDataModel.groupTracks.Add(groupTrack); // since we don't trigger timeline change immediately
            }
            else
            {
                groupTrack = TweenTimeLineDataModel.groupTracks[groupTrackIndex];
            }

            return groupTrack;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="trackAssetType"></param>
        /// <param name="timelineAsset"></param>
        /// <param name="groupTrack"></param>
        /// <param name="createNewTrack">For example, if in different direction, it should be false</param>
        /// <returns></returns>
        private static TrackAsset AddTrackToTimeline(TrackInfoContext trackInfoContext,
            TimelineAsset timelineAsset, TrackAsset groupTrack, bool createNewTrack)
        {
            Component component = trackInfoContext.component;
            Type trackType = trackInfoContext.trackType;

            Assert.IsNotNull(trackType, nameof(trackType) + " != null");
            Assert.IsNotNull(component, nameof(component) + " != null");

            TrackAsset track = FindExistTrackAsset(component, trackType);
            if (track == null || createNewTrack)
            {
                string trackName = GetTrackName(component.transform);
                track = timelineAsset.CreateTrack(trackType, groupTrack, trackName);
                Debug.Log("Track added: " + track.name);
            }
            return track;
        }

        public static string GetTrackName(Transform child)
        {
            bool isFullPath = TweenTimelinePreferencesProvider.GetBool(TweenGenSettings.UseFullPathName);

            Transform trackRoot = isFullPath ?
             BindUtility.GetAttachRoot(child.transform) : null;

            string trackName = isFullPath
            ? child.GetFullPathTrans(trackRoot)
            : child.gameObject.name;

            return trackName;
            // return $"{trackName}_{componentType.FullName}_{trackType.Name}";
        }

        public static Transform FindTrackBindTarget(Transform root, TrackAsset trackAsset)
        {
            bool isFullPath = TweenTimelinePreferencesProvider.GetBool(TweenGenSettings.UseFullPathName);

            GetTrackBindInfos(trackAsset, out var bindTarget, out var bindType);
            if (isFullPath)
            {
                return root.Find(bindTarget);
            }
            return root.transform.FindChildByName(bindTarget);
        }

        public static IUniqueBehaviour GetBehaviourByTrackAsset(TrackAsset trackAsset)
        {
            IUniqueBehaviour uniqueBehaviour = null;
            foreach (var clip in trackAsset.GetClips())
            {
                uniqueBehaviour = GetBehaviourByTimelineClip(clip);
                break;
            }

            return uniqueBehaviour;
        }

        public static IUniqueBehaviour GetBehaviourByTimelineClip(TimelineClip clip)
        {
            IUniqueBehaviour uniqueBehaviour;
            if (!TimelineWindowExposer.GetBehaviourValue(clip.asset, out var value))
            {
                throw new System.Exception($"BehaviourValue is null ");
            }
            uniqueBehaviour = value as IUniqueBehaviour;
            return uniqueBehaviour;
        }

        public static void GetTrackBindInfos(TrackAsset trackAsset, out string bindTarget, out string bindType)
        {
            if (trackAsset is AudioTrack)
            {
                bindTarget = trackAsset.name;
                bindType = typeof(AudioSource).FullName;
                return;
            }

            foreach (var clip in trackAsset.GetClips())
            {
                if (!TimelineWindowExposer.GetBehaviourValue(clip.asset, out var value))
                {
                    throw new System.Exception($"BehaviourValue is null ");
                }
                var uniqueBehaviour = value as IUniqueBehaviour;
                bindTarget = uniqueBehaviour.BindTarget;
                bindType = uniqueBehaviour.BindType;
                return;
            }

            // bindTarget = string.Empty;
            // bindType = string.Empty;
            throw new NotImplementedException($"{trackAsset.name} don't have bind target");
        }

        public static TrackAsset FindExistTrackAsset(Component component, Type trackAssetType)
        {
            return TweenTimeLineDataModel.TrackObjectDict.FirstOrDefault(item =>
                item.Key.GetType() == trackAssetType
                && item.Value == component).Key;
        }
        #endregion

        public static TweenActionEditorWindow GetActionEditorWindow()
        {
            if (EditorWindow.HasOpenInstances<TweenActionEditorWindow>())
            {
                TweenActionEditorWindow window = EditorWindow.GetWindow<TweenActionEditorWindow>();
                return window;
            }
            return null;
        }

        public static bool EnsureCanPreview()
        {
            if (!InitTimeline())
            {
                // Debug.Log("Please open the timeLine window first");
                if (!IsInit)
                {
                    Debug.LogError("Fail to Init Timeline");
                }
                return false;
            }
            return true;
        }

        #region Utility
        internal static object GetStartValue(IUniqueBehaviour behaviour, bool isStart)
        {
            TrackAsset trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
            if (TweenTimeLineDataModel.TrackBehaviourDict.TryGetValue(trackAsset, out var behaviourList))
            {
                for (int i = 0; i < behaviourList.Count; i++)
                {
                    if (behaviour.Equals(behaviourList[i]))
                    {
                        var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];

                        if (isStart)
                        {
                            if (i == 0)
                            {
                                return behaviour.Get(target);
                            }
                            return behaviourList[i - 1].EndPos;
                        }
                        else
                        {
                            if (behaviourList.Count > i + 1)
                            {
                                return behaviourList[i + 1].StartPos;
                            }
                            return behaviourList[i].StartPos;
                        }
                    }
                }
            }
            return null;
        }

        public static TrackAsset GetTrackRoot(TrackAsset trackAsset)
        {
            if (trackAsset == null)
            {
                return null;
            }
            var trackAssetParent = trackAsset.parent as TrackAsset;

            if (trackAssetParent == null)
            {
                return trackAsset;
            }

            return GetTrackRoot(trackAssetParent);
        }

        public static TrackAsset GetTrackSecondRoot(TrackAsset trackAsset)
        {
            if (trackAsset == null)
            {
                return null;
            }
            var trackAssetParent = trackAsset.parent as TrackAsset;
            if (trackAssetParent == null)
            {
                return trackAsset;
            }

            var grandTrackAssetParent = trackAssetParent.parent as TrackAsset;
            if (grandTrackAssetParent == null)
            {
                return trackAsset;
            }

            return GetTrackSecondRoot(trackAssetParent);
        }

        public static int GetPanelTracks(IEnumerable<TrackAsset> tracks, out List<TrackAsset> trackHierarchy, out TrackAsset rootTrack)
        {
            int isIn = -10;
            rootTrack = null;
            trackHierarchy = new List<TrackAsset>();
            foreach (var track in tracks)
            {
                rootTrack = TweenTimelineManager.GetTrackRoot(track);
                int inDir = rootTrack.name == TweenTimelineDefine.InDefine ? 1
                         : (rootTrack.name == TweenTimelineDefine.OutDefine ? -1 : 0);

                if (isIn == -10)
                {
                    isIn = inDir;
                }
                else
                {
                    if (inDir != isIn)
                    {
                        throw new InvalidOperationException($"A different track was detected: {track.name} vs {rootTrack.name}.");
                    }
                }

                if (track.name == rootTrack.name)
                {
                    trackHierarchy.Clear();
                    trackHierarchy.AddRange(rootTrack.GetChildTracks());
                    break;
                }
                if (track.parent.name == rootTrack.name)
                {
                    if (!trackHierarchy.Contains(track))
                    {
                        trackHierarchy.Add(track);
                    }
                }
            }

            if (trackHierarchy.Count <= 0)
            {
                throw new System.Exception("Please select panel or composite group track");
            }

            return isIn;
        }

        public static void SelectBeforeToggle(IUniqueBehaviour behaviour)
        {
            ClipInfo clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[behaviour];
            var curTime = TimelineWindowExposer.GetSequenceTime();
            if (curTime >= clipInfo.delayTime && curTime <= clipInfo.delayTime + clipInfo.duration)
            {
                return;
            }
            TimelineWindowExposer.SkipToTimelinePos(stateInfo.IsRecordStart ?
            clipInfo.delayTime : clipInfo.delayTime + clipInfo.duration);
        }


        #endregion
    }

}
