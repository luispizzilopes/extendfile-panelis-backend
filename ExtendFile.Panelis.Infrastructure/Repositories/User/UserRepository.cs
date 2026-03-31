using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.User;
using ExtendFile.Panelis.Domain.Modules.User.Entities;
using ExtendFile.Panelis.Infrastructure.Context;
using ExtendFile.Panelis.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExtendFile.Panelis.Infrastructure.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PaginedResult<Domain.Modules.User.Entities.User>> GetUsersAsync(PaginationParams paginationParams, string? name, string? email, CancellationToken cancellationToken)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(u => u.Name.Contains(name));
        
        if (!string.IsNullOrWhiteSpace(email))
            query = query.Where(u => u.Email!.Contains(email));

        return await query
            .OrderBy(u => u.Name)
            .PaginationAsync(paginationParams, cancellationToken);
    }

    public async Task<Domain.Modules.User.Entities.User?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<Domain.Modules.User.Entities.User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task CreateAsync(Domain.Modules.User.Entities.User user, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Users.AddAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar o usuário. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Domain.Modules.User.Entities.User user, CancellationToken cancellationToken)
    {
        try
        {
            _context.Users.Update(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao atualizar o usuário. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await GetByIdAsync(id, cancellationToken);
            
            if (user is null)
            {
                _logger.LogWarning("Usuário com id {UserId} não encontrado.", id);
                return;
            }

            _context.Users.Remove(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao excluir o usuário com id {UserId}. Erro: {Message}", id, ex.Message);
            throw;
        }
    }
}
