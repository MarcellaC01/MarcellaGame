using UnityEngine;
using System.Collections;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathParticleSystem;
    public bool HasDied { get; private set; }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
    }

    public bool ShouldDieFromCollision(Collision2D collision)
    {
        if (HasDied)
        {
            return false;
        }

        Unicorn unicorn = collision.gameObject.GetComponent<Unicorn>();
        if (unicorn != null)
        {
            return true;
        }

        if (collision.contacts[0].normal.y < -0.5f)
        {
            return true;
        }

        return false;
    }

    public IEnumerator Die()
    {
        HasDied = true;
        _deathParticleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
