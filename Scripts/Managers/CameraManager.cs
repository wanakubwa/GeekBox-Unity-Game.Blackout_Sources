using UnityEngine;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class CameraManager : SingletonScenarioSaveableManager<CameraManager, CameraManagerMemento>
{
    #region Fields

    [SerializeField]
    private int defaultCameraDistance = 40;
    [SerializeField]
    private int maxCameraDistance = 55;
    [SerializeField]
    private int minCameraDistance = 30;
    [SerializeField]
    private float cameraMoveSpeedFactor = 0.5f;
    [SerializeField]
    private Vector2 cameraBounds;

    [Title("Animations")]
    [SerializeField]
    private float cameraZoomAnimationDurationS = 0.15f;
    [SerializeField]
    private float cameraMoveAnimationDurationS = 0.1f;

    #endregion

    #region Propeties

    public Camera CurrentSceneCamera
    {
        get;
        set;
    }

    public int DefaultCameraDistance { 
        get => defaultCameraDistance;
        private set => defaultCameraDistance = value;
    }

    public int MaxCameraDistance { 
        get => maxCameraDistance; 
        private set => maxCameraDistance = value; 
    }

    public int MinCameraDistance {
        get => minCameraDistance; 
        private set => minCameraDistance = value; 
    }

    public float CameraZoomAnimationDurationS { 
        get => cameraZoomAnimationDurationS; 
        private set => cameraZoomAnimationDurationS = value; 
    }

    private Vector2 LastMouseClickPosition
    {
        get;
        set;
    } = Vector2.zero;

    public float CameraMoveSpeedFactor { 
        get => cameraMoveSpeedFactor; 
        private set => cameraMoveSpeedFactor = value; 
    }

    public float CameraMoveAnimationDurationS { 
        get => cameraMoveAnimationDurationS; 
        private set => cameraMoveAnimationDurationS = value; 
    }

    public Vector2 CameraBounds { 
        get => cameraBounds; 
        private set => cameraBounds = value; 
    }

    public bool IsCameraMoveEnabled
    {
        get;
        set;
    } = true;

    #endregion

    #region Methods

    public void SetIsCameraMove(bool isEnabled)
    {
        IsCameraMoveEnabled = isEnabled;
    }

    public void SetCenterCamera()
    {
        Camera.main.transform.position = new Vector3(0f, 0f, Camera.main.transform.position.z);
    }

    public override void CreateNewScenario()
    {
        SetCenterCamera();
    }

    public override void LoadManager(CameraManagerMemento memento)
    {
        //todo;
    }

    public void CameraZoomByFactor(float value)
    {
        float newSize = Mathf.Clamp(CurrentSceneCamera.orthographicSize - value, MinCameraDistance, MaxCameraDistance);
        CurrentSceneCamera.DOOrthoSize(newSize, CameraZoomAnimationDurationS);
    }

    public void SetCameraZoom(float value)
    {
        float newSize = Mathf.Clamp(value, MinCameraDistance, MaxCameraDistance);
        CurrentSceneCamera.DOOrthoSize(newSize, CameraZoomAnimationDurationS);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        InputManager inputManager = InputManager.Instance;
        inputManager.OnMouseScrollChanged += OnMouseScrollHandler;
        inputManager.OnMouseLeftHold += OnMouseLeftDrag;
        inputManager.OnMouseLeftUp += OnMouseLeftDragEnd;
        inputManager.OnPinchOpenGesture += OnPinchOpenGestureHandle;
        inputManager.OnPinchCloseGesture += OnPinchCloseGestureHandle;
        inputManager.OnOneOfFingersUp += ResetDragPosition;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        InputManager inputManager = InputManager.Instance;
        if(inputManager != null)
        {
            inputManager.OnMouseScrollChanged -= OnMouseScrollHandler;
            inputManager.OnMouseLeftHold -= OnMouseLeftDrag;
            inputManager.OnMouseLeftUp -= OnMouseLeftDragEnd;
            inputManager.OnPinchOpenGesture -= OnPinchOpenGestureHandle;
            inputManager.OnPinchCloseGesture -= OnPinchCloseGestureHandle;
            inputManager.OnOneOfFingersUp -= ResetDragPosition;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        CurrentSceneCamera = Camera.main;
        SetCameraDefaultDistance();
    }

    protected override void OnSceneSwitched(Scene scene)
    {
        base.OnSceneSwitched(scene);

        CurrentSceneCamera = Camera.main;
        SetCameraDefaultDistance();
    }

    private void SetCameraDefaultDistance()
    {
        CurrentSceneCamera.orthographicSize = DefaultCameraDistance;
    }

    private void MoveCameraByVector(Vector2 transitionVector)
    {
        transitionVector = transitionVector * CameraMoveSpeedFactor;

        //CurrentSceneCamera.transform.Translate(transitionVector);
        CurrentSceneCamera.transform.DOBlendableMoveBy(transitionVector, CameraMoveAnimationDurationS);
        CheckCameraInBoundsPosition();
    }

    private void CheckCameraInBoundsPosition()
    {
        // Strona prawa: x > 0;
        bool isRightBound = CurrentSceneCamera.transform.position.x > CameraBounds.x;

        // Strona lewa: x < 0;
        bool isLeftBound = CurrentSceneCamera.transform.position.x < -CameraBounds.x;

        // Strona gorna: y > 0;
        bool isUpBound = CurrentSceneCamera.transform.position.y > CameraBounds.y;

        // Strona dolna: y < 0;
        bool isDwnBound = CurrentSceneCamera.transform.position.y < -CameraBounds.y;


        if (isRightBound == true)
        {
            CurrentSceneCamera.transform.position = new Vector3(CameraBounds.x, CurrentSceneCamera.transform.position.y, CurrentSceneCamera.transform.position.z);
        }
        else if (isLeftBound == true)
        {
            CurrentSceneCamera.transform.position = new Vector3(-CameraBounds.x, CurrentSceneCamera.transform.position.y, CurrentSceneCamera.transform.position.z);
        }

        if (isUpBound == true)
        {
            CurrentSceneCamera.transform.position = new Vector3(CurrentSceneCamera.transform.position.x, CameraBounds.y, CurrentSceneCamera.transform.position.z);
        }
        else if (isDwnBound == true)
        {
            CurrentSceneCamera.transform.position = new Vector3(CurrentSceneCamera.transform.position.x, -CameraBounds.y, CurrentSceneCamera.transform.position.z);
        }
    }

    private void OnMouseScrollHandler(float changedValue)
    {
        if (changedValue != 0)
        {
            CameraZoomByFactor(changedValue);
        }
    }

    private void OnPinchCloseGestureHandle(float factor)
    {
        CameraZoomByFactor(factor);
    }

    private void OnPinchOpenGestureHandle(float factor)
    {
        CameraZoomByFactor(factor);
    }

    private void OnMouseLeftDrag(Vector2 screenPosition)
    {
        if(IsCameraMoveEnabled == false || SelectingManager.Instance.IsPointerOverUI(screenPosition) == true)
        {
            return;
        }

        if (LastMouseClickPosition != Vector2.zero)
        {
            Vector2 delta = LastMouseClickPosition - screenPosition;
            MoveCameraByVector(delta);
        }

        LastMouseClickPosition = screenPosition;
    }

    private void OnMouseLeftDragEnd(Vector2 position)
    {
        LastMouseClickPosition = Vector2.zero;
    }

    private void ResetDragPosition()
    {
        LastMouseClickPosition = Vector2.zero;
    }

    #endregion

    #region Enums



    #endregion
}
