using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class Money : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Animator _animator;
    private static readonly int Take = Animator.StringToHash("Take");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void StartParticle(ParticleSystem particleSystem)
    {
        particleSystem.Play();
    }

    public void TakeMoneyAnimation()
    {
       _boxCollider.enabled = false;
        _animator.SetTrigger(Take);
    }

    public void PutAway()
    {
       _boxCollider.enabled = true;
        gameObject.SetActive(false);
    }
}
