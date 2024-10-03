using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cr7Sund.TweenTimeLine
{
    [CreateAssetMenu(menuName = "Cr7Sund/TweenTimeLine/TweenGenTrackConfig", fileName = "CustomTweenGenTrackConfig")]
    public class TweenGenTrackConfig : ScriptableObject
    {
        [FormerlySerializedAs("componentValuePairs")]
        public List<TweenComponentData> componentDatas;

        [ContextMenu(nameof(GenTrackAsset))]
        public void GenTrackAsset()
        {
            var configPath = AssetDatabase.GetAssetPath(this);
            string outPutPath = String.Empty;
            if (configPath.StartsWith(TweenTimelineDefine.CustomConfigPath))
            {
                outPutPath = TweenTimelineDefine.CustomControlTacksFolder;
            }
            else
            {
                outPutPath = TweenTimelineDefine.BuiltInControlTacksFolder;
            }

            if (!Directory.Exists(outPutPath))
            {
                Directory.CreateDirectory(outPutPath);
            }
            TweenCustomTrackCodeGenerator.GenerateCode(outPutPath, componentDatas);
            AnimationCollectionGenerator.CreateBuildInAnimation(this);
            CreateAnimEffects();

            AssetDatabase.Refresh();
        }

        private void CreateAnimEffects()
        {
            var setting = AssetDatabase.LoadAssetAtPath<TweenActionLibrary>(TweenTimelineDefine.tweenLibraryPath);

            foreach (var item in componentDatas)
            {
                setting.AddEffect(CreateAnimationEffect(item));
            }

            EditorUtility.SetDirty(setting);
            AssetDatabase.SaveAssetIfDirty(setting);
        }

        public static TweenActionEffect CreateAnimationEffect(TweenComponentData method)
        {
            string typeName = TweenCustomTrackCodeGenerator.GetTypeName(method.ComponentType);
            string identifier = TweenCustomTrackCodeGenerator.GetTweenBehaviourIdentifier(method);
            string tweenMethod = $"{identifier}ControlBehaviour";
            var tweenOperationType = method.GetTweenOperationType(tweenMethod);
            return new TweenActionEffect(method.GetPropertyMethod, typeName)
            {
                image = $"{typeName}.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = AnimationCollectionGenerator.GetDefaultValue(method.ValueType),
                        tweenOperationType= tweenOperationType,
                        tweenMethod =$"{tweenMethod}",
                        label = method.GetPropertyMethod,
                    }
                }
            };
        }
    }

}
