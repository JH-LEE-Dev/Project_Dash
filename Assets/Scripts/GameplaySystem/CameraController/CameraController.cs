using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject InitialFocusPoint;
    private CinemachineCamera vcam;

    [SerializeField] private float zoomSpeed = 5f;     // ¡‹ ∫Ø»≠ º”µµ
    [SerializeField] private float minZoom = 3f;       // √÷º“ ¡‹
    [SerializeField] private float maxZoom = 10f;      // √÷¥Î ¡‹

    private float InitialOrthoSize;
    private float targetZoom;

    bool bZoom = false;

    private void Awake()
    {
        vcam = GetComponent<CinemachineCamera>();

        vcam.Follow = InitialFocusPoint.transform;
        InitialOrthoSize = vcam.Lens.OrthographicSize;
    }

    private void Update()
    {
        //SmoothZoom();
    }

    private void ZoomIn()
    {
        bZoom = true;
    }

    private void SmoothZoom()
    {
        vcam.Lens.OrthographicSize = Mathf.Lerp(vcam.Lens.OrthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }

    public void SetCameraTarget(Transform newTarget)
    {
        if (vcam == null)
            Debug.Log("vCam is null");

        vcam.Follow = newTarget;
    }
}