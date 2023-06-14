using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour {
    //Velocidade de movimento do Player
    float speed = 20.0F;
    //Velocidade de rotação do Player
    float rotationSpeed = 120.0F;
    //Aqui vai o prefab da bala
    public GameObject bulletPrefab;
    //Ponto de spawn da bala
    public Transform bulletSpawn;

    void Update() {
        //Obtém o valor da entrada vertical e multiplica pela velocidade do Player
        float translation = Input.GetAxis("Vertical") * speed;
        //Obtém o valor da entrada horizontal e multiplica pela velocidade de rotação do Player
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        //Ajusta a velocidade de movimento 
        translation *= Time.deltaTime;
        //Ajusta a velocidade de rotação
        rotation *= Time.deltaTime;
        //Move o Player para frente 
        transform.Translate(0, 0, translation);
        //Rotaciona o Player em torno do eixo Y 
        transform.Rotate(0, rotation, 0);
        //Verifica se a tecla de espaço foi pressionada
        if(Input.GetKeyDown("space"))
        {
            //Instancia a bala a partir do prefab no ponto de spawn
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            //Aplica uma força para impulsionar a bala para a frente
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*2000);
        }
    }
}
