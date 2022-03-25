using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    #region Singelton
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public GameObject i;
    public GameObject cloud;

    [Header("Timer")]
    [SerializeField] private float maxTime;
    [SerializeField] private float timer;
    [SerializeField] private bool isTime;

    [Header("House")]
    [SerializeField] Transform pointHouse;
    [SerializeField] ParticleSystem effectSleep;

    [Header("StaminaPlayer")]
    [SerializeField, Range(0, 100)] private float valueStamina;
    [SerializeField, Range(0, 102.6375f)] private float transparment;

    [Header("WeaponPlayer")]
    [SerializeField] GameObject[] axe;

    [Header("MovementSettings")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] private float speedMovement;
    [SerializeField] private float speedRotate;
    [SerializeField] private float gravityMultyplier;
    [SerializeField] private float groundDistance;
    [SerializeField] Animator _animPlayer;
    [SerializeField] string[] namePunch;
    private bool canMovement;
    private bool isPunch;
    private bool isDie;

    Transform _transformPlayer;
    CharacterController _characterController;
    Vector3 gravityVelocity;
    FloatingJoystick joystick;

    private void Start()
    {
        _transformPlayer = gameObject.GetComponent<Transform>();
        _characterController = gameObject.GetComponent<CharacterController>();
        joystick = GameObject.FindObjectOfType<FloatingJoystick>();
    }

    private void FixedUpdate()
    {
        if (TimesOfDayController.Instance.isNight)
        {
            i.SetActive(true);
            i.transform.DOScale(transparment, 1f);
        }
        if (!TimesOfDayController.Instance.isNight)
        {
            valueStamina = 50;
            transparment = 118.2794f;
            i.transform.DOScale(transparment, 1f);
        }

        if(valueStamina <= 30)
        {
            cloud.SetActive(true);
            _animPlayer.SetBool("Idle", false);
            _animPlayer.SetBool("Run", false);
        }
        if(valueStamina > 30)
        {
            cloud.SetActive(false);
        }
    }

    private void Update()
    {
        if (isDie || LightHouseController.Instance.isLevelCompleted)
            return;
        Timer();
        Controll();
    }

    private void Controll()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animPlayer.SetBool("Sleep", false);
            _animPlayer.SetBool("WaitBonfire", false);
            if (valueStamina <= 30)
            {
                _animPlayer.SetBool("ScaryWalk", true);
                _animPlayer.SetBool("Scary", false);
                canMovement = true;
                return;
            }
            _animPlayer.SetBool("ScaryWalk", false);
            _animPlayer.SetBool("Scary", false);
            _animPlayer.SetBool("Idle", false);
            _animPlayer.SetBool("Run", true);
            effectSleep.Stop();
            canMovement = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (joystick != null)
            {
                var xInput = joystick.Horizontal;
                var yInput = joystick.Vertical;

                _characterController.Move((Vector3.right * xInput + Vector3.forward * yInput) * speedMovement * Time.deltaTime);

                transform.LookAt(transform.position + (Vector3.right * xInput + Vector3.forward * yInput) * speedRotate * Time.deltaTime);

                var isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                if (isGrounded && gravityVelocity.y < 0 && isGrounded && gravityVelocity.y > 1)
                {
                    gravityVelocity.y = -2f;
                }

                gravityVelocity += Vector3.up * gravityMultyplier * Time.deltaTime;
                _characterController.Move(gravityVelocity);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (valueStamina <= 30)
            {
                _animPlayer.SetBool("Idle", false);
                _animPlayer.SetBool("ScaryWalk", false);
                _animPlayer.SetBool("Scary", true);
                canMovement = false;
                return;
            }
            _animPlayer.SetBool("Scary", false);
            _animPlayer.SetBool("Run", false);
            _animPlayer.SetBool("ScaryWalk", false);
            canMovement = false;
        }
    }

    private void Timer()
    {
        if (TimesOfDayController.Instance.isNight && valueStamina > 0)
        {
            if (timer < maxTime)
            {
                timer += Time.deltaTime;
                isTime = false;
            }
            if (timer >= maxTime)
            {
                if (!isTime)
                {
                    valueStamina -= 2;
                    transparment -= 4;
                    timer = 0;
                    isTime = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDie)
            return;

        if(other.gameObject.tag == "House")
        {
            if (valueStamina <= 30)
                return;

            UIManager.Instance.HouseButtonActive(true);
        }

        if (other.gameObject.tag == "Log")
        {
            if (valueStamina <= 30)
                return;

            if (!Backpack—ontroller.Instance.isMaxBackpack)
            {
                Destroy(other.gameObject);
                StartCoroutine(Backpack—ontroller.Instance.NewStuff(0));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "FireZone")
        {
            if (other.gameObject.GetComponent<BonfireController>().isBonfireOpen)
            {
                if (!canMovement &&
                    !other.gameObject.GetComponent<BonfireController>().fireOff)
                {
                    _animPlayer.SetBool("Run", false);
                    _animPlayer.SetBool("Idel", false);
                    _animPlayer.SetBool("WaitBonfire", true);
                    other.gameObject.GetComponent<BonfireController>().ScreenFire(true);
                    if (valueStamina < 50)
                    {
                        valueStamina++;
                    }
                    if(transparment < 118.2794f)
                    {
                        transparment++;
                    }
                }
            }
            Backpack—ontroller.Instance.UnstackStuff(other.gameObject, 0);
        }

        if(other.gameObject.tag == "Bridge")
        {
            if (valueStamina <= 30)
                return;
            if (other.gameObject.GetComponent<BridgeController>().isBridgeOpen)
            {
                return;
            }
            Backpack—ontroller.Instance.UnstackStuff(other.gameObject, 1);
        }

        if(other.gameObject.tag == "LightHouse")
        {
            if (valueStamina <= 30)
                return;
            if (other.gameObject.GetComponent<LightHouseController>().isLightHouseOpen)
            {
                return;
            }
            Backpack—ontroller.Instance.UnstackStuff(other.gameObject, 2);
        }

        if (other.gameObject.tag == "Tree"){
            if(valueStamina <= 30)
                return;

            if (!canMovement && other.gameObject != null){
                if (!isPunch && !Backpack—ontroller.Instance.isMaxBackpack){
                    if (!other.GetComponent<Resource>().isCrushResource)
                    {
                        StartCoroutine(PunchResource(other));
                        axe[0].SetActive(false);
                        axe[1].SetActive(true);
                        isPunch = true;
                    }
                }
            }
            else{
                StopCoroutine(PunchResource(null));
                axe[0].SetActive(true);
                axe[1].SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        axe[0].SetActive(true);
        axe[1].SetActive(false);
        UIManager.Instance.HouseButtonActive(false);
        if(other.gameObject.GetComponent<BonfireController>() != null)
        {
            other.gameObject.GetComponent<BonfireController>().ScreenFire(false);
        }
    }
    private int random;
    IEnumerator PunchResource(Collider collider)
    {
        if (collider != null 
            && !collider.GetComponent<Resource>().isCrushResource){
            random = Random.Range(0, 2);
            _animPlayer.SetTrigger(namePunch[random]);
            collider.GetComponent<Resource>().PunchPlayer(1);
        }
        yield return new WaitForSeconds(0.8f);
        isPunch = false;
    }

    public void SleepPlayer()
    {
        _animPlayer.SetBool("Sleep", true);
        _transformPlayer.position = pointHouse.position;
        _transformPlayer.rotation = pointHouse.rotation;
        effectSleep.Play();
    }
}
