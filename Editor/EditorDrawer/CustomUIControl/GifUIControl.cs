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
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (_cache == null)
            {
                var guids = AssetDatabase.FindAssets("t:GifCache",
                new string[] {
                    TweenTimelineDefine.BuiltInConfigPath,
                    TweenTimelineDefine.CustomConfigPath });
                foreach (var guid in guids)
                {
                    _cache = AssetDatabase.LoadAssetAtPath<GifCache>(AssetDatabase.GUIDToAssetPath(guid));
                    break;
                }
                if (guids.Length == 0)
                {
                    _cache = ScriptableObject.CreateInstance<GifCache>();
                }
            }

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

            sequenceID = EditorTweenCenter.RegisterUpdateCallback(this, -1, (element, elapsedTime) =>
            {
                var mTime = elapsedTime - curTotalTime;
                if (mTime >= _gifData.Frames[_gifData.Frames.Count - 1].Delay)
                {
                    mTime = 0;
                    curTotalTime = elapsedTime;
                    mCurFrame = 0;
                }
                if (mTime >= _gifData.Frames[mCurFrame].Delay)
                {
                    mCurFrame = (mCurFrame + 1) % _gifData.Frames.Count;
                    var gifField = element as GifUIControl;
                    gifField.image = _gifData.Frames[mCurFrame].Frame;
                }
            });
        }

    }
}
