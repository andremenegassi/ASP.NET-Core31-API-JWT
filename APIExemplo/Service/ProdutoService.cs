using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIExemplo.Service
{
    public class ProdutoService
    {
        private readonly AppSettings _appSettings;
        private readonly IMongoCollection<Models.Produto> _produtoCollection;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public ProdutoService(AppSettings appSetting, MongoClient client, IMongoDatabase database)
        {
            _appSettings = appSetting;
            _client = client;
            _database = database;

            _produtoCollection = database.GetCollection<Models.Produto>("Produtos");
        }

        public bool Salvar(Models.Produto produto)
        {
            _produtoCollection.InsertOne(produto);

            return true;

        }

        public IEnumerable<Models.Produto> ObterTodos()
        {
            IEnumerable<Models.Produto> produtos = 
                      _produtoCollection.AsQueryable().Where(p => p.Ativo).ToList();

            return produtos;

        }

        public void Excluir(string id)
        {
            var filter = Builders<Models.Produto>.Filter.Eq(p => p.Id, id);
            _produtoCollection.DeleteOne(filter);
        }

        public void Atualizar(Models.Produto produto)
        {
            var filter = Builders<Models.Produto>.Filter.Eq(p => p.Id, produto.Id);
            var update = Builders<Models.Produto>.Update
                .Set(p => p.Nome, produto.Nome)
                .Set(p => p.Preco, produto.Preco)
                .Set(p => p.Categorias, produto.Categorias);

            _produtoCollection.UpdateOne(filter, update);
        }

    }
}
