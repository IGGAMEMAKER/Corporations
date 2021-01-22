using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// COPIED FROM
//https://answers.unity.com/questions/614348/change-default-script-folder.html

//purpose of this postprocessor is to ensure that listed file extensions
// are not in certain filepaths, when they are they are moved to a 
//specified default path
public class UpdateAssetPathIfAssetWasMoved : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        for (var i = 0; i < movedFromAssetPaths.Count(); i++) // importedAssets
        {
            string oldFilePath = movedFromAssetPaths[i];
            string newPath = movedAssets[i];

            Debug.Log("Checking path: " + oldFilePath);

            string filename = Path.GetFileName(oldFilePath);

            Debug.Log(string.Format("Moving asset ({0}) from {2} to path: {1}", filename, newPath, oldFilePath));

            //MovingUsedPrefab(newPath + filename);
            //MovingUsedPrefab(oldFilePath, newPath + filename);
            MovingUsedPrefab(oldFilePath, newPath);
        }
    }

    static void MovingUsedPrefab(string oldPath, string newPath)
    {
        var prefabs = SimpleUI.prefabs;

        //if (SimpleUI.prefabs.Any(p => p.AssetPath.Equals(newPath)))

        for (var i = 0; i < prefabs.Count; i++)
        {
            var p = prefabs[i];

            Debug.Log("Moving existing prefab: " + oldPath);

            if (p.AssetPath.Equals(oldPath))
            {
                p.AssetPath = newPath;


                SimpleUI.UpdatePrefab(p, i);
            }
        }

        SimpleUI.FindMissingAssets();
    }
}