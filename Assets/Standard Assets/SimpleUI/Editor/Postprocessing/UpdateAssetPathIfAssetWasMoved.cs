using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleUI
{
    // COPIED FROM
    //https://answers.unity.com/questions/614348/change-default-script-folder.html

    //purpose of this postprocessor is to ensure that listed file extensions
    // are not in certain filepaths, when they are they are moved to a 
    //specified default path
    public class UpdateAssetPathIfAssetWasMoved : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var instance = SimpleUI.GetInstance();
            var prefabs = SimpleUI.GetPrefabsFromFile();

            for (var i = 0; i < movedFromAssetPaths.Count(); i++) // importedAssets
            {
                string oldFilePath = movedFromAssetPaths[i];
                string newPath = movedAssets[i];

                Debug.Log("Checking path: " + oldFilePath);

                string filename = Path.GetFileName(oldFilePath);

                Debug.Log(string.Format("Moving asset ({0}) from {2} to path: {1}", filename, newPath, oldFilePath));

                MovingUsedPrefab(oldFilePath, newPath, prefabs, instance);
            }

            for (var i = 0; i < importedAssets.Count(); i++)
            {
                var path = importedAssets[i];

                var index = path.LastIndexOf('/');
                var fileName = path.Substring(index);

                Debug.Log("Imported asset: " + fileName);
            }
        }

        static void MovingUsedPrefab(string oldPath, string newPath, List<SimpleUISceneType> prefabs, SimpleUI instance)
        {
            for (var i = 0; i < prefabs.Count; i++)
            {
                var p = prefabs[i];

                //Debug.Log("Checking prefab: " + p.AssetPath);

                if (p.AssetPath.Equals(oldPath))
                {
                    p.AssetPath = newPath;

                    instance.UpdatePrefab(p, i);
                }
            }

            instance.FindMissingAssets();
        }
    }

    public class FileModificationWarning : SaveAssetsProcessor
    {
        static string[] OnWillSaveAssets(string[] paths)
        {
            Debug.Log("OnWillSaveAssets");

            foreach (string path in paths)
            {
                //SimpleUI.UpdateAssetWithPath(path);
            }

            return paths;
        }
    }
}