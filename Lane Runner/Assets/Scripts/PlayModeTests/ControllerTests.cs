using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
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
        player = new GameObject();
        player.AddComponent<PlayerController>();
        player.AddComponent<CharacterController>();
        player.transform.position = new Vector3(137, 1, 0);
        
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
        yield return new WaitForSeconds(2);

        Assert.Greater(storePosition, player.transform.position.y);
    }

    [UnityTest]
    public IEnumerator IsPlayerAffectedByGravity()
    {
        player.GetComponent<PlayerController>().forwardMovement = 0;
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
