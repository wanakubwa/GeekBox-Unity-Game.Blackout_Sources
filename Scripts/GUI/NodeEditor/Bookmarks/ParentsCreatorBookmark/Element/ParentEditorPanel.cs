using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParentEditorPanel : UIMonoBehavior
{
    #region Fields

    [SerializeField]
    private Image colorVisualizationImage;
    [SerializeField]
    private TextMeshProUGUI parentIdText;
    [SerializeField]
    private TMP_Dropdown colorsDropdown;

    [Header("Setup buttons")]
    [SerializeField]
    private Button setupButton;

    #endregion

    #region Propeties

    public Image ColorVisualizationImage { 
        get => colorVisualizationImage; 
    }

    public TextMeshProUGUI ParentIdText { get => parentIdText; }
    public TMP_Dropdown ColorsDropdown { get => colorsDropdown; }

    public Button SetupButton { get => setupButton; }

    private NodeParent CachedParent
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void OnEnable()
    {
        base.OnEnable();

        ColorsDropdown.onValueChanged.AddListener(OnColorDropdownChangedHandler);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        ColorsDropdown.onValueChanged.RemoveAllListeners();
    }

    public void RefreshPanel(NodeParent parent)
    {
        CachedParent = parent;

        InitializeColorsDropdown();
        RefreshParentIdText();
        RefreshSetupButton();

        gameObject.SetActive(true);
    }

    public void SaveChanges()
    {
        if(CachedParent != null)
        {
            CachedParent.Settings.SetMainColor(GetSelectedColor().ParentColor);
            CachedParent.Settings.SetFontColor(GetSelectedColor().FontColor);
            CachedParent.Settings.SetShieldsColor(GetSelectedColor().ShieldsColor);

            UpdateParentNodesAfterSave();
        }
    }

    public void ShowParentSetupPopUp()
    {
        PopUpManager.Instance.ShowEditorParentSetupPopUp(CachedParent);
    }

    private void UpdateParentNodesAfterSave()
    {
        // To jest straszna ale dla edytora wystarczy.
        List<int> cachedNodesIds = CachedParent.NodesIdCollection;
        foreach (int nodeId in cachedNodesIds)
        {
            MapManager.Instance.MapNodesCollection.GetElementByID(nodeId)?.SetParent(CachedParent);
        }
    }

    private void InitializeColorsDropdown()
    {
        ColorsDropdown.ClearOptions();

        string[] colorsLabels = ParentsContentSettings.Instance.GetAllColorsLabels();
        ColorsDropdown.AddOptions(colorsLabels.ToList());
        ColorsDropdown.SetValueWithoutNotify(colorsLabels.IndexOf(ParentsContentSettings.Instance.GetColorLabelByColor(CachedParent.Settings.MainColor)));
        OnColorDropdownChangedHandler(-1);
    }

    private void RefreshParentIdText()
    {
        ParentIdText.text = string.Format("ID: {0}", CachedParent.ID);
    }

    private void RefreshSetupButton()
    {
        SetupButton.interactable = CachedParent.IsPlayerOrNeutralParent() == false;
    }

    private ParentsContentSettings.ParentColorValue GetSelectedColor()
    {
        int selectedIndex = ColorsDropdown.value;
        ParentsContentSettings.ParentColorValue selectedColor = ParentsContentSettings.Instance.GetParentColorByLabel(ColorsDropdown.options[selectedIndex].text);

        return selectedColor;
    }

    private void OnColorDropdownChangedHandler(int index)
    {
        ColorVisualizationImage.color = GetSelectedColor().ParentColor;
    }

    #endregion

    #region Enums



    #endregion
}
