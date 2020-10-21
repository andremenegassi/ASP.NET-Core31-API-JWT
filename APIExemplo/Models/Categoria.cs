using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIExemplo.Models
{
    public class Categoria
    {
        int _id;
        string _nome;

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }

    }
}
