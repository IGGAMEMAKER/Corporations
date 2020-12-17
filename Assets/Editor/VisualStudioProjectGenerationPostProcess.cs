

// https://gamedev.stackexchange.com/questions/166401/use-a-different-target-framework-version-in-a-unity-c-project-other-than-4-6

// target migrator
// https://stackoverflow.com/questions/28436066/retargeting-all-projects-in-a-solution-to-net-4-5-2

//#if (UNITY_EDITOR)
//public class VisualStudioProjectGenerationPostProcess : AssetPostprocessor
//{
//    private static void OnGeneratedCSProjectFiles()
//    {
//        Debug.Log("OnGeneratedCSProjectFiles");
//        Debug.Log("-------------------------");

//        //var dir = Directory.GetCurrentDirectory();
//        //var files = Directory.GetFiles(dir, "*.csproj");
//        //foreach (var file in files)
//        //    ChangeTargetFrameworkInfProjectFiles(file);
//    }

//    static void ChangeTargetFrameworkInfProjectFiles(string file)
//    {
//        Debug.Log("Changing Target Framework for: " + file);

//        var text = File.ReadAllText(file);
//        var find = "TargetFrameworkVersion>*</TargetFrameworkVersion";
//        var replace = "TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion";

//        if (text.IndexOf(find) != -1)
//        {
//            text = Regex.Replace(text, find, replace);
//            File.WriteAllText(file, text);
//        }
//    }
//}
//#endif