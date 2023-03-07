using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ControllerTests : IPrebuildSetup
{
    GameObject player;
    [SetUp]
    public void Setup()
    {
        player = new GameObject();
        player.AddComponent<PlayerController>();
        player.AddComponent<CharacterController>();       
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
}
