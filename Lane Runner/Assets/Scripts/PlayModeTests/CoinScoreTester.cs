using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CoinScoreTester : IPrebuildSetup
{
    GameObject player;
    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Scenes/GameScene.unity", new LoadSceneParameters(LoadSceneMode.Additive));
        player = new GameObject();
        player.AddComponent<PlayerController>();
        player.AddComponent<CoinInventory>();
        player.AddComponent<CharacterController>();
        player.transform.position = new Vector3(137, 1, 0);
    }

    [UnityTest]
    public IEnumerator CoinsAddedToInventory()
    {
        var coins = player.GetComponent<CoinInventory>().coins;
        yield return new WaitForSeconds(3);
        Assert.Greater(0, coins);
    }
}
