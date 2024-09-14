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
                if (!action.label.Contains(filterName)) continue;

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
                if (action.label.Contains('.'))
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

        public void AddEffect(TweenActionEffect effect)
        {
            var existingEffectIndex = animationCollections.FindIndex(e => e.label == effect.label);
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
