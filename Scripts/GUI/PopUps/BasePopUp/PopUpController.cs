using System;
using System.Xml.Serialization;
using UnityEngine;

public class PopUpController : UIMonoBehavior
{
    #region Fields

    [Space]
    [SerializeField]
    PopUpManager.PopUpPrority prority;

    [Space]
    [SerializeField]
    private PopUpModel model;
    [SerializeField]
    private PopUpView view;

    [Space(5)]
    [SerializeField]
    private Canvas popUpCanvas;
    [SerializeField]
    private CanvasGroup popUpCanvasGroup;

    #endregion

    #region Propeties

    public event Action OnPopUpClose = delegate { };

    internal PopUpModel Model
    {
        get => model;
    }

    internal PopUpView View
    {
        get => view;
    }

    public Canvas PopUpCanvas
    {
        get => popUpCanvas;
        private set => popUpCanvas = value;
    }

    public CanvasGroup PopUpCanvasGroup { get => popUpCanvasGroup; }

    public PopUpManager.PopUpPrority Prority
    {
        get => prority;
    }

    #endregion

    #region Methods

    public override void OnDisable()
    {
        base.OnDisable();

        DettachEvents();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        InAnimation inAnimation = GetComponent<InAnimation>();
        if(inAnimation != null)
        {
            inAnimation.PlayAnimation();
        }

        AttachEvents();
    }

    public void Start()
    {
        View.CustomStart();
    }

    public virtual void AttachEvents()
    {
        Model.AttachEvents();
        View.AttachEvents();
    }

    public virtual void DettachEvents()
    {
        Model.DettachEvents();
        View.DettachEvents();
    }

    public virtual void Initialize()
    {
        gameObject.SetActive(true);

        Model.Initialize();
        View.Initialize();
        InitializeCanvasCamera(Camera.main);
    }

    public void ClosePopUp()
    {
        PopUpManager.Instance.RequestClosePopUp(this);

        OnPopUpClose();

        SetPopUpInteractable(false);

        Model.ClosePopUp();
        View.ClosePopUp();

        OutAnimation inAnimation = GetComponent<OutAnimation>();
        if (inAnimation != null)
        {
            inAnimation.PlayAnimation(() => Destroy(gameObject));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TogglePopUp()
    {
        if (gameObject.activeInHierarchy == true)
        {
            Model.HidePopUp();
        }
        else
        {
            Model.ShowPopUp();
        }
    }

    public void SetPopUpInteractable(bool isInteractable)
    {
        if(PopUpCanvasGroup != null)
        {
            PopUpCanvasGroup.interactable = isInteractable;
        }
    }

    private void InitializeCanvasCamera(Camera mainCamera)
    {
        if (PopUpCanvas != null)
        {
            PopUpCanvas.worldCamera = mainCamera;
        }
    }

    public T GetModel<T>() where T : PopUpModel
    {
        T model = GetComponent<PopUpModel>() as T;
        return model;
    }

    public T GetView<T>() where T : PopUpView
    {
        T model = GetComponent<PopUpView>() as T;
        return model;
    }

    #endregion

    #region Handlers



    #endregion
}
