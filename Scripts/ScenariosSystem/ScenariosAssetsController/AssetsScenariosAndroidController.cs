using MEC;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace ScenariosSystem.ScenariosAssetsController
{
    class AssetsScenariosAndroidController : AssetsScenariosController
    {
        #region Fields

        public const string COPIED_SCENARIOS_PACKAGE_NAME = "scenarios_cpy_package.zip";
        private const string PREFS_APP_VERSION = "AppVersion";
        private const string PREFS_BUILD_VERSION = "BuildVersion";

        #endregion

        #region Propeties

        public string CachedAppVersion {
            get;
            private set;
        }

        public string CachedBuildVersion { 
            get; 
            private set; 
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            CachedAppVersion = PlayerPrefs.HasKey(PREFS_APP_VERSION) ? PlayerPrefs.GetString(PREFS_APP_VERSION) : string.Empty;
            CachedBuildVersion = PlayerPrefs.HasKey(PREFS_BUILD_VERSION) ? PlayerPrefs.GetString(PREFS_BUILD_VERSION) : string.Empty;

            Debug.Log("APP_V: "+ CachedAppVersion);
            Debug.Log("BUID_V: " + CachedBuildVersion);
        }

        protected override void CacheData()
        {
            PlayerPrefs.SetString(PREFS_APP_VERSION, Application.version);
            PlayerPrefs.SetString(PREFS_BUILD_VERSION, BuildVersionSettings.Instance.BuildVersion);
            PlayerPrefs.Save();
        }

        public override bool CanLoadFromAssets()
        {
            bool isLoad = false;
            if (CachedAppVersion != Application.version || CachedBuildVersion != BuildVersionSettings.Instance.BuildVersion)
            {
                isLoad = true;
            }

            if (Directory.GetDirectories(ScenarioFilePath.GetScenariosRootPath()).Length < 1)
            {
                isLoad = true;
            }

            return isLoad;
        }

        protected override void StartLoading()
        {
            Debug.Log("Extract scenarios Android...");

            string targetCompressScenariosPath = Path.Combine(Application.streamingAssetsPath, ScenariosContentSettings.Instance.ScenariosPackageName + ".zip");
            MEC.Timing.RunCoroutine(_ExtractScenariosArchive(targetCompressScenariosPath, ScenarioFilePath.GetScenariosRootPath()));
        }

        private IEnumerator<float> _ExtractScenariosArchive(string compressedDataPath, string extractPath)
        {
            Directory.Delete(extractPath, true);
            Directory.CreateDirectory(extractPath);

            string copiedFilePath = Path.Combine(extractPath, COPIED_SCENARIOS_PACKAGE_NAME);

            UnityWebRequest loadingRequest = UnityWebRequest.Get(compressedDataPath);
            yield return Timing.WaitUntilDone(loadingRequest.SendWebRequest());

            while (!loadingRequest.downloadHandler.isDone)
            {
                if (loadingRequest.isNetworkError || loadingRequest.isHttpError)
                {
                    break;
                }
            }
            if (loadingRequest.isNetworkError || loadingRequest.isHttpError)
            {
                Debug.Log("Loading request error for: " + compressedDataPath);
            }

            File.WriteAllBytes(copiedFilePath, loadingRequest.downloadHandler.data);
            Debug.LogFormat("Copied scenarios to path: {0}", copiedFilePath);

            SharpZipFacade.ExtractZipFile(copiedFilePath, extractPath);
            File.Delete(copiedFilePath);

            // Wyslanie informacji, ze ladowanie sie zakonczylo.
            NotifyOnAssetsLoaded();
        }

        #endregion

        #region Enums



        #endregion
    }
}
