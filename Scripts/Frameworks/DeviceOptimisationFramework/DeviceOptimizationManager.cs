using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace GB.Frameworks.DeviceOptimisationFramework
{
    public class DeviceOptimizationManager : MonoBehaviour
    {
        #region Fields

        public const int DEFAULT_SDK_VERSION = -1;

        private static DeviceOptimizationManager instance;

        [SerializeField]
        private int loDeviceSdkThreshold = 28;

        #endregion

        #region Propeties

        public event Action<DeviceQualityLvl> OnDeviceQualityUpdate = delegate { };

        public static DeviceOptimizationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (DeviceOptimizationManager)FindObjectOfType(typeof(DeviceOptimizationManager));
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public DeviceQualityLvl CurrentQualityLvl { get; private set; }

        #endregion

        #region Methods

        public void SetDeviceQualityLvl(DeviceQualityLvl qualityLvl)
        {
            CurrentQualityLvl = qualityLvl;
            OnDeviceQualityUpdate(CurrentQualityLvl);
        }

        private void DontDestroyCheck()
        {
            DeviceOptimizationManager[] objs = FindObjectsOfType<DeviceOptimizationManager>();

            if (objs.Length > 1)
            {
                gameObject.SetActive(false);
                Destroy(this.gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AutoInitialize()
        {
            if (Instance != null)
            {
                return;
            }

            GameObject go = new GameObject("DeviceOptimizationManager");
            DeviceOptimizationManager bob = go.AddComponent<DeviceOptimizationManager>();
            GameObject.DontDestroyOnLoad(go);

            Debug.Log("[DeviceOptimizationManager] Auto initialized!");
        }

        private void Awake()
        {
            int sdkVerison = GetAndroidSDKInt();
            Debug.Log("Device SDKVersion: " + sdkVerison);

            if(sdkVerison == DEFAULT_SDK_VERSION)
            {
                SetDeviceQualityLvl(DeviceQualityLvl.HIGH);
                return;
            }

            if(sdkVerison <= loDeviceSdkThreshold)
            {
                SetDeviceQualityLvl(DeviceQualityLvl.LOW);
            }
            else
            {
                SetDeviceQualityLvl(DeviceQualityLvl.HIGH);
            }
        }

        private int GetAndroidSDKInt()
        {
#if UNITY_ANDROID && !UNITY_EDITOR

            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                return version.GetStatic<int>("SDK_INT");
            }
#endif
            return DEFAULT_SDK_VERSION;
        }

        #endregion

#if UNITY_EDITOR

        [Button(ButtonSizes.Medium)]
        private void SwitchDeviceQualityLvl(DeviceQualityLvl qualityLvl)
        {
            SetDeviceQualityLvl(qualityLvl);
        }

#endif

        #region Enums

        public enum DeviceQualityLvl
        {
            HIGH,
            LOW
        }

        #endregion
    }
}
