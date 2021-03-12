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

        const string PATH_CountableAssets = "SimpleUI/CountableAssets.txt";
        const string PATH_SimpleUI = "SimpleUI/SimpleUI.txt";

        const string PATH_MissingAssets = "SimpleUI/SimpleUI-MissingUrls.txt";
        const string PATH_Matches = "SimpleUI/SimpleUI-matches.txt";

        List<CountableAsset> _assets;
        public List<CountableAsset> countableAssets
        {
            get
            {
                if (_assets == null)
                    LoadCountableAssets();

                return _assets;
            }
        }

        // getting data
        public void ScanProject(bool forceLoad = false)
        {
            if (!isProjectScanned || forceLoad)
            {
                BoldPrint("Scanning project (Loading assets & scripts)");

                MarkProjectAsScanned();

                LoadCountableAssets();
                // FullAssetScan();
            }
        }

        void LoadCountableAssets()
        {
            _assets = GetCountableAssetsFromFile();
        }


        static void SaveCountableAssets(List<CountableAsset> data)
        {
            SaveToFile(PATH_CountableAssets, data);
        }

        public static List<CountableAsset> GetCountableAssetsFromFile()
        {
            var fileName = PATH_CountableAssets;
            var fullDirectory = fileName; // Directory.GetCurrentDirectory() + fileName;

            if (!File.Exists(fullDirectory))
            {
                var fs = File.Create(fullDirectory);
                fs.Close();
            }

            return GetJSONDataFromFile<List<CountableAsset>>(fileName);
        }

        public void UpdateCountableAsset(CountableAsset asset, int index)
        {
            countableAssets[index] = asset;
            SaveCountableAssets(countableAssets);
        }
    }
}