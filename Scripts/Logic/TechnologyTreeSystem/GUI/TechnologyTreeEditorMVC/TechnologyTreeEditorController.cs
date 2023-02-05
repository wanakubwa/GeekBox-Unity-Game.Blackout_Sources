using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TechnologyTreeEditorModel), typeof(TechnologyTreeEditorView))]
public class TechnologyTreeEditorController : UIController, IUpgradeUIEditorListener
{
    #region Fields



    #endregion

    #region Propeties

    private TechnologyTreeEditorModel CurrentModel
    {
        get;
        set;
    }

    private TechnologyTreeEditorView CurrentView
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<TechnologyTreeEditorModel>();
        CurrentView = GetView<TechnologyTreeEditorView>();
    }

    public void OnDeleteUpgrade(UpgradeUIEditorElement sender)
    {
        CurrentModel.DeleteUpgradeNode(sender.CachedUpgrade);
        CurrentView.DeleteUpgradeVisualization(sender);
    }

    public void OnClickCreateNextConnection(UpgradeUIEditorElement sender)
    {
        CurrentModel.SetFirstSelectedNode(sender.CachedUpgrade);
        CurrentView.CreateSelectedConnectionVisualization(sender);
    }

    public void OnClickPrevConnection(UpgradeUIEditorElement sender)
    {
        if(CurrentModel.FirstSelectedNode != null)
        {
            if (CurrentModel.FirstSelectedNode.IDEqual(sender.CachedUpgrade.ID) == true)
            {
                Debug.Log("Can't create connection between same node!".SetColor(Color.red));
                return;
            }

            CurrentModel.AddConnectionToSelectedNode(sender.CachedUpgrade);
            CurrentView.DestroySelectedConnectionVisualization();
            CurrentView.HandleCreatedConnectionForUpgrades(CurrentModel.FirstSelectedNode, sender);
        }
        else
        {
            Debug.Log("You must select next button first!".SetColor(Color.red));
        }

        CurrentModel.HandleEndCreatingConnection();
    }

    public void OnUpgradeElementDrag(UpgradeUIEditorElement sender, Vector2 screenPoint)
    {
        sender.RefreshPosition(CurrentModel.CalculateUpgradeEditorPosition(screenPoint));
    }

    public void CreateUpgrade(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        if(pointerEventData.button == PointerEventData.InputButton.Right)
        {
            UpgradeNode upgrade = CurrentModel.CreateNewUpgrade(pointerEventData.position);
            CurrentView.AddUpgradeVisualization(upgrade);
            //todo: dodanie do widoku.
        }
    }

    public void ChangePanelZoom(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        CurrentView.ChangeMainContentZoom(pointerEventData.scrollDelta.y);
    }

    #endregion

    #region Enums



    #endregion
}
