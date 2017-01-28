using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatics
{
    public struct PlayerProperties
    {
        public Color color;
        public PlayerControls controls;
        public string name;
    }

    public static PlayerProperties Get(int playerId)
    {
        if (playerId == 2)
            return Player1;
        if (playerId == 3)
            return Player2;
        if (playerId == 1)
            return Player3;
        return Player1;
    }

    public static PlayerProperties Invalid {
        get {
            PlayerProperties p = new PlayerProperties();
            p.color = Color.grey;
            p.controls = new PlayerControls();
            p.name = "INVALID";
            return p;
        }
    }

    public static PlayerProperties Player1 {
        get {
            PlayerProperties p = new PlayerProperties();
            p.color = Color.red;
            p.controls = PlayerController.GamePadController1;
            p.name = "Red";
            return p;
        }
    }

    public static PlayerProperties Player2 {
        get {
            PlayerProperties p = new PlayerProperties();
            p.color = Color.blue;
            p.controls = PlayerController.GamePadController2;
            p.name = "Blue";
            return p;
        }
    }

    public static PlayerProperties Player3 {
        get {
            PlayerProperties p = new PlayerProperties();
            p.color = Color.yellow;
            p.controls = PlayerController.Keyboard;
            p.name = "Yellow";
            return p;
        }
    }
}

public class PlayerSpawn : MonoBehaviour
{
    public int id = 0;

    private void OnDrawGizmos()
    {
        Color prevColor = Gizmos.color;
        Gizmos.color = PlayerStatics.Get(id).color;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 10f, Vector3.one * 20f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 80f);
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 80f, 10f);
        Gizmos.color = prevColor;
    }
}
