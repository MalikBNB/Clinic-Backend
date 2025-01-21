using Clinic.DataService.Data;
using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataService.Repositories
{
    public class RefreshTokensRepository : GenericRepository<RefreshToken>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<RefreshToken>> GetAllAsync(string[] includes = null)
        {
            try
            {
                return await dbSet.Where(rt => rt.Status == 1)
                                    .AsNoTracking()
                                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetAllAsync has generated an error", typeof(RefreshToken));
                return new List<RefreshToken>();
            }
        }

        public async Task<RefreshToken> GetByRefreshToken(string refreshToken)
        {
            try
            {
                return await dbSet.AsNoTracking().FirstOrDefaultAsync(rt => rt.Token.ToLower() == refreshToken.ToLower());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetByRefreshToken has generated an error", typeof(RefreshToken));
                return null;
            }
        }

        public async Task<bool> MarkRefreshTokenAsUsed(RefreshToken refreshToken)
        {
            try
            {
                var token =  await dbSet.AsNoTracking()
                                        .FirstOrDefaultAsync(r => r.Token.ToLower() == refreshToken.Token.ToLower());

                if (token == null) return false;

                token.IsUsed = refreshToken.IsUsed;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetByRefreshToken has generated an error", typeof(RefreshToken));
                return false;
            }
        }
    }
}
