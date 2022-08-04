using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMaker : MonoBehaviour
{
    [SerializeField] float deadTime;
    [SerializeField] float freq;
    [SerializeField] Vector2 dir;
    [SerializeField] GameObject ball;

    IEnumerator MakeBall()
    {
        while (true)
        {
            yield return new WaitForSeconds(freq);
            GameObject b = Instantiate(ball, transform);
            b.transform.parent = null;
            Rigidbody2D rb;
            if (b.TryGetComponent<Rigidbody2D>(out rb))
            {
                rb.AddForce(dir, ForceMode2D.Impulse);
            }
            Destroy(b, deadTime);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeBall());
    }
}
