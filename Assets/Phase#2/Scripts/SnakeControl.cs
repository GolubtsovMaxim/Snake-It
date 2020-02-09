using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeControl : MonoBehaviour
{
    public List<Transform> Tails; //list of bones
    [Range(0, 3)]
    public float BonesDistance; //distance between bones elems
    public GameObject BonePreFab; //template for the tail
    [Range(0, 4)]
    public float Velocity;

    private Transform _transform; //ref to transform

    private void Start()
    {
        _transform = GetComponent<Transform>(); //get Snake's <Transform> info
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Velocity);

        float angle = Input.GetAxis("Horizontal") * 4;
        _transform.Rotate(0, angle, 0);
    }

    private void MoveSnake(Vector3 newPosition)
    {
        var dist = Mathf.Pow(BonesDistance, 2);
        Vector3 PrevBonePos = _transform.position; //transform is actually a Snake's head

        foreach (var bone in Tails)
        {
            if ((bone.position - PrevBonePos).sqrMagnitude > dist )
            {
                var temp = bone.position;
                bone.position = PrevBonePos;
                PrevBonePos = temp;
            }
            else
            {
                break;
            }
        }

        _transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);

            var bone = Instantiate(BonePreFab);
            Tails.Add(bone.transform);
        }
    }
}
