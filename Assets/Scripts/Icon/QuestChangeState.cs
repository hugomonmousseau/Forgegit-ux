using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChangeState : MonoBehaviour
{
    [SerializeField] bool questCompleted;
    Animator anim;
    [SerializeField] List<GameObject> questPossibilities;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    [ContextMenu("Switch State")]
    void SwitchQuestState()
    {
        questCompleted = !questCompleted;
        anim.SetTrigger("Rotate");
    }

    public void SwitchQuestPossibilities()
    {
        foreach(GameObject objects in questPossibilities)
        {
            //print(objects.activeInHierarchy + " " + objects.name);
            objects.SetActive(!objects.activeInHierarchy);
        }
    }
}
