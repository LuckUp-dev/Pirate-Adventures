﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrew.Model;

namespace PixelCrew.Components
{

    public class ReloadLevelComponent : MonoBehaviour
    {

        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

    }
}