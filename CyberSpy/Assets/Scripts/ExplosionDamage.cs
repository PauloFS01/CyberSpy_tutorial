using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public int damageCaused;

    private void Update()
    {
        StartCoroutine(SelfDestroy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            other.gameObject.GetComponent<EnemyHealthSystem>().TakeDamage(damageCaused);

        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<PlayerhealthSystem>().TakeDamage(damageCaused);
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
