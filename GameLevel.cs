using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevel : MonoBehaviour
{
    [SerializeField] string nextLevelName;
    Enemy[] _enemies;

    void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }


    // Update is called once per frame
    void Update()
    {
        if (EnemiesDead())
            NextLevel();
    }

    void NextLevel()
    {
        Debug.Log("On to the next Level" + nextLevelName);
        SceneManager.LoadScene(nextLevelName);


    }

    bool EnemiesDead()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy.gameObject.activeSelf)
                return false;
        }
        return true;
    }
}
