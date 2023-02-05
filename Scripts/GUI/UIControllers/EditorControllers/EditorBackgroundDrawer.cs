using UnityEngine;

public class EditorBackgroundDrawer : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private LineRenderer borderLinePrefab;
    [SerializeField]
    private LineRenderer graduationLinePrefab;

    [SerializeField]
    private int graduateStep = 10;

    #endregion

    #region Propeties

    public LineRenderer BorderLinePrefab { 
        get => borderLinePrefab; 
    }

    public LineRenderer GraduationLinePrefab { 
        get => graduationLinePrefab; 
    }

    public int GraduateStep { 
        get => graduateStep; 
        private set => graduateStep = value; 
    }

    #endregion

    #region Methods

    private void Start()
    {
        DrawGraduationLines();
        DrawBorderLines();
    }

    private void DrawBorderLines()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;

        float camHalfHeight = CameraManager.Instance.DefaultCameraDistance;
        float camHalfWidth = screenAspect * camHalfHeight;

        Vector3 upLeftCorner = new Vector3(-camHalfWidth, camHalfHeight, 1f);
        Vector3 upRightCorner = new Vector3(camHalfWidth, camHalfHeight, 1f);
        Vector3 dwnLeftCorner = new Vector3(-camHalfWidth, -camHalfHeight, 1f);
        Vector3 dwnRighttCorner = new Vector3(camHalfWidth, -camHalfHeight, 1f);

        CreateLine(BorderLinePrefab, upLeftCorner, upRightCorner);
        CreateLine(BorderLinePrefab, upLeftCorner, dwnLeftCorner);
        CreateLine(BorderLinePrefab, dwnLeftCorner, dwnRighttCorner);
        CreateLine(BorderLinePrefab, dwnRighttCorner, upRightCorner);
    }

    private void DrawGraduationLines()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;

        CameraManager cameraManager = CameraManager.Instance;
        float camHalfHeight = cameraManager.DefaultCameraDistance;
        float camHalfWidth = screenAspect * camHalfHeight;

        float halfMapWidth = camHalfWidth + cameraManager.CameraBounds.x;
        float halfMapHeight = camHalfHeight + cameraManager.CameraBounds.y;

        // Budowanie linni poziomych od 0 w gore oraz w dol.
        for(int i =0; i < halfMapHeight; i++)
        {
            if(i % GraduateStep == 0)
            {
                CreateLine(GraduationLinePrefab, new Vector3(-halfMapWidth, i), new Vector3(halfMapWidth, i));
                CreateLine(GraduationLinePrefab, new Vector3(-halfMapWidth, -i), new Vector3(halfMapWidth, -i));
            }
        }

        // Budowanie linni pionowych od 0 w lewo oraz w prawo.
        for (int i = 0; i < halfMapWidth; i++)
        {
            if (i % GraduateStep == 0)
            {
                CreateLine(GraduationLinePrefab, new Vector3(i, halfMapHeight), new Vector3(i, -halfMapHeight));
                CreateLine(GraduationLinePrefab, new Vector3(-i, halfMapHeight), new Vector3(-i, -halfMapHeight));
            }
        }
    }

    private void CreateLine(LineRenderer prefabReference, Vector3 firstPosition, Vector3 secondPosition)
    {
        LineRenderer newLine = Instantiate(prefabReference);
        newLine.transform.ResetParent(transform);

        newLine.SetPosition(0, firstPosition);
        newLine.SetPosition(1, secondPosition);
    }

    #endregion

    #region Enums



    #endregion
}
