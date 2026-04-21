using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInteractor : MonoBehaviour
{
    public LayerMask interactableLayerMask;

    public TVInteraction currentHover;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayerMask))
        {
            TVInteraction tv = hit.collider.GetComponent<TVInteraction>();

            if (tv != currentHover)
            {
                if (currentHover != null)
                    currentHover.OnHoverExit();

                currentHover = tv;
                currentHover.OnHoverEnter();
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                tv.OnClick();
            }
        }

        else
        {
            if (currentHover != null)
            {
                currentHover.OnHoverExit();
                currentHover = null;
            }
        }
    }
}
