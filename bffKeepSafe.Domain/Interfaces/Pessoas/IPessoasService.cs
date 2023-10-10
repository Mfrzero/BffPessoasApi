using bffKeepSafe.Domain.Enums;
using bffKeepSafe.Domain.Models.Pessoas;

namespace bffKeepSafe.Domain.Interfaces.Pessoas
{
    public interface IPessoasService
    {
        Task<IEnumerable<PessoasResponse>> GetAllPessoas();
        Task<int> GetPessoasCount();
        Task<double> GetIdadeMedia();
        Task<IEnumerable<PessoasResponse>> GetPessoasPorCidade(string cidade);
        Task<IEnumerable<PessoasResponse>> GetMenoresDeIdade();
        Task<IEnumerable<PessoasResponse>> GetMaioresDeIdade();
        Task<IEnumerable<PessoasResponse>> GetPessoasComMesmoNome(string nome);
        Task<IEnumerable<PessoasResponse>> GetAllPorSexo(TipoSexo sexo);
        Task<int> GetTotalPorSexo(TipoSexo sexo);
    }
}
