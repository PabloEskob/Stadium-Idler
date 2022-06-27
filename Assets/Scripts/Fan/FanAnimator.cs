using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FanAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _minWaitChangeAnimation;
    [SerializeField] private float _maxWaitChangeAnimation;

    private IEnumerator _coroutine;
    private static readonly int Value = Animator.StringToHash("Value");

    private void Start()
    {
        _coroutine = ChangeAnimation();
        StartCoroutine(_coroutine);
    }

    private IEnumerator ChangeAnimation()
    {
        while (true)
        {
            var randomWaitTime = Random.Range(_minWaitChangeAnimation, _maxWaitChangeAnimation);
            var waitForSeconds = new WaitForSeconds(randomWaitTime);
            var rand = Random.Range(0, 2);
            _animator.SetInteger(Value, rand);
            yield return waitForSeconds;
        }
    }
}