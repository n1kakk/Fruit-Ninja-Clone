using UnityEngine;
using System.Collections;

public class Rotation: MonoBehaviour
{
    public Quaternion[] targetRotations;
    public float rotationSpeed = 0.5f; 
    private int currentTargetIndex = 0;

    void Start()
    {
        targetRotations = new Quaternion[]
        {
            new Quaternion(0.402643263f, 0.913531125f, 0.00806015637f, -0.0572219603f),
            new Quaternion(0.0178210903f, 0.079355374f, -0.402329683f, 0.91187501f)
        };

        StartCoroutine(RotateContinuously());
    }

       private IEnumerator RotateContinuously()
    {
        while (true)
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = targetRotations[currentTargetIndex];

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * rotationSpeed;
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
                yield return null;
            }

            currentTargetIndex = (currentTargetIndex + 1) % targetRotations.Length;
        }
    }
}
