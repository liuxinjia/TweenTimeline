using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public struct TweenCollection
    {
        public List<TweenActionEffect> animationCollections;
        public string category;
        public static HashSet<string> IgnoreDotSet = new()
        {
           "color.a"
        };
        
        public TweenCollection(string name)
        {
            animationCollections = new();
            this.category = name;
        }

        public Dictionary<string, List<TweenActionEffect>> FilterActionCategory(string filterName, GameObject selectGO)
        {
            var actionCategoryList = new Dictionary<string, List<TweenActionEffect>>();

            foreach (var action in animationCollections)
            {
                if (!action.label.ToLower().Contains(filterName.ToLower())) continue;

                bool containsComponent = false;
                foreach (var actionUnit in action.animationSteps)
                {
                    var componentType = actionUnit.GetComponentType();
                    if (!typeof(Component).IsAssignableFrom(componentType))
                    {
                        continue;
                    }
                    if (selectGO == null)
                    {
                        continue;
                    }

                    var component = selectGO.GetComponent((Type)componentType);
                    containsComponent = component != null;
                }

                if (containsComponent)
                {
                    if (!actionCategoryList.TryGetValue(action.effectCategory, out var list))
                    {
                        list = new List<TweenActionEffect>();
                        actionCategoryList.Add(action.effectCategory, list);
                    }
                    list.Add(action);
                }
            }

            return actionCategoryList;
        }

        public Dictionary<string, List<TweenActionEffect>> GetAnimActionCategory(GameObject selectGO)
        {
            var actionCategoryList = new Dictionary<string, List<TweenActionEffect>>();

            foreach (var action in animationCollections)
            {
                if (!IgnoreDotSet.Contains(action.label)
                && action.label.Contains('.'))// skip specific action, e.g. position.x 
                {
                    continue;
                }

                bool containsComponent = false;
                foreach (var actionUnit in action.animationSteps)
                {
                    var componentType = actionUnit.GetComponentType();
                    if (componentType == typeof(UnityEngine.Transform)) // default show
                    {
                        containsComponent = true;
                        continue;
                    }

                    if (!typeof(Component).IsAssignableFrom(componentType)) // exclude material
                    {
                        continue;
                    }
                    if (selectGO == null)
                    {
                        continue;
                    }

                    var component = selectGO.GetComponent((Type)componentType);
                    containsComponent = component != null;
                    if (!containsComponent)
                    {
                        break;
                    }
                }

                if (containsComponent)
                {
                    if (!actionCategoryList.TryGetValue(action.effectCategory, out var list))
                    {
                        list = new List<TweenActionEffect>();
                        actionCategoryList.Add(action.effectCategory, list);
                    }
                    list.Add(action);
                }
            }

            return actionCategoryList;
        }

        public void AddEffect(TweenActionEffect effect)
        {
            var existingEffectIndex = animationCollections.FindIndex(e => e.label == effect.label
            && e.collectionCategory == effect.collectionCategory
            && e.effectCategory == effect.effectCategory);
            if (existingEffectIndex >= 0)
            {
                // Update existing effect
                animationCollections[existingEffectIndex].CopyFrom(effect);
            }
            else
            {
                // Add new effect
                animationCollections.Add(effect);
            }
        }

        internal void RemoveEffect(TweenActionEffect effect)
        {
            var findIndex = animationCollections.FindIndex(e => e.label == effect.label);
            if (findIndex >= 0)
            {
                animationCollections.RemoveAt(findIndex);
            }
        }
    }

}
