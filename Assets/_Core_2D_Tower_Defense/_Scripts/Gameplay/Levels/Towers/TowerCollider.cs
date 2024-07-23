using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerCollider : MonoBehaviour
{
    public CircleCollider2D coll;
    public Tower tower;

    public List<Monster> targets;
    public List<Monster> targetsPassed;
    public List<Monster> targetsBacked;

    public void Init(Tower tower)
    {
        this.tower = tower;
        coll = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.On_Spawn_Next_Wave, ClearLists);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.On_Spawn_Next_Wave, ClearLists);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enermy"))
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                // Nếu quái thoát khỏi tầm mà lại quay lại tầm bắn 
                if (targetsPassed.Contains(monster))
                {
                    targetsBacked.Add(monster);
                }

                if (!targets.Contains(monster))
                {
                    targets.Add(monster);
                    targetsPassed.Add(monster);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enermy"))
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                if (targets.Contains(monster))
                {
                    targets.Remove(monster);
                }
                
                if (targetsBacked.Contains(monster))
                {
                    targetsBacked.Remove(monster);
                }
            }
        }
    }

    public Monster GetCurrentTarget()
    {
        Monster monster;
        
        if (targetsBacked.Count > 0)
        {
            monster = targetsBacked[0];
            for (int i = 0; i < targetsBacked.Count; i++)
            {
                if (targetsBacked[i] != null)
                {
                    return targetsBacked[i];
                }
            }
        }
        else
        {
            monster = targets[0];
        }
        
        return monster;
    }

    public bool CanDetectTarget()
    {
        if (targets.Count > 0 || targetsBacked.Count > 0)
        {
            return true;
        }

        return false;
    }

    private void ClearLists(object param)
    {
        targets.Clear();
        targetsPassed.Clear();
        targetsBacked.Clear();
    }
}