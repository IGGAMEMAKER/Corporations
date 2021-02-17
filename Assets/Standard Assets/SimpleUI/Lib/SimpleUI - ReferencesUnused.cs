using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace SimpleUI
{
    // Scripts, attached to prefab
    /*
    public partial class SimpleUI
    {
        void RenderScriptsAttachedToThisPrefab(SimpleUISceneType p)
        {
            var GO = Selection.activeObject as GameObject;
            var scripts = new Dictionary<string, int>();

            RenderScriptsAttachedToThisGameObject(GO.transform, ref scripts);

            Debug.Log("Scripts, attached to PREFAB");

            foreach (var s in scripts)
                Debug.Log(s.Key);
        }

        void RenderScriptsAttachedToThisGameObject(Transform GO, ref Dictionary<string, int> scripts)
        {
            foreach (Transform go in GO.transform)
            {
                foreach (Component c in go.GetComponents<Component>())
                {
                    string name = ObjectNames.GetInspectorTitle(c);
                    if (name.EndsWith("(Script)"))
                    {
                        string formated = name.Replace("(Script)", String.Empty).Replace(" ", String.Empty) + ".cs";
                        scripts[formated] = 1;
                    }
                }

                RenderScriptsAttachedToThisGameObject(go, ref scripts);
            }
        }
    }
    */

    public partial class SimpleUI
    {
        /*
        public static void PrintMatchInfo(IEnumerable<PrefabMatchInfo> matches)
        {
            foreach (var matchingComponent in matches)
            {
                if (matchingComponent.IsDirectMatch)
                {
                    // directly appears in prefab
                    // so you can upgrade it and safely save ur prefab

                    Print($"Directly occurs as {matchingComponent.ComponentName} in {matchingComponent.PrefabAssetPath}");
                }
                else
                {
                    // appears somewhere in nested prefabs

                    if (matchingComponent.IsOverridenAsAddedComponent)
                    {
                        // is added by root component

                        Print($"{matchingComponent.ComponentName} Is <b>ATTACHED BY ROOT</b> to some nested prefab\n");
                    }
                    else
                    {
                        // is part of nested prefab
                        if (matchingComponent.IsOverridenAsComponentProperty)
                        {
                            Print($"Root <b>OVERRIDES VALUES</b> on {matchingComponent.ComponentName}");
                        }

                        if (matchingComponent.IsNormalPartOfNestedPrefab)
                        {
                            Print($"{matchingComponent.ComponentName} is <b>part of a nested prefab</b>");
                        }
                    }
                }
            }
        }

        public static bool HasOverridenProperties(MonoBehaviour component, string[] properties)
        {
            var result = PrefabUtility.HasPrefabInstanceAnyOverrides(component.gameObject, false);


            var overrides = PrefabUtility.GetObjectOverrides(component.gameObject);

            // var wat = overrides.First().coupledOverride.GetAssetObject();
            // Debug.Log("first override " + wat);

            var nearestPrefab = PrefabUtility.GetCorrespondingObjectFromSource(component);

            // Debug.Log($"Check overrides for component {component.gameObject.name}");

            foreach (var modification in PrefabUtility.GetPropertyModifications(component))
            {
                if (modification.propertyPath.Contains("m_"))
                    continue;

                if (properties.Contains(modification.propertyPath))
                    return true;
                // Debug.Log($"Property: {modification.propertyPath}");
                // Debug.Log($"Value: {modification.value}");
                // Debug.Log(modification.target);
            }

            //Debug.Log("Corresponding object for " + component.gameObject.name + " is " + nearestPrefab.name);

            //var str = result ? "HAS" : "Has NO";

            //// PrintArbitraryInfo(null, $"{component.gameObject.name} {str} overrides"); // ({root.gameObject.name})

            //return result;

            return false;
        }

        // not used
        public static bool IsRootOverridenProperties(MonoBehaviour component, GameObject root, string[] properties)
        {
            var fastFilter = new Func<PropertyModification, bool>(p => properties.Contains(p.propertyPath));
            var print = new Func<IEnumerable<PropertyModification>, string>(p => string.Join(", ", p.Select(pp => pp.propertyPath).ToList()));

            var outermost = PrefabUtility.GetOutermostPrefabInstanceRoot(component);
            var outermostPropertyChanges = PrefabUtility.GetPropertyModifications(outermost).Where(fastFilter);

            var outermostPath = AssetDatabase.GetAssetPath(outermost);
            var outermostAsset = AssetDatabase.LoadMainAssetAtPath(outermostPath);

            // var objectOverrides = PrefabUtility.GetObjectOverrides(component.gameObject).Where(change => change.instanceObject.GetType() == typeof(OpenUrl));
            // var propertyChanges = PrefabUtility.GetPropertyModifications(component.gameObject).Where(p => !p.propertyPath.Contains("m_") && properties.Contains(p.propertyPath));
            // PrefabUtility.HasPrefabInstanceAnyOverrides()
            var propertyChanges = PrefabUtility.GetPropertyModifications(component.gameObject).Where(fastFilter);

            var text = $"Outermost changes {outermost.name} hasAnyChanges={PrefabUtility.HasPrefabInstanceAnyOverrides(outermost, false)}, {outermostPath}\n";
            text += print(outermostPropertyChanges);
            text += "\nComponent changes\n";
            text += print(propertyChanges);

            Debug.Log(text);

            return propertyChanges.Any();
        }

        // not used
        public static PrefabMatchInfo GetPrefabMatchInfo(MonoBehaviour component, GameObject root, string path, string[] matchingProperties)
        {
            var matchInfo = new PrefabMatchInfo { PrefabAssetPath = path, ComponentName = component.gameObject.name };
            return matchInfo;
            string text;


            var objectOverrides = PrefabUtility.GetObjectOverrides(component.gameObject)
                .Where(change => change.instanceObject.GetType() == typeof(OpenUrl));
            var propertyChanges = PrefabUtility.GetPropertyModifications(component.gameObject)
                .Where(p => !p.propertyPath.Contains("m_"));

            var parent = PrefabUtility.GetCorrespondingObjectFromSource(component.gameObject);
            var nearest = PrefabUtility.GetNearestPrefabInstanceRoot(component);
            var outermost = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

            var selfAddedComponents = ParseAddedComponents(component.gameObject);
            var parentAddedComponents = ParseAddedComponents(parent);
            var nearestAddedComponents = ParseAddedComponents(nearest);
            var outermostAddedComponents = ParseAddedComponents(outermost);

            var urlChanges = propertyChanges
                    // .Where(p => p.target.GetInstanceID() == c.GetInstanceID())
                    .Where(p => matchingProperties.Contains(p.propertyPath))
                // .Where(p => p.target.GetType() == typeof(OpenUrl))
                ;

            if (urlChanges.Any())
                text = $"<b>HAS</b> {urlChanges.Count()} URL overrides of {component.gameObject.name} in {root.name}";
            else
                text =
                    $"<b>NO</b> url overrides of {component.gameObject.name} in {root.name}, while propertyChanges={propertyChanges.Count()}";

            var concatObjectOverrides = string.Join(", \n",
                objectOverrides.Select(change =>
                    (change.instanceObject.name + " (" + change.instanceObject.name + ")")));

            text += "\n\n" + $"({objectOverrides.Count()}) Object Overrides on: {component.gameObject.name}" + "\n\n" +
                    concatObjectOverrides;

            text += $"\n\nAdded Components self={component.gameObject}\n({selfAddedComponents})";
            text += $"\n\nAdded Components parent={parent}\n({parentAddedComponents})";
            text += $"\n\nAdded Components nearest={nearest}\n({nearestAddedComponents})";
            text += $"\n\nAdded Components outermost={outermost}\n({outermostAddedComponents})";

            Debug.Log(text);

            // PrintBlah(null, $"<b>NO</b> url overrides of {component.gameObject.name} in {root.name}. propertyChanges={urlChanges.Count()} hasOverrides=<b>{hasOverrides}</b>");


            // var c = component.gameObject;
            // var routeToRoot = new List<GameObject>();
            //
            // routeToRoot.Add(c);
            //
            // int counter = 0;
            // while (c.transform.parent != null && counter < 10)
            // {
            //     c = c.transform.parent.gameObject; 
            //     
            //     routeToRoot.Add(c);
            //
            //     counter++;
            // }
            //
            // if (counter == 10)
            // {
            //     PrintBlah(null, "<B>OVERFLOW</B>");
            // }
            // else
            // {
            //     routeToRoot.Reverse();
            //     foreach (var o in routeToRoot)
            //     {
            //         bool isRoot = root.GetInstanceID() == o.GetInstanceID();
            //         bool isPrefabSelf = PrefabUtility.IsAnyPrefabInstanceRoot(o);
            //         bool isPrefabVariantSelf = PrefabUtility.IsPartOfVariantPrefab(o);
            //
            //         var propertyChanges = PrefabUtility.GetPropertyModifications(o).ToList()
            //             .Where(p => !p.propertyPath.Contains("m_"))
            //             .Where(p => properties.Contains(p.propertyPath))
            //             .Where(p => p.target.GetType() == typeof(OpenUrl));
            //
            //         bool hasOverrides = false;
            //         
            //         PrintBlah(null, $"{o.name} - {o.GetInstanceID()}. isRoot={isRoot}, isPrefab={isPrefabSelf}, hasOverrides={hasOverrides}");
            //     }
            // }

            return matchInfo;
        }

        */
    }
}