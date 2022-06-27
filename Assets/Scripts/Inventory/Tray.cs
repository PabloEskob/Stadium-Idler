using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Tray : MonoBehaviour
{
    [SerializeField] private int _maxCountFoods;
    [SerializeField] private Player _player;

    private List<Food> _foods = new List<Food>();

    public event UnityAction<Food> AddedToTray;
    public event UnityAction<Food> RemovedFromTray;
    public event UnityAction<Food, Fan> ThrewFoodAtFan;

    private void OnEnable()
    {
        _player.GaveFood += GetFood;
    }

    private void OnDisable()
    {
        _player.GaveFood -= GetFood;
    }

    public void AddFood(Food food)
    {
        if (_maxCountFoods > _foods.Count)
        {
            _foods.Add(food);
            AddedToTray?.Invoke(food);
        }
    }

    private void GetFood(Fan fan)
    {
        if (fan.Food != null)
        {
            var food = _foods.FirstOrDefault(f => f.Icon == fan.Food.Icon);

            if (food != null)
            {
                RemovedFromTray?.Invoke(food);
                ThrewFoodAtFan?.Invoke(food, fan);
                _foods.Remove(food);
                fan.TakeFood();
            }
        }
    }
}