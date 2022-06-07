using System.Collections;
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
        if (Input.GetKeyDown(KeyCode.W) && usingSkill == false
                                        && _playerController.faceRight == false) // left side teleport
        {
            Skill1Left();
        }
        if (Input.GetKeyDown(KeyCode.W) && usingSkill == false 
                                        && _playerController.faceRight == true) // right side teleport
        {
            Skill1Right();
        }
        
        // Skill 2
        if (Input.GetKeyDown(KeyCode.E) && usingSkill == false)
        {
            Skill2();
        }
    }

    #region Skill
    
    void Skill()
    {
        _playerController.ReSize(true);
        
        _gun.ammoMinus(-minusAmmoValue);
        Instantiate(skillEffect, transform.position, transform.rotation); // Instantiate effect
        StartCoroutine(ResetSize(waitTime));
    }

    IEnumerator ResetSize(float secs)
    {
        usingSkill = true;
        yield return new WaitForSeconds(secs);
        Instantiate(skillEffect, transform.position, transform.rotation); // Instantiate effect
        
        _playerController.ReSize(false);
        
    }
    
    #endregion

    #region Skill1

    void Skill1Left()
    {
        _gun.ammoMinus(-minusAmmoValue);
        Instantiate(skill1Effect, transform.position, transform.rotation); // Instantiate effect
        _playerController.rb.AddForce(transform.right * -forceToMove);
        Instantiate(skill1Effect, transform.position, transform.rotation); // Instantiate effect
        usingSkill = true;
    }
    void Skill1Right()
    {
        _gun.ammoMinus(-minusAmmoValue);
        Instantiate(skill1Effect, transform.position, transform.rotation); // Instantiate effect
        _playerController.rb.AddForce(transform.right * forceToMove);
        Instantiate(skill1Effect, transform.position, transform.rotation); // Instantiate effect
        usingSkill = true;
    }

    #endregion

    #region Skill2

    void Skill2()
    {
        _gun.ammoMinus(-minusAmmoValue);
        usingSkill = true;
        _playerController.rb.gravityScale /= 2;
        StartCoroutine(ResetGravityScale(7f));
    }

    IEnumerator ResetGravityScale(float secs)
    {
        yield return new WaitForSeconds(secs);
        _playerController.rb.gravityScale *= 2;
    }

    #endregion

}
