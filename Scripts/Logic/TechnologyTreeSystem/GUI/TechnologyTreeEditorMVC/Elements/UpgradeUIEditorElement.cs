using GeekBox.TechnologyTree;
using GeekBox.TechnologyTree.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradeUIEditorElement : MonoBehaviour , IIDEquatable, IDragHandler, ICleanable
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI idText;
    [SerializeField]
    private TMP_InputField nameKeyInput;
    [SerializeField]
    private TMP_Dropdown iconsDropdown;
    [SerializeField]
    private TMP_Dropdown nodeTypeDropdown;
    [SerializeField]
    private Image iconPreview;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button prevButton;

    [Header("Popups")]
    [SerializeField]
    private TechnologyTreeEffectsEditorController effectsEditorPopup;

    #endregion

    #region Propeties

    private TextMeshProUGUI IdText { 
        get => idText; 
    }

    private TMP_InputField NameKeyInput { 
        get => nameKeyInput; 
    }

    private TMP_Dropdown IconsDropdown { 
        get => iconsDropdown; 
    }

    public Image IconPreview { 
        get => iconPreview; 
    }

    public UpgradeNode CachedUpgrade {
        get;
        private set;
    }

    public int ID => CachedUpgrade.ID;

    private IUpgradeUIEditorListener Listener {
        get;
        set;
    }

    private List<UpgradeUIEditorConnection> Connections { 
        get; 
        set; 
    } = new List<UpgradeUIEditorConnection>();

    public Button NextButton { get => nextButton; }
    public Button PrevButton { get => prevButton; }
    public TechnologyTreeEffectsEditorController EffectsEditorPopup { get => effectsEditorPopup; }
    public TMP_Dropdown NodeTypeDropdown { get => nodeTypeDropdown; }

    #endregion

    #region Methods

    public void Initialize(UpgradeNode targetUpgrade, IUpgradeUIEditorListener listener)
    {
        CachedUpgrade = targetUpgrade;
        Listener = listener;

        InitializeVisualization();
        InitializePosition();

        StartCoroutine(_WaitAndRefreshTextFields());
    }

    public void AddConnection(UpgradeUIEditorConnection connection)
    {
        Connections.Add(connection);
    }

    public void RemoveConnection(UpgradeUIEditorConnection connection)
    {
        Connections.Remove(connection);
    }

    public void OnNameKeyChanged(string key)
    {
        CachedUpgrade.SetNameKey(key);
    }

    public void OnIconChanged(int index)
    {
        CachedUpgrade.SetIconName(IconsDropdown.options[index].text);
        RefreshIconPreview();
    }

    public void OnNodeTypeChanged(int index)
    {
        CachedUpgrade.SetUpgradeType((UpgradeType)Enum.Parse(typeof(UpgradeType), NodeTypeDropdown.options[index].text));
    }

    public void OnDeleteButtonClick()
    {
        Listener.OnDeleteUpgrade(this);
    }

    public void OnEffectsButtonClick()
    {
        TechnologyTreeEffectsEditorController spawnedController = Instantiate(EffectsEditorPopup);
        spawnedController.Setup(CachedUpgrade);
    }

    public void OnNextConnectionButton()
    {
        Listener.OnClickCreateNextConnection(this);
    }

    public void OnPrevConnectionButton()
    {
        Listener.OnClickPrevConnection(this);
    }

    public bool IDEqual(int otherId)
    {
        return ID == otherId;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Listener.OnUpgradeElementDrag(this, eventData.position);
    }

    public void CleanData()
    {
        Connections.ClearClean();
        Destroy(gameObject);
    }

    private void InitializePosition()
    {
        transform.localPosition = CachedUpgrade.EditorPosition;
    }

    public void RefreshPosition(Vector3 position)
    {
        transform.localPosition = position;
        CachedUpgrade.SetEditorPosition(transform.localPosition);

        for (int i =0; i < Connections.Count; i++)
        {
            Connections[i].RefreshPosition();
        }
    }

    private void InitializeVisualization()
    {
        IdText.text = string.Format("ID: {0}", CachedUpgrade.ID.ToString());
        NameKeyInput.SetTextWithoutNotify(CachedUpgrade.NameKey);
        InitializeIconsDropdown();
        InitializeNodeTypeDropdown();
        OnIconChanged(IconsDropdown.value);
    }

    private void InitializeIconsDropdown()
    {
        string[] iconsNames = TechnologyTreeSettings.Instance.Icons.GetAllIconsNames();

        IconsDropdown.ClearOptions();
        IconsDropdown.AddOptions(iconsNames.ToList());

        int selectedIconIndex = iconsNames.IndexOf(CachedUpgrade.IconName);
        IconsDropdown.SetValueWithoutNotify(selectedIconIndex == Constants.DEFAULT_INDEX ? 0 : selectedIconIndex);
    }

    private void InitializeNodeTypeDropdown()
    {
        string[] names = Enum.GetNames(typeof(UpgradeType));

        NodeTypeDropdown.ClearOptions();
        NodeTypeDropdown.AddOptions(names.ToList());

        int selectedIndex = names.IndexOf(CachedUpgrade.CurrentType.ToString());
        NodeTypeDropdown.SetValueWithoutNotify(selectedIndex == Constants.DEFAULT_INDEX ? 0 : selectedIndex);
    }

    private void RefreshIconPreview()
    {
        IconPreview.sprite = TechnologyTreeSettings.Instance.Icons.GetIconByName(CachedUpgrade.IconName);
    }

    private IEnumerator _WaitAndRefreshTextFields()
    {
        NameKeyInput.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        NameKeyInput.gameObject.SetActive(true);
    }

    #endregion

    #region Enums



    #endregion
}