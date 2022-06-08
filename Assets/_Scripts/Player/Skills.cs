using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    private Gun _gun;
    private PlayerController _playerController;

    private bool CDSkill = false,
        CDSkill1 = false,
        CDSkill2 = false;

    private Vector3 _originalSize = new Vector3((float)1, (float)1, (float)1);
    private Vector3 _resizeSize = new Vector3((float)0.5, (float)0.5, (float)0.5);

    public float forceToMove; // for Skills

    public int minusAmmoValue;
    public float waitTime;

    public ParticleSystem skillEffect;
    public ParticleSystem skill1Effect;

    [Header("Mini Map & Full Map")] public GameObject MiniMap;
    public GameObject FullMap;

    [Header("Skills Images")] public Image[] SkillsImages;

    private void Start()
    {
        // mini map instantiate & big map set active - false
        if (MiniMap != null && FullMap != null)
        {
            MiniMap.gameObject.SetActive(true);
            FullMap.gameObject.SetActive(false);
        }

        _playerController = GetComponent<PlayerController>();
        _gun = GameObject.FindGameObjectWithTag("Stick").GetComponent<Gun>();
    }

    private void Update()
    {
        // mini map(full map) multiply & devide
        if (MiniMap != null && FullMap != null)
        {
            if (Input.GetMouseButtonDown(2))
            {
                MiniMap.gameObject.SetActive(false);
                FullMap.gameObject.SetActive(true);
            }
            else if (Input.GetMouseButtonUp(2))
            {
                MiniMap.gameObject.SetActive(true);
                FullMap.gameObject.SetActive(false);
            }
        }

        // SKill
        if (Input.GetKeyDown(KeyCode.Q) && CDSkill == false)
        {
            Skill();
        }

        // Skill 1
        if (Input.GetKeyDown(KeyCode.W) && CDSkill1 == false
                                        && _playerController.faceRight == false) // left side teleport
        {
            Skill1Left();
        }

        if (Input.GetKeyDown(KeyCode.W) && CDSkill1 == false
                                        && _playerController.faceRight == true) // right side teleport
        {
            Skill1Right();
        }

        // Skill 2
        if (Input.GetKeyDown(KeyCode.E) && CDSkill2 == false)
        {
            Skill2();
        }
    }

    #region Skill

    void Skill()
    {
        _playerController.ReSize(true);
        CDSkill = true;

        Instantiate(skillEffect, transform.position, transform.rotation); // Instantiate effect
        StartCoroutine(ResetSize(waitTime));
        // Skill CD reset
        StartCoroutine(SkillCDReset(8f, 0, 0));
    }

    IEnumerator ResetSize(float secs)
    {
        yield return new WaitForSeconds(secs);
        Instantiate(skillEffect, transform.position, transform.rotation); // Instantiate effect
        _playerController.ReSize(false);
        Instantiate(skillEffect, transform.position, transform.rotation); // Instantiate effect
    }

    #endregion

    #region Skill1

    void Skill1Left()
    {
        Instantiate(skill1Effect, transform.position, transform.rotation); // Instantiate effect
        _playerController.rb.AddForce(transform.right * -forceToMove);
        Instantiate(skill1Effect, transform.position, transform.rotation); // Instantiate effect
        // Skill CD reset
        StartCoroutine(SkillCDReset(8f, 1, 1));
    }

    void Skill1Right()
    {
        Instantiate(skill1Effect, transform.position, transform.rotation); // Instantiate effect
        _playerController.rb.AddForce(transform.right * forceToMove);
        Instantiate(skill1Effect, transform.position, transform.rotation); // Instantiate effect
        // Skill CD reset
        StartCoroutine(SkillCDReset(8f, 1, 1));
    }

    #endregion

    #region Skill2

    void Skill2()
    {
        Instantiate(skill1Effect, transform.position, transform.rotation); // Instantiate effect
        _playerController.rb.gravityScale /= 2;
        StartCoroutine(ResetGravityScale(7f));
        // Skill CD reset
        StartCoroutine(SkillCDReset(8f, 2, 2));
    }

    IEnumerator ResetGravityScale(float secs)
    {
        yield return new WaitForSeconds(secs);
        Instantiate(skill1Effect, transform.position, transform.rotation); // Instantiate effect
        _playerController.rb.gravityScale *= 2;
    }

    #endregion

    #region Skill Coul Down Reset

    IEnumerator SkillCDReset(float secs, int numberOfArray, int whichSkillUse)
    {
        // Examination of which skill use
        if (whichSkillUse == 0)
        {
            CDSkill = true;
        }
        else if (whichSkillUse == 1)
        {
            CDSkill1 = true;
        }
        else if (whichSkillUse == 2)
        {
            CDSkill2 = true;
        }
        else
        {
            Debug.LogError("Err! no area array");
        }

        // _gun.ammoChange(-minusAmmoValue);
        _gun.ammoChange(-minusAmmoValue);
        SkillsImages[numberOfArray].color = new Color32(255, 100, 100, 100);
        yield return new WaitForSeconds(secs);
        SkillsImages[numberOfArray].color = new Color32(100, 245, 255, 255);

        // Examination of which skill use
        if (whichSkillUse == 0)
        {
            CDSkill = false;
        }
        else if (whichSkillUse == 1)
        {
            CDSkill1 = false;
        }
        else if (whichSkillUse == 2)
        {
            CDSkill2 = false;
        }
        else
        {
            Debug.LogError("Err! no area array");
        }
    }

    #endregion
}