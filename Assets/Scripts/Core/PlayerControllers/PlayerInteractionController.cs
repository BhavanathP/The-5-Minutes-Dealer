using UnityEngine;
using System.Collections.Generic;
using System;


public class PlayerInteractionController : MonoBehaviour
{
    public GameObject interactIcon;
    private Vector2 lastMoveDir = Vector2.down;

    private void OnEnable()
    {
        InputManager.Instance.OnInteract += OnInteract;
        InputManager.Instance.OnInteractCanceled += OnInteractCanceled;
        InputManager.Instance.OnMove += UpdateFacing;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInteract -= OnInteract;
        InputManager.Instance.OnInteractCanceled -= OnInteractCanceled;
        InputManager.Instance.OnMove -= UpdateFacing;
    }

    void Update()
    {
        interactIcon.SetActive(nearbyInteractables.Count > 0);
    }

    private void UpdateFacing(Vector2 moveInput)
    {
        if (moveInput.sqrMagnitude > 0.1f)
        {
            lastMoveDir = moveInput.normalized;
        }
    }
    private void OnInteract()
    {
        if (nearbyInteractables.Count == 0) return;

        currentInteractable = nearbyInteractables[^1];
        InputManager.Instance.controls.Player.Move.Disable();
        currentInteractable.Interact();
    }
    private IInteractable currentInteractable;
    private void OnInteractCanceled()
    {
        if (currentInteractable == null) return;

        InputManager.Instance.controls.Player.Move.Enable();
        currentInteractable.StopInteract();
        currentInteractable = null;
    }

    private List<IInteractable> nearbyInteractables = new List<IInteractable>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactables"))
        {
            if (collision.TryGetComponent<IInteractable>(out var interactable) && !nearbyInteractables.Contains(interactable))
            {
                nearbyInteractables.Add(interactable);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactables"))
        {
            if (collision.TryGetComponent<IInteractable>(out var interactable))
            {
                nearbyInteractables.Remove(interactable);
            }
        }
    }
}
