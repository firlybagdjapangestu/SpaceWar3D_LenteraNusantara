using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBehavior : MonoBehaviour
{
    public float moveSpeedForward;
    public float moveSpeedBackward;
    public float forwardMoveTime; // Waktu bergerak ke depan sebelum bergerak ke belakang
    private float currentTime;

    public float randForwardMoveTime;
    public float randDirectionX;

    public float limitBackward;

    public void Active()
    {
        currentTime = 0f;
        randForwardMoveTime = Random.Range(0.3f, 1f);
        randDirectionX = Random.Range(0, 2) * 2 - 1;
        StartCoroutine(MoveExp());
    }
    private IEnumerator MoveExp()
    {
        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime <= randForwardMoveTime)
            {
                transform.position = new Vector3(transform.position.x + (moveSpeedForward * randDirectionX), transform.position.y,
                    transform.position.z + moveSpeedForward);
            }
            else
            {
                transform.Translate(Vector3.back * moveSpeedBackward * Time.deltaTime);
            }

            if (transform.position.z <= limitBackward)
            {
                gameObject.SetActive(false);
            }

            yield return null;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
