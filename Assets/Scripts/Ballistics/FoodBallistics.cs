using System.Collections;
using UnityEngine;

public class FoodBallistics : MonoBehaviour
{
    [SerializeField] private Tray _tray;
    [SerializeField] private Player _player;
    [SerializeField] private AnimationCurve _animationCurveYPosition;
    [SerializeField] private float _heightToPlayer;
    [SerializeField] private float _heightToFan;
    [SerializeField] private float _duration;

    private float _runningTime;
    private Food _foodBullet;

    private void OnEnable()
    {
        _player.SteppendOnButton += ThrowFoodAtPlayer;
        _player.ExitedButton += DestroyJumper;
        _tray.ThrewFoodAtFan += ThrowFoodAtFan;
    }

    private void OnDisable()
    {
        _player.SteppendOnButton -= ThrowFoodAtPlayer;
        _player.ExitedButton -= DestroyJumper;
        _tray.ThrewFoodAtFan -= ThrowFoodAtFan;
    }

    private void ThrowFoodAtPlayer(FoodLoading foodLoading)
    {
        if (_foodBullet == null)
        {
            _foodBullet = Instantiate(foodLoading.FoodPrefab, foodLoading.FoodPointSpawn.transform.position,
                Quaternion.identity);
            _foodBullet.PlayAnimation(_foodBullet);
            StartCoroutine(AnimationByTime(_foodBullet, _player.TargetFoodJump, foodLoading.Duration, _heightToPlayer));
        }
    }

    private void ThrowFoodAtFan(Food food, Fan fan)
    {
        if (_foodBullet == null)
        {
            _foodBullet = Instantiate(food, _player.TargetFoodJump.transform.position, Quaternion.identity);
            StartCoroutine(AnimationByTime(_foodBullet, fan.EatingPoint, _duration, _heightToFan));
        }
    }

    private void DestroyJumper()
    {
        _runningTime = 0;

        if (_foodBullet != null)
        {
            Destroy(_foodBullet.gameObject);
        }
    }

    private IEnumerator AnimationByTime(Food jumper, Transform target, float duration, float heightTo)
    {
        var targetPosition = target.position;

        while (_runningTime < duration)
        {
            if (jumper != null)
            {
                _runningTime += Time.deltaTime;
                float normalizeRunningTime = _runningTime / duration;
                float heightAnimationCurve = _animationCurveYPosition.Evaluate(normalizeRunningTime);
                float height = Mathf.Lerp(0f, heightTo, heightAnimationCurve);
                jumper.transform.position =
                    Vector3.Lerp(jumper.transform.position, targetPosition, normalizeRunningTime) +
                    new Vector3(0, height, 0);
            }

            yield return null;
        }

        DestroyJumper();
    }
}