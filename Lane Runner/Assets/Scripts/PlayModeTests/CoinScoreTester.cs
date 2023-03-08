using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CoinScoreTester : IPrebuildSetup
{
    GameObject player;
    GameObject coin;
    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Scenes/GameScene.unity", new LoadSceneParameters(LoadSceneMode.Additive));
        coin = GameObject.FindGameObjectWithTag("Coin");
        GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Player.prefab");
        player = GameObject.Instantiate(playerPrefab);
        player.transform.position = new Vector3(137, 1, 0);
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(player);
        EditorSceneManager.UnloadSceneAsync("Assets/Scenes/GameScene.unity");
    }

    [UnityTest]
    public IEnumerator CoinsAddedToInventory()
    {        
        yield return new WaitForSeconds(3);
        var coins = player.GetComponent<CoinInventory>().coins;
        Assert.Greater(coins, 0);
        
    }

    [UnityTest]
    public IEnumerator CoinsRemovedFromScene()
    {
        yield return new WaitForSeconds(5);
        Assert.IsNull(coin);
    }
}
