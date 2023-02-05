using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


public class LoadScenarioEditorPopUpView : PopUpView
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI scenarioNamePrefab;
    [SerializeField]
    private RectTransform namesContent;

    #endregion

    #region Propeties

    public TextMeshProUGUI ScenarioNamePrefab { get => scenarioNamePrefab; }
    public RectTransform NamesContent { get => namesContent; }
    private List<TextMeshProUGUI> SpawnedScenariosNames { get; set; } = new List<TextMeshProUGUI>();

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        RefreshScenariosNames(GetModel<LoadScenarioEditorPopUpModel>().GetAvaibleEditorScenarios());
    }

    private void RefreshScenariosNames(string[] names)
    {
        SpawnedScenariosNames.ClearDestroy();

        foreach (string name in names)
        {
            TextMeshProUGUI spawnedName = Instantiate(ScenarioNamePrefab);
            spawnedName.transform.ResetParent(NamesContent);
            spawnedName.text = name;

            SpawnedScenariosNames.Add(spawnedName);
        }
    }

    #endregion

    #region Enums



    #endregion
}
