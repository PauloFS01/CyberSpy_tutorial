using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeToRespaw=2f;
    public void PlayerRespawn()
    {
        StartCoroutine(WillReapawnCouroutine());
    }

    IEnumerator WillReapawnCouroutine()
    {
        yield return new WaitForSeconds(timeToRespaw);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
