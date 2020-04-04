//using UnityEngine;
//using UnityEditor;
//using System.Collections.Generic;
//using System.IO;

//// COPIED FROM
////https://answers.unity.com/questions/614348/change-default-script-folder.html

////purpose of this postprocessor is to ensure that listed file extensions
//// are not in certain filepaths, when they are they are moved to a 
////specified default path
//public class FileImportHandler :
//    AssetPostprocessor
//{
//    //only evaluate files imported into these paths
//    static List<string> pathsToMoveFrom = new List<string>()
//    {
//        "Assets"
//    };


//    static Dictionary<string, string> defaultFileLocationByExtension = new Dictionary<string, string>()
//     {
//         //{".mp4",   "Assets/StreamingAssets/"},//for IOS, movies need to be in StreamingAssets
 
//         //{".anim",   "Assets/Art/Animations/"},
//         //{".mat",    "Assets/Art/Materials/"},
//         //{".fbx",    "Assets/Art/Meshes/"},
 
//         ////Images has subfolders for Textures, Maps, Sprites, etc.
//         //// up to the user to properly sort the images folder
//         //{".bmp",    "Assets/Art/Images/"},
//         //{".png",    "Assets/Art/Images/"},
//         //{".jpg",    "Assets/Art/Images/"},
//         //{".jpeg",   "Assets/Art/Images/"},
//         //{".psd",    "Assets/Art/Images/"},

//         //{".mixer",    "Assets/Audio/Mixers"},
//         ////like images, there are sub folders that the user must manage
//         //{".wav",    "Assets/Audio/Sources"}, 
//         //like images, there are sub folders that the user must manage
//         {".cs",     "Assets/Views/"},
//         //{".shader", "Assets/Dev/Shaders"},
//         //{".cginc",  "Assets/Dev/Shaders"}
//     };

//    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
//    {
//        List<string> oldFilePaths = new List<string>();
//        List<string> newFilePaths = new List<string>();

//        foreach (string oldFilePath in importedAssets)
//        {
//            Debug.Log("Checking path: " + oldFilePath);

//            string directory = Path.GetDirectoryName(oldFilePath);
//            if (!pathsToMoveFrom.Contains(directory))
//                continue;

//            string extension = Path.GetExtension(oldFilePath).ToLower();
//            if (!defaultFileLocationByExtension.ContainsKey(extension))
//                continue;

//            string filename = Path.GetFileName(oldFilePath);
//            string newPath = defaultFileLocationByExtension[extension];

//            oldFilePaths.Add(oldFilePath);
//            newFilePaths.Add(newPath + filename);
//            //var str = AssetDatabase.MoveAsset(oldFilePath, newPath + filename);

//            Debug.Log(string.Format("Moving asset ({0}) from {2} to path: {1}", filename, newPath, oldFilePath));
//        }

//        for (var i = 0; i < oldFilePaths.Count; i++)
//        {
//            File.Move(oldFilePaths[i], newFilePaths[i]);
//        }
//    }
//}