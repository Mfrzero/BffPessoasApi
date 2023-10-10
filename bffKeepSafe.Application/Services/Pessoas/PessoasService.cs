using bffKeepSafe.Domain.Enums;
using bffKeepSafe.Domain.Interfaces.Api;
using bffKeepSafe.Domain.Interfaces.Pessoas;
using bffKeepSafe.Domain.Models.Pessoas;

namespace bffKeepSafe.application.Services.Pessoas
{
    public class PessoasService : IPessoasService
    {
        private readonly IApiPessoasService _pessoas;

        public PessoasService(IApiPessoasService pessoas)
        {
            _pessoas = pessoas;
        }

        public async Task<IEnumerable<PessoasResponse>> GetAllPessoas()
        {
            try
            {
                var response = await _pessoas.GetApiResponseAsync();
                return response; 
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
        
        public async Task<int> GetPessoasCount()
        {
            try
            {
                var response = await _pessoas.GetApiResponseAsync();
                return response.Count(); 
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public async Task<double> GetIdadeMedia()
        {
            try
            {
                var result = await _pessoas.GetApiResponseAsync();
                return result.Average(p => p.Idade);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<IEnumerable<PessoasResponse>> GetPessoasPorCidade(string cidade)
        {
            try
            {
                var result = await _pessoas.GetApiResponseAsync();
                return result.Where(p => p.Cidade == cidade).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<IEnumerable<PessoasResponse>> GetMenoresDeIdade()
        {
            try
            {
                var result = await _pessoas.GetApiResponseAsync();
                return result.Where(p => p.Idade < 18).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PessoasResponse>> GetMaioresDeIdade()
        {
            try
            {
                IEnumerable<PessoasResponse> result = await _pessoas.GetApiResponseAsync();
                return result.Where(p => p.Idade >= 18).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<PessoasResponse>> GetPessoasComMesmoNome(string nome)
        {
            try
            {
                IEnumerable<PessoasResponse> result = await _pessoas.GetApiResponseAsync();
                return result.Where(p => p.Nome == nome).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public async Task<IEnumerable<PessoasResponse>> GetAllPorSexo(TipoSexo sexo)
        {
            try
            {
                IEnumerable<PessoasResponse> result = await _pessoas.GetApiResponseAsync();
                return result.Where(p => p.Sexo == sexo).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<int> GetTotalPorSexo(TipoSexo sexo)
        {
            try
            {
                IEnumerable<PessoasResponse> result = await _pessoas.GetApiResponseAsync();
                return result.Where(p => p.Sexo == sexo).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
