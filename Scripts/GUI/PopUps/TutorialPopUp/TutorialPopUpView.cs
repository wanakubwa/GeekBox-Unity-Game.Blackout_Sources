using Sirenix.OdinInspector;
using TutorialSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class TutorialPopUpView : PopUpView
{
    #region Fields

    [SerializeField]
    private TutorialPage pagePrefab;
    [SerializeField]
    private RectTransform pagesParent;

    [SerializeField]
    private Toggle paginationPrefab;
    [SerializeField]
    private RectTransform paginationParent;

    [SerializeField]
    private HorizontalScrollSnap horizontalScroll;

    [Title("Buttons")]
    [SerializeField]
    private ButtonBase nextPageBtn;
    [SerializeField]
    private ButtonBase prevPageBtn;
    [SerializeField]
    private ButtonBase closeBtn;

    #endregion

    #region Propeties

    public TutorialPage PagePrefab { get => pagePrefab; }
    public RectTransform PagesParent { get => pagesParent; }
    public Toggle PaginationPrefab { get => paginationPrefab; }
    public RectTransform PaginationParent { get => paginationParent; }

    private TutorialPopUpModel CurrentModel
    {
        get;
        set;
    }

    public HorizontalScrollSnap HorizontalScroll {
        get => horizontalScroll; 
        private set => horizontalScroll = value; 
    }
    public ButtonBase NextPageBtn { get => nextPageBtn; }
    public ButtonBase PrevPageBtn { get => prevPageBtn; }
    public ButtonBase CloseBtn { get => closeBtn; }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<TutorialPopUpModel>();
    }

    public void OnPageChanged()
    {
        if (HorizontalScroll.CurrentPage == CurrentModel.GetCurrentTutorials().Length - 1)
        {
            NextPageBtn.gameObject.SetActive(false);
            CloseBtn.gameObject.SetActive(true);
        }
        else
        {
            NextPageBtn.gameObject.SetActive(true);
            CloseBtn.gameObject.SetActive(false);
        }
    }

    public void RefreshView()
    {
        TutorialElement[] tutorialPages = CurrentModel.GetCurrentTutorials();
        if(tutorialPages != null && tutorialPages.Length > 0)
        {
            CreatePaginationBullets(tutorialPages.Length);

            for(int i =0; i < tutorialPages.Length; i++)
            {
                CreateTutorialPage(tutorialPages[i]);
            }

            // Ustawienie na pierwsza strone tutoriala.
            HorizontalScroll.ChangePage(0);
        }
    }

    private void CreateTutorialPage(TutorialElement element)
    {
        TutorialPage tutorialElement = Instantiate(PagePrefab);
        tutorialElement.SetInfo(element);

        HorizontalScroll.AddChild(tutorialElement.gameObject);
    }

    private void CreatePaginationBullets(int ammount)
    {
        for(int i =0; i < ammount; i++)
        {
            Toggle bullet = Instantiate(PaginationPrefab);
            bullet.transform.ResetParent(PaginationParent);
        }
    }

    #endregion

    #region Enums



    #endregion
}
