using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

[RequireComponent(typeof(BottomPanelModel), typeof(BottomPanelView))]
class BottomPanelController : UIController
{

    #region Fields



    #endregion

    #region Propeties

    private BottomPanelModel CurrentModel
    {
        get;
        set;
    }

    private BottomPanelView CurrentView
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<BottomPanelModel>();
        CurrentView = GetView<BottomPanelView>();
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        if(SelectingManager.Instance != null)
        {
            SelectingManager.Instance.OnMapNodeSelected += OnMapNodeSelectedHandler;
        }
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        if(SelectingManager.Instance != null)
        {
            SelectingManager.Instance.OnMapNodeSelected -= OnMapNodeSelectedHandler;
        }
    }

    public void OnUpgradeElementClick(UpgradeUIElement element)
    {
        CurrentModel.TryBuyUpgrade(element.CachedProfile);
    }

    private void OnMapNodeSelectedHandler(MapNode selectedNode)
    {
        if(CurrentModel.CurrentSelectedNode != null && CurrentModel.CurrentSelectedNode.ParentId.Equals(Constants.NODE_PLAYER_PARENT_ID) == true)
        {
            CurrentModel.CurrentSelectedNode.Values.OnValueChanged -= OnSelectedNodeValueChanged;
        }

        if(selectedNode.ParentId == Constants.NODE_PLAYER_PARENT_ID)
        {
            selectedNode.Values.OnValueChanged += OnSelectedNodeValueChanged;
        }

        CurrentModel.SetSelectedNode(selectedNode);
        CurrentView.RefreshView();
    }

    private void OnSelectedNodeValueChanged(int value)
    {
        CurrentView.RefreshView();
    }

    #endregion

    #region Enums



    #endregion
}
