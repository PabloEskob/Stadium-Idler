using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Fan))]
public class SpawnMoney : MonoBehaviour
{
    [SerializeField] private float _waitingTimeMoney;
    [SerializeField] private ParticleSystem _particleSystem;

    private IEnumerator _coroutine;
    private Fan _fan;

    private void Awake()
    {
        _fan = GetComponent<Fan>();
    }

    private void OnEnable()
    {
        _fan.GaveAnAward += Spawn;
    }

    private void OnDisable()
    {
        _fan.GaveAnAward -= Spawn;
    }

    private void Spawn(Money money)
    {
        _particleSystem.gameObject.SetActive(true);
        _coroutine = SpawnRoutine(_waitingTimeMoney, money);
        StartCoroutine(_coroutine);
    }

    private IEnumerator SpawnRoutine(float waitTime, Money money)
    {
        var waitForSecond = new WaitForSeconds(waitTime);
        yield return waitForSecond;
        money.gameObject.SetActive(true);
        money.StartParticle(_particleSystem);
    }
}