using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodDisplay : MonoBehaviour
{
    [SerializeField] private Food[] _foods;
    [SerializeField] private Tray _tray;

    private Stack<Food> _stackFoods;

    private void OnEnable()
    {
        _tray.AddedToTray += EnableVisibillity;
        _tray.RemovedFromTray += DisableVisibillity;
    }

    private void OnDisable()
    {
        _tray.AddedToTray -= EnableVisibillity;
        _tray.RemovedFromTray -= DisableVisibillity;
    }

    private void Start()
    {
        _stackFoods = new Stack<Food>();

        foreach (var food in _foods)
        {
            _stackFoods.Push(food);
            food.gameObject.SetActive(false);
        }
    }

    private void EnableVisibillity(Food product)
    {
        Food food = _stackFoods.LastOrDefault(f => f.gameObject.activeSelf == false && f.Icon == product.Icon);
        if (food != null) food.gameObject.SetActive(true);
    }

    private void DisableVisibillity(Food product)
    {
        Food food = _stackFoods.FirstOrDefault(f => f.gameObject.activeSelf && f.Icon == product.Icon);
        if (food != null) food.gameObject.SetActive(false);
    }
}