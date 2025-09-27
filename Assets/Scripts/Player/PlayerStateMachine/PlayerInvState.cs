using UnityEngine;

public class PlayerInvState : PlayerBaseState
{

    public PlayerInvState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }
    public override void EnterState()
    {

    }
    HighlightInteractableOutline outlineScript;
    GameObject draggedObject;
    public override void UpdateState()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // If we're hovering a new object, exit the old one
                HighlightInteractableOutline newOutline = hit.collider.gameObject.GetComponent<HighlightInteractableOutline>();
                if (outlineScript == newOutline) return;
                if (outlineScript != null) outlineScript.Exit();

                outlineScript = newOutline;

                if (outlineScript != null) outlineScript.Enter();
            }
            else
            {
                // Not hovering anything, exit previous outline
                if (outlineScript == null) return;

                outlineScript.Exit();
                outlineScript = null;
            }
        }
        else
        {
            DetectWhenExit();
        }
    }
    public override void ExitState()
    {
        Debug.Log("Left Inv State");
        _context.UpdateCurrentState(PlayerStateManager.State.Idle);
    }

    public void DetectWhenExit()
    {
        ExitState();
    }
}
