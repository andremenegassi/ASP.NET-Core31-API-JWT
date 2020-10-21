using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIExemplo.Controllers
{
    [Authorize("ValidaUsuario")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly Service.ProdutoService _produtoService;

        public ProdutoController(Service.ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        //[HttpGet("[action]")]
        //public IActionResult ObterTodos()
        //{
        //    Produto p = new Produto();

        //    return Ok(p.ObterTodos());
        //}


        [HttpPost("[action]")]
        public IActionResult Adicionar([FromBody] System.Text.Json.JsonElement item)
        {
            string x = item.GetProperty("item").ToString();
          
            return Ok(true);
        }

        [HttpGet("[action]")]
        public IActionResult Salvar()
        {
            Models.Produto p = new Models.Produto();
            p.Nome = "Abacaxi";
            p.Ativo = true;

            List<Models.Categoria> cats = p.Categorias.ToList();
            cats.Add(new Models.Categoria() {
                Id = 1,
                Nome = "Frutas"
            });
            cats.Add(new Models.Categoria()
            {
                Id = 2,
                Nome = "Legumes"
            });

            p.Categorias = cats;

            _produtoService.Salvar(p);

            return Ok(true);
        }

        [HttpGet("[action]")]
        public IActionResult ObterTodos()
        {
            return Ok(_produtoService.ObterTodos());
        }


        [HttpGet("[action]")]
        public IActionResult Excluir(string id)
        {
            _produtoService.Excluir(id);
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult Atualizar(string id)
        {

            Models.Produto p = new Models.Produto();
            p.Id = id;
            p.Nome = "Abacaxi Alterado sddsadsadsad";
            p.Preco = 30000;
            List<Models.Categoria> cats = p.Categorias.ToList();
            cats.Add(new Models.Categoria()
            {
                Id = 2,
                Nome = "Legumes"
            });
            p.Categorias = cats;
            _produtoService.Atualizar(p);
   
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult Obter(int id)
        {
            Produto p = new Produto();
            Produto pAchado = p.Obter(id);

            if (pAchado != null)
                return Ok(pAchado);
            else return NotFound(new
            {
                msg = "O produto " + id + " não foi encontrado"
            });
        }


    }
}