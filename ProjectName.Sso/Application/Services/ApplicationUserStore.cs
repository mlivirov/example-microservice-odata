﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectName.Sso.Application.Database;
using ProjectName.Sso.Models;

namespace ProjectName.Sso.Application.Services
{
    public class ApplicationUserStore : IApplicationUserStore
    {
        private readonly DatabaseContext _databaseContext;

        public ApplicationUserStore(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public void Dispose()
        {
        }
        
        public async Task<bool> ValidateCredentials(string username, string password)
        {
            var user = await FindByNameAsync(username, CancellationToken.None);
            if (user == null)
            {
                return false;
            }

            var hashedPassword = GetHashedPassword(password, user.PasswordSalt);
            return hashedPassword == user.Password;
        }
        
        private string GetHashedPassword(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);

            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        }

        private string GeneratePasswordSalt()
        {
            var saltSize = 64;
            var salt = new byte[saltSize];
            var cryptoProvider = new RNGCryptoServiceProvider();
            cryptoProvider.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt);
        }
        
        public IEnumerable<Claim> GetClaims(User user)
        {
            yield return new Claim(ClaimTypes.Name, user.Username);
        }

        public async Task<User> FindByExternalProviderAsync(string provider, string providerUserId)
        {
            var userExternalProvider = await _databaseContext.UserExternalProviders.FirstOrDefaultAsync(t =>
                t.Provider == provider && t.ProviderUserId == providerUserId, CancellationToken.None);

            if (userExternalProvider == null)
            {
                return null;
            }
            
            var user = await _databaseContext.Users.FirstOrDefaultAsync(t => 
                t.UserId == userExternalProvider.UserId);
            
            return user;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var existingUser = await FindByNameAsync(user.Username, cancellationToken);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = AccountOptions.UsernameAllreadyExistsErrorMessage,
                });
            }

            user.PasswordSalt = GeneratePasswordSalt();
            user.Password = GetHashedPassword(user.Password, user.PasswordSalt);
            user.Guid = Guid.NewGuid().ToString();
            
            try
            {
                await _databaseContext.Users.AddAsync(user, cancellationToken);
                await _databaseContext.SaveChangesAsync(cancellationToken);
            }
            // TODO: catch only unique key violations here, all other exceptions must pop up.
            catch (SqlException e)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = AccountOptions.UsernameAllreadyExistsErrorMessage,
                });
            }
            
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _databaseContext.Users.FirstOrDefaultAsync(t => t.Guid == userId);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _databaseContext.Users.FirstOrDefaultAsync(t => t.Username == normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Guid);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}