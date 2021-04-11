using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.Repositories
{
    public class RankingRepository : IRankingRepository
    {
        private readonly PokerContext _context;
        private List<RankingOverallDTO> _rankingOveralls = new List<RankingOverallDTO>();
        private SqlConnection _connection;
        private SqlCommand _command;

        public RankingRepository(PokerContext context)
        {
            _context = context;
        }

        public void Add(Ranking ranking)
        {
            _context.Ranking.Add(ranking);
        }

        public void Update(Ranking ranking)
        {
            _context.Ranking.Update(ranking);
        }

        public async Task<IEnumerable<Ranking>> GetAllAsync()
        {
            return await _context.Ranking.AsNoTracking().ToListAsync();
        }

        public async Task<Ranking> GetByIdAsync(Guid id)
        {
            return await _context.Ranking.FindAsync(id);
        }

        public async Task<IEnumerable<RankingOverallDTO>> GetOverallById(Guid id)
        {
            var query = @"SELECT T1.[Name], T3.[Description], T2.[Position], T2.[Punctuation]
                        FROM Players T1 (NOLOCK)
                        INNER JOIN RoundsPunctuations T2 (NOLOCK) ON T1.Id = T2.PlayerId
                        INNER JOIN Rounds T3 (NOLOCK) ON T2.RoundId = T3.Id
                        INNER JOIN Ranking T4 (NOLOCK) ON T3.RankingId = T4.Id
                        WHERE T4.Id = @RankingId";

            var parameters = new Dictionary<string, object>();
            parameters.Add("@RankingId", id);
            var reader = await ExecuteQuerySqlServer(query, parameters);
            MappingDataReaderToRankingOverallDTO(reader);
            CloseConnectionSqlServer();

            return _rankingOveralls;
        }

        public async Task<IEnumerable<RankingOverallDTO>> GetOverallByPeriod(DateTime initialDate, DateTime finalDate)
        {
            var query = @"SELECT T1.[Name], T3.[Description], T2.[Position], T2.[Punctuation]
                        FROM Players T1 (NOLOCK)
                        INNER JOIN RoundsPunctuations T2 (NOLOCK) ON T1.Id = T2.PlayerId
                        INNER JOIN Rounds T3 (NOLOCK) ON T2.RoundId = T3.Id
                        WHERE T3.[Date] >= @InitialDate AND T3.[Date] <= @FinalDate";

            var parameters = new Dictionary<string, object>();
            parameters.Add("@InitialDate", initialDate);
            parameters.Add("@FinalDate", finalDate);
            var reader = await ExecuteQuerySqlServer(query, parameters);
            MappingDataReaderToRankingOverallDTO(reader);
            CloseConnectionSqlServer();

            return _rankingOveralls;
        }

        private async Task<SqlDataReader> ExecuteQuerySqlServer(string query, IDictionary<string, object> parameters)
        {
            _connection = new SqlConnection(_context.Database.GetConnectionString());
            _connection.Open();

            var _command = new SqlCommand(query, _connection);
            parameters.Select(x => _command.Parameters.Add(new SqlParameter(x.Key, x.Value)));

            return await _command.ExecuteReaderAsync();
        }

        private void MappingDataReaderToRankingOverallDTO(SqlDataReader reader)
        {
            while (reader.Read())
            {
                var name = reader["Name"].ToString();
                var rankingOverall = _rankingOveralls.Where(x => x.Name == name).FirstOrDefault();
                if (rankingOverall == null) rankingOverall = new RankingOverallDTO();

                rankingOverall.Name = name;
                rankingOverall.Punctuations.Add(new PunctuationOverallDTO()
                {
                    Description = reader["Description"].ToString(),
                    Position = Convert.ToInt16(reader["Position"].ToString()),
                    Punctuation = Convert.ToInt16(reader["Punctuation"].ToString())
                });

                if (!_rankingOveralls.Any(x => x.Name == name)) _rankingOveralls.Add(rankingOverall);
            }
        }

        private void CloseConnectionSqlServer()
        {
            _command.Clone();
            _connection.Close();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
