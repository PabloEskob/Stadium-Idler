using System;
using UnityEngine;
using UnityEngine.UI;

public class FoodLoading : MonoBehaviour
{
    [SerializeField] private FoodPointSpawn _spawnPoint;
    [SerializeField] private Image _image;
    [SerializeField] private float _duration;
    [SerializeField] private Player _player;
    [SerializeField] private Tray tray;
    [SerializeField] private Food _foodPrefab;

    private float _runningTime;
    private float _normalizeRunningTime;

    public Food FoodPrefab => _foodPrefab;
    public FoodPointSpawn FoodPointSpawn => _spawnPoint;
    public float Duration => _duration;
    
    private void OnEnable()
    {
        _player.SteppendOnButton += TurnOnIndicator;
        _player.ExitedButton += StopTurnIndicator;
    }

    private void OnDisable()
    {
        _player.SteppendOnButton -= TurnOnIndicator;
        _player.ExitedButton -= StopTurnIndicator;
    }

    private void TurnOnIndicator(FoodLoading foodLoading)
    {
        if (foodLoading._runningTime < _duration)
        {
            foodLoading._runningTime += Time.deltaTime / 2;
            _normalizeRunningTime = foodLoading._runningTime / foodLoading._duration;
            foodLoading._image.fillAmount = Mathf.Lerp(0, 1, _normalizeRunningTime);

            if (Math.Abs(foodLoading._image.fillAmount - 1) == 0)
            {
                PutOn(foodLoading._foodPrefab);
                foodLoading._runningTime = 0;
            }
        }
    }

    private void StopTurnIndicator()
    {
        _image.fillAmount = 0;
        _runningTime = 0;
    }

    private void PutOn(Food food)
    {
        tray.AddFood(food);
    }
}