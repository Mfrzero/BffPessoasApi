using bffKeepSafe.Domain.Enums;
using bffKeepSafe.Domain.Interfaces.Pessoas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bffKeepSafe.Api.Controllers
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoasService _pessoasService;

        public PessoasController(IPessoasService pessoasService)
        {
            _pessoasService = pessoasService;
        }

        [HttpGet]
        public async Task<IActionResult> Pessoas()
        {
            var result = await _pessoasService.GetAllPessoas();
            return Ok(result);
        }

        [HttpGet("total")]
        public async Task<IActionResult> PessoasCount()
        {
            var result = await _pessoasService.GetPessoasCount();
            return Ok(result);
        }

        [HttpGet("media_idades")]
        public async Task<IActionResult> PessoasMediaDeIdades()
        {
            var result = await _pessoasService.GetIdadeMedia();
            return Ok(result);
        }

        [HttpGet("cidade")]
        public async Task<IActionResult> GetPessoasPorCidade(string cidade)
        {
            var result = await _pessoasService.GetPessoasPorCidade(cidade);
            return Ok(result);
        }

        [HttpGet("menores/")]
        public async Task<IActionResult> GetMenoresDeIdade()
        {
            var result = await _pessoasService.GetMenoresDeIdade();
            return Ok(result);
        }

        [HttpGet("maiores")]
        public async Task<IActionResult> GetMaioresDeIdade()
        {
            var result = await _pessoasService.GetMaioresDeIdade();
            return Ok(result);
        }

        [HttpGet("nome")]
        public async Task<IActionResult> GetPessoasComMesmoNome(string nome)
        {
            var result = await _pessoasService.GetPessoasComMesmoNome(nome);
            return Ok(result);
        }

        [HttpGet("sexo")]
        public async Task<IActionResult> GetPorSexo(TipoSexo sexo)
        {
            var result = await _pessoasService.GetAllPorSexo(sexo);
            return Ok(result);
        }

        [HttpGet("total_sexo")]
        public async Task<IActionResult> GetTotalPorSexo(TipoSexo sexo)
        {
            var result = await _pessoasService.GetTotalPorSexo(sexo);
            return Ok(result);
        }
    }
}
