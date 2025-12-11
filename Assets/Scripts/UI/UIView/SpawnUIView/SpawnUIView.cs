using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpawnUIView : UIView
{
    [Header("UI References")]
    [SerializeField] private Transform uiRoot;
    [SerializeField] private GameObject uiPrefab;

    private SpawnUIPresenter presenter;

    protected override void Awake()
    {
        base.Awake();

        // Presenter ¿¬°á
        presenter = new SpawnUIPresenter(this);

        Instantiate(uiPrefab, uiRoot);
    }

    protected override void OnShow()
    {
        base.OnShow();
    }

    protected override void OnHide()
    {
        base.OnHide();
    }

    private void OnClickClose()
    {
        UIManager.Instance.Close<SpawnUIView>();
    }

    public void RenderUI()
    {
       
    }
}
