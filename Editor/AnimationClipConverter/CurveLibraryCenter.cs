using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public class CurveLibraryCenter
    {
        private CurveWrapperLibrary curveWrapperLibrary;

        
        public void CreateCurveWrapperLibrary(string path)
        {
            curveWrapperLibrary = AssetDatabase.LoadAssetAtPath<CurveWrapperLibrary>(path);
            if (curveWrapperLibrary == null)
            {
                curveWrapperLibrary = ScriptableObject.CreateInstance<CurveWrapperLibrary>();
                curveWrapperLibrary.Curves = new();
                AssetDatabase.CreateAsset(curveWrapperLibrary, path);
                AssetDatabase.SaveAssetIfDirty(curveWrapperLibrary);
            }
            else
            {
                curveWrapperLibrary.Curves.Clear();
            }
        }

        public void ConvertClip(AnimationClip clip)
        {
            var curves = GenCurveInfos(clip);
            curveWrapperLibrary.Curves.AddRange(curves.Where(curve => !curveWrapperLibrary.Curves.Any(c => c.name == curve.name)));
        }

        public void GenCurveInfoDict()
        {
            curveWrapperLibrary.GenCurveInfoDict();
        }

        private List<CurveWrapper> GenCurveInfos(AnimationClip clip)
        {
            List<CurveWrapper> curves = new();

            EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(clip);
            foreach (EditorCurveBinding binding in curveBindings)
            {
                AnimationCurve editorCurve = AnimationUtility.GetEditorCurve(clip, binding);
                string clipName = clip.name;
                string propertyName = binding.propertyName;
                AnimationClipConverter.MapProperty(binding.type, ref propertyName);
                string targetTypeName, tweenMethod, curveName;
                AnimationClipConverter.GetClipInfoName(clipName, binding.type, propertyName, out targetTypeName, out tweenMethod, out curveName);
                if (string.IsNullOrEmpty(curveName))
                {
                    continue;
                }
                var curve = new CurveWrapper()
                {
                    Curve = editorCurve,
                    name = curveName
                };
                curves.Add(curve); // 添加到曲线列表
            }

            return curves;
        }

    }
}
