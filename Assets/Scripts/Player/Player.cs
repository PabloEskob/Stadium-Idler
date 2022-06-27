using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _targetFoodJump;

    private FoodLoading _foodLoading;
    private Fan _fan;

    public Transform TargetFoodJump => _targetFoodJump;

    public event UnityAction<FoodLoading> SteppendOnButton;
    public event UnityAction ExitedButton;
    public event UnityAction<Fan> GaveFood;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<FoodLoading>())
        {
            _foodLoading = hit.gameObject.GetComponent<FoodLoading>();
            SteppendOnButton?.Invoke(_foodLoading);
        }

        if (hit.gameObject.GetComponent<Ground>())
        {
            ExitedButton?.Invoke();
        }

        if (hit.gameObject.GetComponent<Fan>())
        {
            _fan = hit.gameObject.GetComponent<Fan>();
            GaveFood?.Invoke(_fan);
        }

        if (hit.gameObject.GetComponent<Money>())
        {
            var money = hit.gameObject.GetComponent<Money>();
            money.TakeMoneyAnimation();
        }
    }
}