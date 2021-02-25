using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager sharedInstance;


    //Lista de bloques que se tienen 
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    //Lista de bloques que están en la escena 
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    //Donde se genera los bloques
    public Transform levelStartPosition;

    private void Awake()
    {
        if (sharedInstance == null) {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {

    }


    //Agregar el bloque que vendrá
    public void AddLevelBlock() {
        //generando un número aleatorio
        int randomIdx = Random.Range(1, allTheLevelBlocks.Count);
        //creando la variable de tipo del bloque
        LevelBlock block;
        //inicializando la posicion en 0
        Vector3 spawnPosition = Vector3.zero;

        //Si es el primer bloque que se creará, entonces será el bloque número 0
        if (currentLevelBlocks.Count == 0)
        {
            //instanciando
            block = Instantiate(allTheLevelBlocks[0]);
            //la posición será en el GO que previamente hemos creado "levelStartPosition"
            spawnPosition = levelStartPosition.position;
        }
        else{ //Si no es el primero en instanciar 
            //el número random para instanciar
            block = Instantiate(allTheLevelBlocks[randomIdx]);
            //su punto de spawn será en el punto final del anterior block
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count-1].EndPoint.position;
        }

        //Acá decimos que todos los bloques serán hijos de este objeto
        block.transform.SetParent(this.transform, false);

        //Acá 
        
        Vector3 correction = new Vector3(spawnPosition.x - block.StartPoint.position.x, 
                        spawnPosition.y - block.StartPoint.position.y, 0);

        /*
         Vector3 correction = new Vector3(spawnPosition.x ,
                         spawnPosition.y , 0);
                    */

        block.transform.position = correction;
        currentLevelBlocks.Add(block);
    }


    //Remover el bloque que ya se pasó 
    public void RemoveLevelBlock() {
        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }


    //Borrar los bloques cuando el personaje muera
    public void RemoveAllLevelBlocks() {
        while (currentLevelBlocks.Count > 0) {
            RemoveLevelBlock();
        }
    }

    //Generar bloques iniciales
    public void GenerateInitialBlocks() {
        for (int i = 0; i < 2; i++) {
            AddLevelBlock();
        }
    }
    
}
