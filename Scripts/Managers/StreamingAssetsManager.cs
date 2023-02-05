using ScenariosSystem;
using ScenariosSystem.ScenariosAssetsController;
using System;

public class StreamingAssetsManager : ManagerSingletonBase<StreamingAssetsManager>, IInitializable
{
    #region Fields

    #endregion

    #region Propeties

    public event Action OnInitialized = delegate { };

    private AssetsScenariosController ScenariosController {
        get; 
        set;
    }

    #endregion

    #region Methods

    public void Initialize()
    {
        if (ScenariosController.CanLoadFromAssets() == true)
        {
            ScenariosController.OnAssetsLoaded += HandleInitializeEnd;
            ScenariosController.LoadScenariosFromAssets();
        }
        else
        {
            HandleInitializeEnd();
        }
    }

    protected override void Awake()
    {
        base.Awake();

#if UNITY_ANDROID && !UNITY_EDITOR
        ScenariosController = new AssetsScenariosAndroidController();
#else

        ScenariosController = new AssetsScenariosDefaultController();
#endif

    }

    private void HandleInitializeEnd()
    {
        OnInitialized();
    }

#endregion

#region Enums



#endregion
}
