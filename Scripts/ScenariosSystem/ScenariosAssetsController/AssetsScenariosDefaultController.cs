using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ScenariosSystem.ScenariosAssetsController
{
    class AssetsScenariosDefaultController : AssetsScenariosController
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        protected override void StartLoading()
        {
            Debug.Log("Default platform scenarios loading.");
            NotifyOnAssetsLoaded();
        }

        #endregion

        #region Enums



        #endregion
    }
}
