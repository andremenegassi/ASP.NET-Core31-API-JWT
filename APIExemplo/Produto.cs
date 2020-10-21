using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIExemplo
{
    public class Produto
    {
        int _id;
        string _nome;

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }

        public Produto()
        {

        }

        public List<Produto> ObterTodos()
        {
            List<Produto> produtos = new List<Produto>();
            produtos.Add(new Produto()
            {
                Id = 1,
                Nome = "Dipirona"
            });

            produtos.Add(new Produto()
            {
                Id = 2,
                Nome = "Magnopirol"
            });

            return produtos;
        }

        public Produto Obter(int id)
        {
            List<Produto> produtos = ObterTodos();
            return produtos.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
