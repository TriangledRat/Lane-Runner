using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ObstacleTest : IPrebuildSetup
{
    GameObject player, obstacle;

    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Scenes/GameScene.unity", new LoadSceneParameters(LoadSceneMode.Additive));
        player = new GameObject();
        player.AddComponent<PlayerController>();
        player.AddComponent<CharacterController>();
        player.AddComponent<PlayerLives>();
        player.transform.position = new Vector3(137, 1, 0);
        obstacle = GameObject.FindGameObjectWithTag("Obstacle");
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(player);
        EditorSceneManager.UnloadSceneAsync("Assets/Scenes/GameScene.unity");
    }


    [UnityTest]
    public IEnumerator IsCollisionDetected()
    {
        if(obstacle != null && obstacle.transform.position.x == player.transform.position.x)
        {
            Assert.IsTrue(player.GetComponent<PlayerController>().hit);
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator DoesPlayerSlowDown()
    {
        var storeSpeed = -player.GetComponent<PlayerController>().forwardMovement;
        if (player.GetComponent<PlayerController>().hit)
        {
            Assert.Greater(storeSpeed, -player.GetComponent<PlayerController>().forwardMovement);
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator DoesPlayerLoseLife()
    {
        var storeLives = player.GetComponent<PlayerLives>().lives;
        if (player.GetComponent<PlayerController>().hit)
        {
            Assert.Less(storeLives, player.GetComponent<PlayerLives>().lives);
        }
        yield return null;
    }

}
