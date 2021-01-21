using UnityEngine;

namespace Assets
{
    [DisallowMultipleComponent]
    public class DataManager: MonoBehaviour
    {
        //public World world;

        private static DataManager dataManager;

        public static DataManager instance
        {
            get
            {
                if (!dataManager)
                {
                    dataManager = FindObjectOfType(typeof(DataManager)) as DataManager;

                    if (!dataManager)
                    {
                        Debug.LogError("There needs to be one active DataManager script on a GameObject in your scene.");
                    }
                    else
                    {
                        //dataManager.Init();
                    }
                }

                return dataManager;
            }
        }

        //void Init()
        //{
        //    instance.world = new World();
        //}
    }
}
