using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        //Chamando o método RandomizePosition, que como o nome diz define uma posição aleatória
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        //Criado variavel bounds tipo Bounds, que define o tamanho da area em que aparecem aletoriamente as comidas
        Bounds bounds = this.gridArea.bounds;

        //utilização dos floats dos eixos x e y, que definem que a comida ira aparecer entre o mínimo de um eixo e o máximo de outro
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        //chamando as duas posições (x e y) para formar um novo ponto para criação da food
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Chamando o método RandomizePosition(após a colisão)
        RandomizePosition();
    }

}
