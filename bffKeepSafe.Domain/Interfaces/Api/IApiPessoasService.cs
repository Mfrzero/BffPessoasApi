using bffKeepSafe.Domain.Models.Pessoas;

namespace bffKeepSafe.Domain.Interfaces.Api
{
    public interface IApiPessoasService
    {
        Task<IEnumerable<PessoasResponse>> GetApiResponseAsync();
    }
}
