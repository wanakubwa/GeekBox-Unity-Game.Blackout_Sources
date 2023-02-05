using System;
using TutorialSystem;
using UnityEngine;

[RequireComponent(typeof(TutorialPopUpView), typeof(TutorialPopUpModel))]
public class TutorialPopUpController : PopUpController
{
    #region Fields



    #endregion

    #region Propeties

    private TutorialPopUpModel CurrentModel
    {
        get;
        set;
    }

    private TutorialPopUpView CurrentView
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<TutorialPopUpModel>();
        CurrentView = GetView<TutorialPopUpView>();
    }

    public void SetUp(TutorialType type, Action<TutorialType> onTutorialFinish)
    {
        CurrentModel.SetUp(type, onTutorialFinish);
        CurrentView.RefreshView();
    }

    public void OnPageChanged()
    {
        CurrentView.OnPageChanged();
    }

    public void FinishTutorial()
    {
        CurrentModel.FinishTotorial();
        ClosePopUp();
    }

    #endregion

    #region Enums



    #endregion
}
