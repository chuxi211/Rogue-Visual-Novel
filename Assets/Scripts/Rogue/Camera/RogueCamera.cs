using System.Collections;
using UnityEngine;
//如果按照Running，Actor，GameObject三层模型，是应该尽可能避免直接让运行时对象直接继承MonoBehaviour的
//优点是简单
//目前CameraController只有这一个引用，所以就随便写
public class RogueCamera : MonoBehaviour
{
    private int MovingSpeed = 0;
    private Vector3 Offset = new Vector3(10, 0, -10);
    private Coroutine MovingCoroutine;
    public void InitAttribute(int movingSpeed, Vector3 offset)
    {
        MovingSpeed = movingSpeed;
        Offset = offset;
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    public void StartMoveTo(Vector3 target)
    {
        if (MovingCoroutine != null)
        {
            StopCoroutine(MovingCoroutine);
        }
        MovingCoroutine = StartCoroutine(MoveTo(target));
    }
    public void ResetPosition()
    {
        transform.position = Offset;
    }
    private IEnumerator MoveTo(Vector3 target)
    {
        Vector3 destination = target + Offset;
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, MovingSpeed * Time.deltaTime);
            yield return null;
        }
        MovingCoroutine = null;
    }
}
