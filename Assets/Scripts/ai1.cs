using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Panda;

public class ai1 : MonoBehaviour
{
    //Referência ao transform do Player
    public Transform player;
    //Ponto de spawn das balas
    public Transform bulletSpawn;
    //Barra de vida
    public Slider healthBar;
    //Prefab da bala
    public GameObject bulletPrefab;
    //Componente NavMeshAgent para controle de movimento
    NavMeshAgent agent;
    //Destino de movimento
    public Vector3 destination;
    //Posição alvo
    public Vector3 target;
    //Vida do Droid
    float health = 100.0f;
    //Velocidade de rotação
    float rotSpeed = 5.0f;
    //Alcance de visão
    float visibleRange = 80.0f;
    //Alcance do disparo
    float shotRange = 40.0f;

    void Start()
    {
        //Esta obtendo o componente NavMeshAgent 
        agent = this.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = shotRange - 5; //for a little buffer
        //Chama repetidamente o método UpdateHealth
        InvokeRepeating("UpdateHealth", 5, 0.5f);
    }
    void Update()
    {
        //Converte a posição do Droid para a posição na tela
        Vector3 healthBarPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //Atualiza o valor da barra de vida 
        healthBar.value = (int)health;
        //posição da barra de vida em cima do Droid na tela
        healthBar.transform.position = healthBarPos + new Vector3(0, 60, 0);
    }
    void UpdateHealth()
    {
        //Esta verificando se a vida atual está abaixo de 100, se estiver, ele almenta o valor da vida em 1
        if (health < 100)
            health++;
    }
    [Task]
    public void PickRandomDestination()
    {
        //Gera uma posição de destino aleatória
        Vector3 dest = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        //Define o destino do agente
        agent.SetDestination(dest);
        //Indica que a tarefa foi concluída 
        Task.current.Succeed();
    }
    [Task]
    public void MoveToDestination()
    {
        if (Task.isInspected)
            //informações de depuração da tarefa
            Task.current.debugInfo = string.Format("t={0:0.00}", Time.time);
        //Verifica se o agente chegou ao destino
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            //Indica que a tarefa foi concluída 
            Task.current.Succeed();
        }
    }
    [Task]
    public bool IsHealthLessThan(float health)
    {
        return this.health < health;
    }
    [Task]
    public bool Explode()
    {
        Destroy(healthBar.gameObject);
        Destroy(this.gameObject);
        return true;
    }
}
