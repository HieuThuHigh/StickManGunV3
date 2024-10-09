using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{


    public SpriteRenderer hat;     // Renderer của mũ
    public SpriteRenderer head;     // Renderer của đầu
    public SpriteRenderer face;     // Renderer của mặt
    public SpriteRenderer body;     // Renderer của thân
    public SpriteRenderer leftArm;  // Renderer của tay trái
    public SpriteRenderer gun;  // Renderer của gun
    public SpriteRenderer rightArm; // Renderer của tay phải
    public SpriteRenderer leftLeg;  // Renderer của chân trái
    public SpriteRenderer rightLeg; // Renderer của chân phải

    // Các sprite tùy chỉnh cho từng bộ phận
    public Sprite[] hatSprites;
    public Sprite[] headSprites;
    public Sprite[] faceSprites;
    public Sprite[] bodySprites;
    public Sprite[] leftArmSprites;
    public Sprite[] gunSprites;
    public Sprite[] rightArmSprites;
    public Sprite[] leftLegSprites;
    public Sprite[] rightLegSprites;

    // Vị trí cố định cho từng bộ phận
    public Vector3 hatPosition = new Vector3(-0.05f, -0.05f, 0); // Vị trí mũ
    public Vector3 headPosition = new Vector3(0, -0.23f, 0); // Vị trí đầu
    public Vector3 facePosition = new Vector3(0, -0.24f, 0); // Vị trí mặt
    public Vector3 bodyPosition = new Vector3(-0.04f, -0.38f, 0); // Vị trí thân
    public Vector3 leftArmPosition = new Vector3(0.3f, -0.35f, 0); // Vị trí tay trái
    public Vector3 gunPosition = new Vector3(0.393f, -0.46f, 0); // Vị trí tay trái
    public Vector3 rightArmPosition = new Vector3(0.214f, -0.35f, 0);  // Vị trí tay phải
    public Vector3 leftLegPosition = new Vector3(0, -0.64f, 0); // Vị trí chân trái
    public Vector3 rightLegPosition = new Vector3(0f, -0.64f, 0); // Vị trí chân phải

    // Hàm dùng để đặt vị trí cho các bộ phận
    void SetPartPositions()
    {
        // Đặt vị trí cho từng bộ phận
        hat.transform.position = hatPosition;
        head.transform.position = headPosition;
        body.transform.position = bodyPosition;
        leftArm.transform.position = leftArmPosition;
        gun.transform.position = gunPosition;
        rightArm.transform.position = rightArmPosition;
        leftLeg.transform.position = leftLegPosition;
        rightLeg.transform.position = rightLegPosition;
    }
    // Hàm thay đổi đầu của nhân vật
    public void ChangeHat(int index)
    {
        if (index >= 0 && index < headSprites.Length)
        {
            head.sprite = headSprites[index];
        }
    }
    public void ChangeHead(int index)
    {
        if (index >= 0 && index < headSprites.Length)
        {
            head.sprite = headSprites[index];
        }
    }
    public void ChangeFace(int index)
    {
        if (index >= 0 && index < headSprites.Length)
        {
            head.sprite = headSprites[index];
        }
    }

    // Hàm thay đổi thân của nhân vật
    public void ChangeBody(int index)
    {
        if (index >= 0 && index < bodySprites.Length)
        {
            body.sprite = bodySprites[index];
        }
    }

    // Tương tự cho các bộ phận khác
    public void ChangeLeftArm(int index)
    {
        if (index >= 0 && index < leftArmSprites.Length)
        {
            leftArm.sprite = leftArmSprites[index];
        }
    }

    public void ChangeRightArm(int index)
    {
        if (index >= 0 && index < rightArmSprites.Length)
        {
            rightArm.sprite = rightArmSprites[index];
        }
    }
    public void ChangeGunArm(int index)
    {
        if (index >= 0 && index < rightArmSprites.Length)
        {
            rightArm.sprite = rightArmSprites[index];
        }
    }

    public void ChangeLeftLeg(int index)
    {
        if (index >= 0 && index < leftLegSprites.Length)
        {
            leftLeg.sprite = leftLegSprites[index];
        }
    }

    public void ChangeRightLeg(int index)
    {
        if (index >= 0 && index < rightLegSprites.Length)
        {
            rightLeg.sprite = rightLegSprites[index];
        }
    }
}


