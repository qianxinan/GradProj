﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    //玩家属性
    [Header("基础属性")]
    [Header("父类属性")]
    
    public int MaxHP;
    public int _HP;
    public int speed;
    public int AttDis = 5;
    public bool dead = false;
    public bool isAttacking = false;
    public bool isHurt = false;
    public int attack = 7;
    public int HP {
        get { return _HP; }
        set {
            _HP = Mathf.Clamp(value, 0, MaxHP);
            if (_HP <= 0) {
                Die();
            }
        }
    }

    public GameObject Player;
    public Animator anim;
    public Rigidbody2D rd;

    [Header("巡逻属性")]
    //巡逻属性
    public float chaseDis = 2;
    public int attackRange = 1;

    public int SkillID;

    public void Start() {
        anim = GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        HP = MaxHP;
        Begin();

    }
    public virtual void Begin() {

    }
    public virtual void DataUp() {

    }
    private void Update() {
        if (dead)
            return;
        DataUp();

        if (isAttacking)
            return;
        if (isHurt)
            return;
        MoveOperation();
    }

    private void MoveOperation() {
        if (Player != null && Vector2.Distance(Player.transform.position, transform.position) < chaseDis) {
            if (Vector2.Distance(Player.transform.position, transform.position) > attackRange) {
                Chase();
                if (anim != null) { anim.SetBool("Walk", true); }
             
            } else {
                if (anim != null) { anim.SetBool("Walk", false); }
                Attack();
              
            }
        } else {
            Seek();
            if (anim != null) { anim.SetBool("Walk", true); }
        }
    }

    public virtual void Attack() {
        isAttacking = true;
    }

    public virtual void BeAttacked(int IntCount) {
        if (HP > 0)
        {
            isHurt = true;
            HP -= IntCount;
            rd.AddForce(new Vector2(100000, 1000));
            ResetAttackState();
        }
    }

    public virtual void Seek() {

    }

    public virtual void Chase() {

    }

    public virtual void Die() {
        dead = true;
        GameManger.instance.skillStoneCreator.CreateSkillStone(SkillID, transform.position);
    }

    public void ResetAttackState() {
        isAttacking = false;
    }

    public void ResetHurtState() {
        isHurt = false;
    }

}
