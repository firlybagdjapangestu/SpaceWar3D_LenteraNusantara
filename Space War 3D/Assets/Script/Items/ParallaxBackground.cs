using System.Collections;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float moveSpeed; // Kecepatan efek parallax
    public float resetZPosition;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.parent.position;
        StartCoroutine(MoveBackground());
    }

    private IEnumerator MoveBackground()
    {
        while (true)
        {
            Vector3 movement = Vector3.up * moveSpeed * Time.deltaTime;

            // Menggerakkan objek ke atas
            transform.Translate(movement);

            if (transform.position.z < resetZPosition)
            {
                // Reset posisi objek ke posisi awal
                transform.position = initialPosition;
            }

            yield return null;
        }
    }
}
