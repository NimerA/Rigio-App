using System.Collections.Generic;
using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
    public interface IMatchService
    {
		Task<List<Match>> GetMatches();
		Task<bool> CreateMatch(Match match);
		Task<Match> GetMatchById(int id);
		Task<bool> UpdateMatch(Match match);
		Task<bool> DeleteMatchById(int id);
    }
}
