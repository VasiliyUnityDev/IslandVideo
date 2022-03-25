using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveStickman : MonoBehaviour
{
    [SerializeField] BonfireController bonfireController;
    [SerializeField] GameObject point;
    [SerializeField] GameObject board;
    [SerializeField] GameObject canvas;
    [SerializeField] Animator _animStickman;
    [SerializeField] private float speedMovement;
    [SerializeField] private bool isWaitBonfire;
    [SerializeField] private bool isWaitHelp;
    [SerializeField] private bool isDownHelp;

    private void Start()
    {
        isWaitHelp = true;
        if (isDownHelp)
        {
            _animStickman.SetBool("HelpDown", true);
        }
    }

    private void FixedUpdate()
    {
        if (isWaitHelp)
        {
            if (board == null) {
                _animStickman.SetBool("HelpDown", false);
                canvas.SetActive(false);
                isWaitHelp = false; }
            return;
        }

        if (!isWaitBonfire)
        {
            if (bonfireController.isBonfireOpen)
            {
                gameObject.transform.position = Vector3.MoveTowards(transform.position, point.transform.position, speedMovement * Time.deltaTime);
                gameObject.transform.LookAt(point.transform);
                if (transform.position == point.transform.position)
                {
                    _animStickman.SetBool("WaitForBonfire", true);
                    _animStickman.SetBool("Run", false);
                    gameObject.transform.LookAt(bonfireController.transform);
                    LightHouseController.Instance.needSurvivor += 1;
                    isWaitBonfire = true;
                    return;
                }
                _animStickman.SetBool("Run", true);
                return;
            }
            _animStickman.SetBool("Run", false);
        }
    }
}
