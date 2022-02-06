using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    private bool isFocused = false;
    private Transform player;
    public Transform interactionTransform;
    private bool hasInteracted;

    public virtual void interact()
    {
    }

    public void onFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void onDefocused()
    {
        isFocused = false;
        player = null;
        hasInteracted = false;
    }

    void Update()
    {
        if (isFocused && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                interact();
                
                hasInteracted = true;
            }
        }
    }

    void OnDrawGizmosSelected() { 
    
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }



}
