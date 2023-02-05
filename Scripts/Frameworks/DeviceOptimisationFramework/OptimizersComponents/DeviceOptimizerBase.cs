using UnityEngine;

namespace GB.Frameworks.DeviceOptimisationFramework
{
    public abstract class DeviceOptimizerBase : MonoBehaviour
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        protected abstract void OnLowLvl();
        protected abstract void OnHighLvl();

        private void OnEnable()
        {
            DeviceOptimizationManager.Instance.OnDeviceQualityUpdate += OnDeviceQualityLvlChangedHandler;
            OnDeviceQualityLvlChangedHandler(DeviceOptimizationManager.Instance.CurrentQualityLvl);
        }

        private void OnDisable()
        {
            if (DeviceOptimizationManager.Instance != null)
            {
                DeviceOptimizationManager.Instance.OnDeviceQualityUpdate -= OnDeviceQualityLvlChangedHandler;
            }
        }

        private void OnDeviceQualityLvlChangedHandler(DeviceOptimizationManager.DeviceQualityLvl qualityLvl)
        {
            switch (qualityLvl)
            {
                case DeviceOptimizationManager.DeviceQualityLvl.HIGH:
                    OnHighLvl();
                    break;
                case DeviceOptimizationManager.DeviceQualityLvl.LOW:
                    OnLowLvl();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
