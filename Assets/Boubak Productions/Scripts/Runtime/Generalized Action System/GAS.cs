using BoubakProductions.Safety;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoubakProductions.GASCore
{
    /// <summary>
    /// Contains GAS actions, to be chained together when necessary.
    /// </summary>
    public static class GAS
    {
        public static void ObjectSetActive(bool status, GameObject gObject)
        {
            SafetyNet.EnsureIsNotNull(gObject, $"{gObject.name} from a GAS action.");
            gObject.SetActive(status);
        }

        public static void SwitchScene(int buildIndex)
        {
            SafetyNet.EnsureIntIsInRange(buildIndex, 0, SceneManager.sceneCount, "Scene buildIndex");
            SceneManager.LoadScene(buildIndex);
        }
    }
}