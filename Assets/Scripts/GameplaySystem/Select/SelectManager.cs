using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager
{
    private InputReader inputReader;
    private Entity selectedObject;

    public void Initialize(InputReader inputReader)
    {
        this.inputReader = inputReader;

        if (inputReader == null)
        {
            Debug.Log("inputReader is null -> SelectManager::Initialize");
            return;
        }

        inputReader.LeftClickReleasedEvent += HandleLeftClickReleased;
    }

    public void Release()
    {
        inputReader.LeftClickReleasedEvent -= HandleLeftClickReleased;
    }

    public void HandleLeftClick(Vector2 screenPos)
    {
        selectedObject = null;

        Vector3 sp = screenPos;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(sp);

        Collider2D col = Physics2D.OverlapPoint(worldPos);

        if (col == null)
        {
            if (selectedObject != null)
            {
                selectedObject.HideOutLine();
            }

            return;
        }

        Entity entity = col.GetComponent<Entity>();

        if (entity != null)
        {
            selectedObject = entity;
            selectedObject.ShowOutLine();
        }
        else
        {
            if (selectedObject != null)
            {
                selectedObject.HideOutLine();
            }
        }
    }

    private void HandleLeftClickReleased()
    {
        if (selectedObject == null)
        {
            Debug.Log("selectedObject is null -> SelectManager::HandleLeftCilckReleased");
            return;
        }

        selectedObject.SetSelected(false);
    }

    public Entity GetSelectedObject()
    {
        return selectedObject;
    }
}
