using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Fan : MonoBehaviour
{
    [SerializeField] private Transform _foodDeliveryPoint;
    [SerializeField] private SpriteRenderer _foodImage;
    [SerializeField] private Food[] _foods;
    [SerializeField] private float _minNumberSecondsToWait;
    [SerializeField] private float _maxNumberSecondsToWait;
    [SerializeField] private float _waitingForFoodDelivery;
    [SerializeField] private Money _money;

    private Food _food;
    private IEnumerator _coroutine;

    public Transform EatingPoint => _foodDeliveryPoint;
    public Food Food => _food;

    public event UnityAction<Money> GaveAnAward;

    private void Start()
    {
        _coroutine = FoodSelection();
        StartCoroutine(_coroutine);
    }

    private IEnumerator FoodSelection()
    {
        while (_food == null)
        {
            var waitFoodSelection = new WaitForSeconds(Random.Range(_minNumberSecondsToWait, _maxNumberSecondsToWait));
            yield return waitFoodSelection;

            _food = _foods[Random.Range(0, _foods.Length)];
            ImageChange(_food, _foodImage);

            var waitFood = new WaitForSeconds(_waitingForFoodDelivery);
            yield return waitFood;

            _food = null;
            ImageChange(_food, _foodImage);
        }
    }

    public void TakeFood()
    {
        _food = null;
        _foodImage.enabled = false;
        StopCoroutine(_coroutine);
        GaveAnAward?.Invoke(_money);
        StartCoroutine(_coroutine);
    }

    private void ImageChange(Food food, SpriteRenderer image)
    {
        if (food != null)
        {
            image.sprite = food.Icon;
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
    }
}