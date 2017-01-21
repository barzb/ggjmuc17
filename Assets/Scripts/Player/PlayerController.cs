using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerControls
{
    public string HorizontalAxisName;
    public string VerticalAxisName;
    public string KickBombKeyName;
    public string PlaceBombKeyName;

    public PlayerControls(string horizontalAxisName, string verticalAxisName, string kickBombKeyName, string placeBombKeyName)
    {
        this.HorizontalAxisName = horizontalAxisName;
        this.VerticalAxisName = verticalAxisName;
        this.KickBombKeyName = kickBombKeyName;
        this.PlaceBombKeyName = placeBombKeyName;
    }
}

[RequireComponent(typeof(BombPlacer))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private PlayerControls localPlayerControls;
    private new Rigidbody rigidbody;
    private BombPlacer bombPlacer;
    private BombBase createdBomb;
    public float LocalPlayerSpeed { get; set; }

    // input setup
    public static PlayerControls GamePadController1 = new PlayerControls("Horizontal Controller1", "Vertical Controller1", "Kick Bombs Controller1", "Place Bombs Controller1");
    public static PlayerControls GamePadController2 = new PlayerControls("Horizontal Controller2", "Vertical Controller2", "Kick Bombs Controller2", "Place Bombs Controller2");
    public static PlayerControls Keyboard = new PlayerControls("Horizontal Keyboard", "Vertical Keyboard", "Kick Bombs Keyboard", "Place Bombs Keyboard");

    public void ListenToInput(PlayerControls inputControls) // set LocalPlayer Controls
    {
        localPlayerControls = inputControls;
    }

    // Use this for initialization
    void Start()
    {
        LocalPlayerSpeed = 10f;
        rigidbody = GetComponent<Rigidbody>();

        //player1 = Xbox
        ListenToInput(GamePadController1);

        bombPlacer = GetComponent<BombPlacer>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if localPlayerControls is set, if not return
        if (localPlayerControls.Equals(default(PlayerControls))) return;

        // calculate movement
        float horizontalVelocity = Input.GetAxis(localPlayerControls.HorizontalAxisName);
        float verticalVelocity = Input.GetAxis(localPlayerControls.VerticalAxisName);
        Vector3 Velocity = new Vector3(horizontalVelocity, 0, verticalVelocity);

        // move player
        rigidbody.velocity = (Velocity * Time.deltaTime * LocalPlayerSpeed * 100f);

        // call function if place bomb key is pressed
        if (Input.GetButtonDown(localPlayerControls.PlaceBombKeyName))
        {
            PlaceBomb();
        }

        // call function if kick bomb key is released
        if (Input.GetButtonUp(localPlayerControls.KickBombKeyName))
        {
            EndKickBomb();
        }

        // call function if kick bomb key is pressed
        if (Input.GetButtonDown(localPlayerControls.KickBombKeyName))
        {
            StartKickBomb();
        }
    }

    private void PlaceBomb()
    {
        if (createdBomb == null)
        {
            createdBomb = bombPlacer.CreateBomb(this.transform.position + new Vector3(15f, 0, 0));
        }
    }

    private void StartKickBomb()
    {
        if (createdBomb.CanKick(transform.position))
        {
            createdBomb.StartKick(transform);
        }
    }

    private void EndKickBomb()
    {
        createdBomb.EndKick();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
