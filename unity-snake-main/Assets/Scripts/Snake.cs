using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    //Declaração de uma nova lista(sendo o segments o corpo da cobra)
    private List<Transform> _segments = new List<Transform>();
    //Declrarção do prefab a ser utilizado
    public Transform segmentPrefab;
    //Declaração da direção que a cobra ira começar andando toda vez que nascer
    public Vector2 direction = Vector2.right;
    //Declração da variável initialSize, que define o tamanho inicial da cobra
    public int initialSize = 4;
        
    //Criação da função privada "Start"
    private void Start()
    {
        //Chamando o método ResetState
        ResetState();
    }
    
    //Criação da função privada "Update"
    private void Update()
    {
        //condicional caso a direção do eixo x seja diferente de 0
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
        //condicional caso a direção do eixo y seja diferente de 0
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        //utilização do for. com uma condicional para passar por todos os elementos da lista
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        //criação das variavéis float x e y, que são responsáveis por mexer todo o corpo da cobra(eixos x e y) junto com a movimentação no game
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        this.transform.position = new Vector2(x, y);
    }
    
    //método grow, que faz a cobra crescer quando pega a comida
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    
    //Criação do método ResetState
    public void ResetState()
    {
        //
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        // 
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        // 
        _segments.Clear();
        _segments.Add(this.transform);

        // 
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    //
    private void OnTriggerEnter2D(Collider2D other)
    {
        //
        if (other.tag == "Food") {
            Grow();
        } 
        //
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
