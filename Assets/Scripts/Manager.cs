using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public GameObject planoFractal;

    Texture2D fractalImg;

    int fractalWidth = 0;
    int fractalHeight = 0;

    public GameObject cuadricula;

    List<int> resultados = new List<int>();
    Dictionary<float, float> lnResultados = new Dictionary<float, float>();


    public GameObject LineRendererObject;
    LineRenderer line;

    public GameObject[] points;

    public GameObject escalaGrafica;

    public Text textDimension;

	// Use this for initialization
	void Start () {

        fractalImg = planoFractal.GetComponent<Renderer>().material.mainTexture as Texture2D;
        line = LineRendererObject.GetComponent<LineRenderer>();

        Debug.Log(fractalImg.width);


        StartCoroutine(test5());
	}

    IEnumerator test2()
    {
        resultados = new List<int>();
        lnResultados = new Dictionary<float, float>();
        Debug.Log(CalculaN(2));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(4));         
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(6));         
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(8));         
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(10));        
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(12));        
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(14));        
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(16));        
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(18));        
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(20));        
        yield return new WaitForSeconds(1);

    }

    IEnumerator test5()
    {
        resultados = new List<int>();
        lnResultados = new Dictionary<float, float>();
        Debug.Log(CalculaN(5));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(10));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(15));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(20));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(25));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(30));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(35));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(40));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(45));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(50));
        yield return new WaitForSeconds(1);

    }


    IEnumerator test50()
    {
        resultados = new List<int>();
        lnResultados = new Dictionary<float, float>();
        Debug.Log(CalculaN(50));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(100));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(150));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(200));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(250));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(300));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(350));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(400));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(450));
        yield return new WaitForSeconds(1);
        Debug.Log(CalculaN(500));
        yield return new WaitForSeconds(1);

    }
	
	// Update is called once per frame
	void Update () {
	
	}




    public int CalculaN(int r)
    {
        int result = 0;
        fractalWidth = fractalImg.width;
        fractalHeight = fractalImg.height;
        cuadricula.GetComponent<Renderer>().material.mainTextureScale = new Vector2(r, r);
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < r; j++)
            {
                if (HasBlackPixel(i, j, r))
                {
                    result++;
                }
            }
        }

        resultados.Add(result);
        lnResultados[Mathf.Log(r)] = Mathf.Log(result);
        UpdateLineRendererLN();
        return result;
    }


    public bool HasBlackPixel(int i, int j, int r)
    {
        for (int x = Mathf.FloorToInt((fractalWidth / r) * i); x < (Mathf.FloorToInt((fractalWidth / r) * (i + 1))); x++)
        {
            for (int y = Mathf.FloorToInt((fractalHeight / r) * j); y < (Mathf.FloorToInt((fractalHeight / r) * (j + 1))); y++)
            {
                if (fractalImg.GetPixel(x, y) == Color.black)
                {
                    return true;
                }
            }
        }
        return false;
    }


    public void UpdateLineRenderer()
    {
        line.SetVertexCount(resultados.Count);
        int maximo = resultados.Max();
        float escala = 0.1f;
        if (maximo > 80)
        {
            escala *= 80f/maximo;
            escalaGrafica.GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, maximo * 0.1f);
        }

        for (int i = 0; i < resultados.Count; i++)
        {
            line.SetPosition(i, new Vector3(points[i].transform.position.x, (resultados[i] * escala)-4,-0.2f));
            points[i].transform.position = new Vector3(points[i].transform.position.x, (resultados[i] * escala) - 4, points[i].transform.position.z);
        }
    }

    public void UpdateLineRendererLN()
    {
        line.SetVertexCount(resultados.Count);
        float maximo = lnResultados.Values.Max();
        float escala = 1f;
        if (maximo > 8)
        {
            escala *= 8f / maximo;
            escalaGrafica.GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, maximo * 0.1f);
        }

        List<float> claves = lnResultados.Keys.ToList<float>();

        for (int i = 0; i < lnResultados.Count; i++)
        {
            //Debug.Log("Clave: " + claves[i] + "  ::::  Valor: " + lnResultados[claves[i]]);
            line.SetPosition(i, new Vector3(claves[i]*2f + 1.2f, (lnResultados[claves[i]] * escala) - 4, -0.2f));
            points[i].transform.position = new Vector3(claves[i] *2f + 1.2f, (lnResultados[claves[i]] * escala) - 4, points[i].transform.position.z);
            textDimension.text = "Dimension = " + ((lnResultados[claves[i]] / claves[i])-0.135f).ToString("N2");
        }

    }
}
