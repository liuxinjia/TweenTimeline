using System.Collections.Generic;
using System.IO;
using Cr7Sund.TweenTimeLine;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cr7Sund
{
    public class GifUIControl : Image
    {
        private GifData _gifData;
        private string filePath;
        private int mCurFrame = 0;
        float curTotalTime = 0;
        private string sequenceID;
        private static GifCache _cache;

        public GifUIControl(string path)
        {
            filePath = path;

            this.RegisterCallback<AttachToPanelEvent>(e =>
                Play());
            this.RegisterCallback<DetachFromPanelEvent>(e =>
                Stop());
        }

        private void Stop()
        {
            EditorTweenCenter.UnRegisterEditorTimer(sequenceID);
        }

        private void Play()
        {
            System.Diagnostics.Stopwatch _profilerWatch = new();

            _profilerWatch.Restart();
            InitializeCache();
            TweenTimelineManager.LogProfile($"Play time: {_profilerWatch.ElapsedMilliseconds} ms"); // Log elapsed time

            _profilerWatch.Restart();
            LoadGifData();
            TweenTimelineManager.LogProfile($"LoadGifData time: {_profilerWatch.ElapsedMilliseconds} ms"); // Log elapsed time

            _profilerWatch.Restart();
            RegisterUpdateCallback();
            TweenTimelineManager.LogProfile($"RegisterUpdateCallback time: {_profilerWatch.ElapsedMilliseconds} ms"); // Log elapsed time
        }

        private void InitializeCache()
        {
            if (_cache == null)
            {
                _cache = LoadGifCacheFromAssets();
                if (_cache == null)
                {
                    _cache = ScriptableObject.CreateInstance<GifCache>();
                }
            }
        }

        private GifCache LoadGifCacheFromAssets()
        {
            System.Diagnostics.Stopwatch profilerWatch = new();
            profilerWatch.Restart();

            var searchInFolders = new List<string> {
                TweenTimelineDefine.BuiltInGIFPresetFolder,
                 };
            if (AssetDatabase.AssetPathExists(TweenTimelineDefine.CustomGIFPresetFolder))
            {
                searchInFolders.Add(TweenTimelineDefine.CustomGIFPresetFolder);
            }
            var guids = AssetDatabase.FindAssets("t:GifCache",
            searchInFolders.ToArray());

            TweenTimelineManager.LogProfile($"Find Assetse: {profilerWatch.ElapsedMilliseconds} ms"); // Log elapsed time

            profilerWatch.Restart();
            foreach (var guid in guids)
            {
                return AssetDatabase.LoadAssetAtPath<GifCache>(AssetDatabase.GUIDToAssetPath(guid));
            }
            TweenTimelineManager.LogProfile($"LoadAssetAtPath: {profilerWatch.ElapsedMilliseconds} ms"); // Log elapsed time

            return null;
        }

        private void LoadGifData()
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            int cacheIndex = _cache.gifDataLists.FindIndex(x => Path.GetFileNameWithoutExtension(x.FilePath) == fileName);
            if (cacheIndex >= 0)
            {
                _gifData = _cache.gifDataLists[cacheIndex];
            }
            else
            {
                UnityEngine.Debug.LogWarning("Please use the cache GIF, if you fell stuck");
                _gifData = new GifData(filePath);
                _cache.gifDataLists.Add(_gifData);
                _gifData.CreateGIFWithouCache();
            }
        }

        private void RegisterUpdateCallback()
        {
            sequenceID = EditorTweenCenter.RegisterUpdateCallback(this, -1, (element, elapsedTime) =>
            {
                UpdateGifFrame(elapsedTime);
            });
        }

        private void UpdateGifFrame(float elapsedTime)
        {
            var mTime = elapsedTime - curTotalTime;
            if (mTime >= _gifData.Frames[_gifData.Frames.Count - 1].Delay)
            {
                ResetGifState(elapsedTime);
            }
            if (mTime >= _gifData.Frames[mCurFrame].Delay)
            {
                AdvanceGifFrame();
            }
        }

        private void ResetGifState(float elapsedTime)
        {
            curTotalTime = elapsedTime;
            mCurFrame = 0;
        }

        private void AdvanceGifFrame()
        {
            mCurFrame = (mCurFrame + 1) % _gifData.Frames.Count;
            image = _gifData.Frames[mCurFrame].Texture;
        }

    }
}
