using UnityEngine;
using System.Collections;

public class cardAnimator : MonoBehaviour {

    //c1 cardModel;
    //GameObject card;
    public AnimationCurve fChange;
    public AnimationCurve mChange;
    public float duration = 0.5f;
    private void Awake()
    {
        //cardModel = GetComponent<c1>();
    }
    public void FlipCard()
    {
        StopCoroutine(Flip());
        StartCoroutine(Flip());
    }
    public void MoveCard(Vector3 destPosition,int temp,System.Action done)
    {
        StopCoroutine(move(destPosition,temp,done));
        StartCoroutine(move(destPosition, temp, done));
    }
    IEnumerator Flip()
    {
        float time = 0f;
        while (time < 1f)
        {
            float scale = fChange.Evaluate(time);
            time = time + Time.deltaTime / duration;
            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;

            

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator move(Vector3 destPosition, int temp,System.Action done)
    {

        float time = 0f;
        Vector3 cof = destPosition-transform.localPosition;
        Vector3 initial = transform.localPosition;
        while (time < 1f)
        {
            Vector3 scale = initial+ cof*mChange.Evaluate(time);
            time = time + Time.deltaTime / duration;

            Vector3 localPos = transform.localPosition;
            localPos = scale;
            //localPos.x = scale;
           // localPos.y = scale;
            transform.localPosition = localPos;


            yield return new WaitForFixedUpdate();
        }
        done();
    }
}
