using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projeto3
{
    class Program
    {
        static List<IEstoque> produtos = new List<IEstoque>();
        enum Menu { Listar = 1, Adicionar = 2, Remover = 3, Entrada = 4, Saida = 5, Sair = 6 }

        static void Main(string[] args)
        {
            Carregar(); //Chamando função para carregar dados
            bool Sair = false;
            while (!Sair) {
                Console.WriteLine("Sistema de Estoque");
                Console.WriteLine("1-Listar\n2-Adicionar\n3-Remover\n4-Entrada\n5-Saída\n6-Sair");
                string opStr = Console.ReadLine();
                int opInt = int.Parse(opStr);

                Menu option = (Menu)opInt;

                switch (option) {
                    case Menu.Listar:
                        Listagem(); //Chamando função para listar produtos cadastrados
                        break;
                    case Menu.Adicionar:
                        Cadastro();//Chamando função de cadastro de produtos
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Entrada:
                        Entrada(); //Chamar função para dar entrada
                        break;
                    case Menu.Saida:
                        Saida();//Chamar função para dar baixa
                        break;
                    case Menu.Sair:
                        Sair = true;
                        break;
                    default:
                        Sair = true;
                        break;
                }
                Console.Clear();
            }
        }
        //Função para listar Produtos
        static void Listagem(){
            Console.WriteLine("Lista de produtos");
            int i = 0;
            foreach (IEstoque produtos in produtos) {
                Console.WriteLine("ID:" + i);
                produtos.Exibir();
                i++;
            }
            Console.ReadLine();
        }

        static void Remover() {
            Listagem();
            Console.WriteLine("Digite o ID do Elemento que você quer excluir");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count) {
                produtos.RemoveAt(id);
                Salvar();
            }
        }
            
        //Função para cadastrar produtos
        static void Cadastro() {
            Console.WriteLine("Cadastro de Produto");
            Console.WriteLine("1-Produto Fisico\n2-Ebook\n3-Curso");
            string opStr = Console.ReadLine();
            int choose = int.Parse(opStr);

            switch (choose) { //Switch para cada tipo de produtos
                case 1:
                    CadastrarPFisicio();
                    break;
                case 2:
                    CadastrarPEbook();
                    break;
                case 3:
                    CadastrarPCurso();
                    break;
            }
        }

        static void Entrada() {
            Listagem();
            Console.WriteLine("Digite o ID do Elemento que você quer dar entrada");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarEntrada();
                Salvar();
            }
        }

         static void Saida() {
            Listagem();
            Console.WriteLine("Digite o ID do Elemento que você quer dar baixa");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarSaida();
                Salvar();
            }
        }
        //Cadastro especifico de produtos fisico
        static void CadastrarPFisicio() {
            Console.WriteLine("Cadastro produto fisico: ");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.WriteLine("Frete: ");
            float frete = float.Parse(Console.ReadLine());
            ProdutoFisico pf = new ProdutoFisico(nome, preco, frete);
            produtos.Add(pf);
            Salvar();
        }
        //Cadastro especifico de Ebook
        static void CadastrarPEbook()
        {
            Console.WriteLine("Cadastro Ebook: ");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();

            Ebook eb = new Ebook(nome, preco, autor);
            produtos.Add(eb);
            Salvar();
        }
        //Cadastro especifico de curso
        static void CadastrarPCurso()
        {
            Console.WriteLine("Cadastro Curso: ");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();

            Curso cs = new Curso(nome, preco, autor);
            produtos.Add(cs);
            Salvar();
        }

        static void Salvar() {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, produtos);

            stream.Close();
        }

        static void Carregar() {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();
            try
            {
                produtos = (List<IEstoque>)encoder.Deserialize(stream);
                if (produtos == null) {
                    produtos = new List<IEstoque>();
                }
            }
            catch (Exception e ){
                produtos = new List<IEstoque>();
            }

            stream.Close();
        }
    }
}
