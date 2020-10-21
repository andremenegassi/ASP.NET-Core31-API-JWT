using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIExemplo.Models
{
    public class Produto
    {
        string _id;
        string _nome;
        bool _ativo;
        decimal? _preco;
        IEnumerable<Categoria> _categorias;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; } 
        public bool Ativo { get => _ativo; set => _ativo = value; }
        
        [BsonIgnoreIfNull]
        public decimal? Preco { get => _preco; set => _preco = value; }

        public IEnumerable<Categoria> Categorias { get => _categorias; set => _categorias = value; }


        public Produto()
        {
            _categorias = new List<Categoria>();
        }
    }
}
