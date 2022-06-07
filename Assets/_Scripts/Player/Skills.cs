using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    private Gun _gun;
    private PlayerController _playerController;
    private bool usingSkill = false;

    private Vector3 _originalSize = new Vector3((float)1, (float)1, (float)1);
    private Vector3 _resizeSize = new Vector3((float) 0.5, (float) 0.5, (float) 0.5);

    public float forceToMove; 
    
    public int minusAmmoValue;
    public float waitTime;

    public ParticleSystem skillEffect;
    public ParticleSystem skill1Effect;
    
    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _gun = GameObject.FindGameObjectWithTag("Stick").GetComponent<Gun>();
    }

    private void Update()
    {
        if (_gun.ammo == _gun.maxAmmo)
        {
            usingSkill = false;
        }
        
        // SKill
        if (Input.GetKeyDown(KeyCode.Q) && usingSkill == false)
        {
            Skill();
        }

        // Skill 1
        if (Input.GetKeyDown(KeyCode.X) && usingSkill == false) // left side teleport
        {
            Skill1Left();   
        }
        if (Input.GetKeyDown(KeyCode.C) && usingSkill == false) // right side teleport
        {
            Skill1Right();
        }
    }

    #region Skill
    
    void Skill()
    {
        _playerController.FlipExamination(); // examination to flip
        transform.localScale = _resizeSize;
        _playerController.FlipExamination(); // examination to flip
        _gun.ammoMinus(-minusAmmoValue);
        Instantiate(skillEffect, transform.position, transform.rotation); 
        // Instantiate effect
        if (_playerController._moveInput == 0)
        {
            _playerController.Flip();
        }
        _playerController.FlipExamination(); // examination to flip
        StartCoroutine(ResetSize(waitTime));
        _playerController.FlipExamination(); // examination to flip
    }

    IEnumerator ResetSize(float secs)
    {
        _playerController.FlipExamination(); // examination to flip
        usingSkill = true;
        yield return new WaitForSeconds(secs);
        Instantiate(skillEffect, transform.position, transform.rotation); 
        // Instantiate effect
        transform.localScale = _originalSize;
        _playerController.FlipExamination(); // examination to flip

    }
    
    #endregion

    #region Skill1

    void Skill1Left()
    {
        _gun.ammoMinus(-minusAmmoValue);
        Instantiate(skill1Effect, transform.position, transform.rotation);
        // Instantiate effect
        _playerController.rb.AddForce(transform.right * -forceToMove);
        Instantiate(skill1Effect, transform.position, transform.rotation);
        // Instantiate effect
        usingSkill = true;
    }
    void Skill1Right()
    {
        _gun.ammoMinus(-minusAmmoValue);
        Instantiate(skill1Effect, transform.position, transform.rotation); 
        // Instantiate effect
        _playerController.rb.AddForce(transform.right * forceToMove);
        Instantiate(skill1Effect, transform.position, transform.rotation); 
        // Instantiate effect
        usingSkill = true;
    }

    #endregion

}
