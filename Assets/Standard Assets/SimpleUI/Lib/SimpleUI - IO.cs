using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace SimpleUI
{
    // Save/Load info
    public partial class SimpleUI
    {
        // https://stackoverflow.com/questions/50800690/prevent-losing-data-for-editorwindow-in-dll-when-editor-recompiles

        public bool isProjectScanned => SessionState.GetBool("isProjectScanned", false);
        public void MarkProjectAsScanned() => SessionState.SetBool("isProjectScanned", true);

        const string PATH_BASE = "SimpleUI/"; // Assets/Standard Assets/

        const string PATH_CountableAssets = PATH_BASE + "CountableAssets.txt";
        const string PATH_SimpleUI = PATH_BASE + "SimpleUI.txt";

        const string PATH_MissingAssets = PATH_BASE + "SimpleUI-MissingUrls.txt";
        const string PATH_Matches = PATH_BASE + "SimpleUI-matches.txt";

        // getting data
        public void ScanProject(bool forceLoad = false)
        {
            if (!isProjectScanned || forceLoad)
            {
                BoldPrint("Scanning project (Loading assets & scripts)");

                MarkProjectAsScanned();

                FullAssetScan();
            }
        }
    }
}