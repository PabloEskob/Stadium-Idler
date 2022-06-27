using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Animator _animator;

    public Sprite Icon => _icon;
    
    public void PlayAnimation(Food food)
    {
        _animator.Play(food.GetComponent<Drink>() ? "TorsionCoffe" : "Torsion");
    }
}