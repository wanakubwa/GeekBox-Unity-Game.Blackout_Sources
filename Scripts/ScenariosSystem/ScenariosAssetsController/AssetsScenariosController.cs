using System;

namespace ScenariosSystem
{
    public class AssetsScenariosController
    {
        #region Fields



        #endregion

        #region Propeties

        public event Action OnAssetsLoaded = delegate { };

        #endregion

        #region Methods

        public AssetsScenariosController()
        {
            Initialize();
        }

        public void LoadScenariosFromAssets()
        {
            Initialize();

            if(CanLoadFromAssets() == true)
            {
                StartLoading();
            }

            CacheData();
        }

        public virtual bool CanLoadFromAssets()
        {
            return true;
        }

        protected virtual void Initialize()
        {

        }

        protected virtual void CacheData()
        {

        }

        protected virtual void StartLoading()
        {

        }

        protected void NotifyOnAssetsLoaded()
        {
            OnAssetsLoaded();
        }

        #endregion

        #region Enums



        #endregion
    }
}
