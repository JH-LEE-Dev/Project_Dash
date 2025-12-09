using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager
{
    private InputReader inputReader;
    private Entity selectedObject;

    public void Initialize(InputReader inputReader)
    {
        this.inputReader = inputReader;
        inputReader.LeftClickReleased += HandleLeftClickReleased;
    }

    public void HandleLeftClick(Vector2 screenPos)
    {
        Vector3 sp = screenPos;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(sp);

        Collider2D col = Physics2D.OverlapPoint(worldPos);

        if (col == null)
        {
            if (selectedObject != null)
            {
                selectedObject.HideOutLine();
            }

            selectedObject = null;

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

            selectedObject = null;
        }
    }

    private void HandleLeftClickReleased()
    {
        if (selectedObject != null)
        {
            selectedObject.SetSelected(false);
        }
    }

    public Entity GetSelectedObject()
    {
        return selectedObject;
    }
}
