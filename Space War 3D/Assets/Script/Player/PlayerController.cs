using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDragging = false;
    public float minX; 
    public float maxX;  
    public float minZ; 
    public float maxZ;  

    private void Start()
    {
        StartCoroutine(DragObject());
    }

    private IEnumerator DragObject()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        isDragging = true;
                    }
                }
            }

            if (isDragging)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 newPosition = new Vector3(
                        Mathf.Clamp(hit.point.x, minX, maxX),
                        transform.position.y,
                        Mathf.Clamp(hit.point.z, minZ, maxZ)
                    );
                    transform.position = newPosition;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    isDragging = false;
                }
            }

            yield return null;
        }
    }
}
