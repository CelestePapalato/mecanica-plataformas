using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas _finDelJuegoCanvas;
    [SerializeField] float _waitTimeReloadScene;

    private void Awake()
    {
        if (_finDelJuegoCanvas)
        {
            _finDelJuegoCanvas.enabled = false;
        }
    }

    public void GameOver()
    {
        if (_finDelJuegoCanvas)
        {
            _finDelJuegoCanvas.enabled = true;
        }
        StartCoroutine(_reloadScene());
    }
    public IEnumerator _reloadScene()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(_waitTimeReloadScene);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
