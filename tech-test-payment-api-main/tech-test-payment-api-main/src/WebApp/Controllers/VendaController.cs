using Dominio.Enum;
using Dominio.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    /// <summary>
    /// Gestão de Vendas
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class VendaController : ControllerBase
    {
        private readonly VendaServico _vendaServico;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="vendaServico">Classe de Serviço</param>
        public VendaController(VendaServico vendaServico)
        {
            _vendaServico = vendaServico;
        }

        /// <summary>
        /// Retorna a venda de id informado.
        /// </summary>
        /// <param name="id">Id da venda</param>
        /// <returns>Venda de id informado.</returns>
        /// <response code="200">Venda encontrada.</response>
        /// <response code="404">Venda não encontrada.</response>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var venda = await _vendaServico.BuscarVenda(id);
                return Ok(venda);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Altera o status da venda.
        /// </summary>
        /// <param name="id">Id da venda.</param>
        /// <param name="status">
        /// Novo status da venda:
        /// 1 - Reservado não utilizar.
        /// 2 - Pagamento Aprovado.
        /// 3 - Enviado para transportadora.
        /// 4 - Entregue.
        /// 5 - Cancelada.
        /// </param>
        /// <returns>Nenhum conteúdo.</returns>
        /// <response code="204">Aleração realizada.</response>
        /// <response code="400">Alteração não realizada.</response>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int? id, int? status)
        {
            if (id == null || status == null || status > 5)
            {
                return BadRequest();
            }
            try
            {
                var statusVenda = (StatusVendaEnum)Enum.ToObject(typeof(StatusVendaEnum), status.Value);
                var venda = await _vendaServico.BuscarVenda(id);
                await _vendaServico.AtualizarVenda(venda, statusVenda);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Registra uma nova venda.
        /// </summary>
        /// <param name="model">Objeto contendo os dados da venda.</param>
        /// <returns>200 caso a venda seja registrada com sucessso.</returns>
        /// <response code="200">Venda registrada.</response>
        /// <response code="400">Venda não registrada.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]VendaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var vendedor = model.ObterVendedor();
                var itensVenda = model.ObterItensVenda();
                await _vendaServico.RegistrarVenda(vendedor, itensVenda);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
