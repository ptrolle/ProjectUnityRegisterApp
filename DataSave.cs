using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave
{
	public static List<string> listCategory = new List<string>(); //para salvar lista de categorias

	public static int maxCategory = 20; //limite de categorias

	//SAVEVALUES salva no mes e categoria selecionada valor incluido 
	public static void SaveValues(string p_key, string p_category,float p_value, string p_description)
	{
		Debug.Log("VALOR: R$ "+p_value);
		string _aux = p_key + p_category; //_aux recebe MES, ANO E CATEGORIA, por exemplo (72018FARMACIA)

	
		if(p_value > 0) //se o valor gasto é maior que zero pode SALVAR
		{
			float _checkCategory = PlayerPrefs.GetFloat(_aux); //verifica se esta key _aux ja existem algum valor salvo

			if(_checkCategory == 0) // caso nao, vai salvar
			{
				Debug.Log("entrou aqui vazio");
				PlayerPrefs.SetFloat(_aux, p_value); //salva na key selecionada o valor gasto;
			}
			else
			{
				Debug.Log("key encontrada salvar valor");
				float _checkKey = PlayerPrefs.GetFloat(_aux); //carrega valor de gasto mensal da possivel categoria
				Debug.Log("valor até entao na categoria:"+ _checkKey);
				_checkKey = _checkKey + p_value; //incrementa o novo valor ao que ja existe
				Debug.Log("mostra novo valor:" + _checkKey); 
				PlayerPrefs.SetFloat(_aux, _checkKey); //salva o novo valor na categoria correta
			}

			//ex: (72018FARMACIA0) = SALVA DESCRICAO, (70218FARMACIA1) = SALVA VALOR DAQUELA DESCRICAO
			PlayerPrefs.SetString(_aux+"0", p_description); //(salva a ultima descrição da categoria e mes selecionado
			PlayerPrefs.SetFloat(_aux+"1", p_value); //salva o valor da ultima descrição da categoria e mes selecionado
		}
		else
		{
			Debug.Log("nao salvou pois o valor é zero ou negativo");
		}

	}

	/*public static void SaveDescription(string p_category, string p_description, )
	{
		
	}*/

	public static void SaveCategory(string p_key, string p_category)
	{
		PlayerPrefs.SetString(p_key, p_category);
	}


	public static void LoadPlayerPrefabsDatas(string p_key)
	{
		float tempValue = PlayerPrefs.GetFloat(p_key);
		Debug.Log(tempValue);
	}

	public static void SaveDatasCategory(int p_type)
	{
		if(p_type == 0) //SALVA NA MEMORIA E NA LISTA, CATEGORIAS INICIAIS /******************************************/
		{
			//lista inicial de categoria
			listCategory.Add("MERCADO");
			listCategory.Add("POSTO");
			listCategory.Add("FARMACIA");
			listCategory.Add("LUZ");
			listCategory.Add("AGUA");
			listCategory.Add("ALUGUEL");
			listCategory.Add("TELEFONE");
			listCategory.Add("TV");

			for(int i=0; i < listCategory.Count; i++) //for para percorrer listas salvando as categorias iniciais
			{
				Debug.Log("C"+i+" : "+ listCategory[i]); 
				PlayerPrefs.SetString("C"+i, listCategory[i]); //percorre lista para salvar categorias existentes
		 	} 
		}
		else if(p_type == 1) //QUANDO FECHA APP E ABRI NOVAMENTE, CARREGAVA VALORES NA MEMORIA DE CATEGORIAS PARA LISTA DE CATEGORIAS /******************************************/
		{
			for(int i=0; i <= maxCategory; i++) //percorrendo um limite de 50 categorias ao max existentes
			{
				if(PlayerPrefs.HasKey("C"+i)) //testando se existe ainda key neste valor de i
				{
					string __auxCategory = PlayerPrefs.GetString("C"+i); //caso exista coloca em uma string temporaria para alimentar a lista de categorias
					listCategory.Add(__auxCategory); //alimenta lista de categorias
					Debug.Log(__auxCategory);
				}
				else
				{
					break; // se nao existe mais quebra (for)
				}
			}
		}
		else if(p_type == 2) //quando deleta uma categoria /******************************************/
		{
			for(int i=0; i <= maxCategory; i++) //primeiro for para deletar todas key categorias para que sejam salvos as novas logo abaixo no prox for
			{
				if(PlayerPrefs.HasKey("C"+i)) //testando se existe key neste valor de i para deletar
				{
					Debug.Log("ENTROU AQUI 1 >>>>>>>>>>>>>>>: C"+i);
					PlayerPrefs.DeleteKey("C"+i);
				}
				else
				{
					break;
				}

			}


			for(int i=0; i < listCategory.Count; i++) //for para percorrer listas de categorias
			{
				Debug.Log("C"+i+" : "+ listCategory[i]); 
				PlayerPrefs.SetString("C"+i, listCategory[i]); //percorre lista para salvar categorias existentes
		 	}
		}
		else //para quando adiciona uma nova categoria /******************************************/
		{
			for(int i=0; i < listCategory.Count; i++) //for para percorrer listas de categorias
			{
				Debug.Log("C"+i+" : "+ listCategory[i]); 
				PlayerPrefs.SetString("C"+i, listCategory[i]); //percorre lista para salvar categorias existentes
		 	}
		}
	}
}
