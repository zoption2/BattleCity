using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsLife : MonoBehaviour
{
    private TextMeshPro text;


    private void Awake()
    {
        text = transform.Find("Canvas/Text").GetComponent<TextMeshPro>();
    }
    private void OnEnable()
    {
        StartCoroutine(move());
    }

    private IEnumerator move()
    {
        float speed = 3f;
        const float MAX_TIME = 1.5f;
        float time = MAX_TIME;
        float dissapereSpeed = 3f;
        Color alpha = text.color;
        alpha.a = 1f;
        text.color = alpha;
        Vector3 moveVector = new Vector3(0.3f, 0, 1f) * speed;

        while (time > 0)
        {
            transform.position += moveVector * Time.deltaTime;
            moveVector -= moveVector * 2f * Time.deltaTime;
            time -= Time.deltaTime;

            if (time > MAX_TIME * 0.5f)
            {
                transform.localScale += Time.deltaTime * (Vector3.one/2);
            }
            else
            {
                transform.localScale -= Time.deltaTime * (Vector3.one/2);
            }

            if (time < 0.3f)
            {
                alpha.a -= Time.deltaTime * dissapereSpeed;
                text.color = alpha;
            }

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
