using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ControllerTests : IPrebuildSetup
{
    GameObject player;

    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Scenes/GameScene.unity", new LoadSceneParameters(LoadSceneMode.Additive));
        GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Player.prefab");
        player = GameObject.Instantiate(playerPrefab);
            
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(player);
        EditorSceneManager.UnloadSceneAsync("Assets/Scenes/GameScene.unity");
    }

    [UnityTest]
    public IEnumerator LeftMovementTest()
    {
        var storePosition = player.transform.position.z;
        player.GetComponent<PlayerController>().MoveLane(false);
        yield return new WaitForSeconds(3);
        Assert.Greater(storePosition, player.transform.position.z);      

    }


    [UnityTest]
    public IEnumerator RightMovementTest()
    {
        var storePosition = player.transform.position.z;
        player.GetComponent<PlayerController>().MoveLane(true);
        yield return new WaitForSeconds(3);

        Assert.Greater(player.transform.position.z, storePosition);
        
    }

    [UnityTest]
    public IEnumerator JumpTest()
    {
        var storePosition = player.transform.position.y;
        player.GetComponent<PlayerController>().Jump(true);
        yield return new WaitForSeconds(.5f);

        Assert.Greater(player.transform.position.y, storePosition);
    }

    [UnityTest]
    public IEnumerator IsPlayerAffectedByGravity()
    {
      
        player.GetComponent<PlayerController>().forwardMovement = 0;
        yield return new WaitForSeconds(2);
        var storePosition = player.transform.position.y;
        player.GetComponent<PlayerController>().Jump(true);
        yield return new WaitForSeconds(6);

        Assert.AreEqual(storePosition, player.transform.position.y);
      
    }

    [UnityTest]
    public IEnumerator PlayerCantMoveWhileJumping()
    {
      
        yield return new WaitForSeconds(1);
        var storePosition = player.transform.position.z;
        player.GetComponent<PlayerController>().Jump(true);
        player.GetComponent<PlayerController>().MoveLane(true);        

        Assert.AreEqual(storePosition, player.transform.position.z);
     
    }
}
