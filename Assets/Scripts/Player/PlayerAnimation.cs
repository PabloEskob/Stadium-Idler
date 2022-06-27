using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Movement))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Movement _movement;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _movement.MoveDirection.magnitude);
    }
}