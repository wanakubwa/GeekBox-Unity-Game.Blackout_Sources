using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GB.Frameworks.DeviceOptimisationFramework
{
    public class ImageMaterialDeviceOptimizer : DeviceOptimizerBase
    {

        #region Fields

        [SerializeField]
        private Image targetImage;

        [Space]
        [SerializeField]
        private Material highDeviceMaterial;
        [SerializeField]
        private Material lowDeviceMaterial;

        #endregion

        #region Propeties



        #endregion

        #region Methods

        protected override void OnHighLvl()
        {
            SetImageMaterial(highDeviceMaterial);
        }

        protected override void OnLowLvl()
        {
            SetImageMaterial(lowDeviceMaterial);
        }

        private void SetImageMaterial(Material mat)
        {
            targetImage.enabled = !(mat == null);
            targetImage.material = mat;
        }

        #endregion

        #region Enums



        #endregion
    }
}

