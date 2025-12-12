using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager
{
    private InputReader inputReader;
    private CameraController cameraController;
    private Entity selectedObject_Clicked;
    private Entity selectedObject;

    public void Initialize(InputReader inputReader,CameraController cameraController)
    {
        this.inputReader = inputReader;
        this.cameraController = cameraController;

        if (inputReader == null)
        {
            Debug.Log("inputReader is null -> SelectManager::Initialize");
            return;
        }
    }

    public void Release()
    {

    }

    public void FindSelectedObject(Vector2 screenPos, bool bClicked)
    {
        Vector3 sp = screenPos;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(sp);

        Collider2D col = Physics2D.OverlapPoint(worldPos);

        if (col == null)
            return;

        Entity entity = col.GetComponent<Entity>();

        if (entity != null)
        {
            if (bClicked == false)
                selectedObject_Clicked = entity;
            else 
                selectedObject = entity;
        }
    }

    public void HandleLeftClick(Vector2 screenPos)
    {
        FindSelectedObject(screenPos,false);

        if(selectedObject_Clicked != null)
        {
            if(selectedObject != null && selectedObject_Clicked != selectedObject)
            {
                selectedObject.SetSelected(false);
            }

            if(selectedObject_Clicked.IsSelected())
            {
                selectedObject_Clicked.SetCharging(true);
            }
        }
    }

    public void HandleLeftClickReleased(Vector2 screenPos)
    {
        FindSelectedObject(screenPos,true);

        if (selectedObject == null)
        {
            selectedObject_Clicked = null;
            selectedObject = null;
            return;
        }

        if (selectedObject_Clicked == selectedObject)
        {
            if (selectedObject.IsSelected())
            {
                selectedObject.SetSelected(false);
            }
            else
            {
                selectedObject.SetSelected(true);
                cameraController.SetCameraTarget(selectedObject.transform);
            }
        }
        else
        {
            selectedObject_Clicked = null;
            selectedObject = null;
        }
    }

    public Entity GetSelectedObject()
    {
        return selectedObject;
    }

    public void ResetSelectedUnit()
    {
        selectedObject = null;
        selectedObject_Clicked = null;
    }
}
