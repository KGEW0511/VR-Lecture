using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class ReturnToTarget : MonoBehaviour
{
    public Transform target;

    public float duration = 1f;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    public UnityEvent OnCompleted;

    public void Call()
    {
        if (!gameObject.activeInHierarchy)
            return;

        StopAllCoroutines();
        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        if(target == null)
        {
            yield break;
        }

        var beginTime = Time.time;

        while (true)
        {
            var t = (Time.time - beginTime) / duration;
            if (t >= 1f)
                break;

            t = curve.Evaluate(t); //���µ� �ɸ��� �ð� ����

            transform.position = Vector3.Lerp(transform.position, target.position, t); //��ǥ�� ���� ������Ʈ�� �̵�

            yield return null;
        }

        transform.position = target.position;

        OnCompleted?.Invoke();
    }
}
