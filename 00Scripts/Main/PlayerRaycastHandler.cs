using UnityEngine;

public class PlayerRaycastHandler : MonoBehaviour
{
    [SerializeField] private Transform topRaycastPoint;
    [SerializeField] private Transform bottomRaycastPoint;
    
    [SerializeField] private float distance = 1;
    [SerializeField] private float offsetX = 0.2f;
    
    public bool Check(float horizontal)
    {
        var hit = Physics2D.Raycast(transform.position + new Vector3(offsetX, 0), new Vector2(horizontal, 0), distance, LayerMask.GetMask("Object"));
        
        // 있으면
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position + new Vector3(offsetX, 0), new Vector2(horizontal, 0) * distance, Color.blue);
            Debug.Log(hit.collider.name);

            return false;
        }
        
        Debug.DrawRay(transform.position + new Vector3(offsetX, 0), new Vector2(horizontal, 0) * distance, Color.red);

        return true;
    }
    
    public bool Check(float horizontal, float direction)
    {
        var topResult = false;
        var bottomResult = false;
        
        var hit = Physics2D.Raycast(topRaycastPoint.position, new Vector2(horizontal, 0), distance, LayerMask.GetMask("Object"));
        
        if (hit.collider != null)
        {
            Debug.DrawRay(topRaycastPoint.position, new Vector2(horizontal, 0) * distance, Color.blue);
            Debug.Log(hit.collider.name);

            topResult = false;
        }
        else
        {
            Debug.DrawRay(topRaycastPoint.position, new Vector2(horizontal, 0) * distance, Color.red);
            topResult = true;
            
        }
        
        hit = Physics2D.Raycast(bottomRaycastPoint.position, new Vector2(horizontal, 0), distance, LayerMask.GetMask("Object"));
            
        if (hit.collider != null)
        {
            Debug.DrawRay(bottomRaycastPoint.position, new Vector2(horizontal, 0) * distance, Color.blue);
            Debug.Log(hit.collider.name);

            bottomResult = false;
        }
        else
        {
            Debug.DrawRay(bottomRaycastPoint.position, new Vector2(horizontal, 0) * distance, Color.red);
                
            bottomResult = true;
        }
        
        return topResult && bottomResult;
    }
}
