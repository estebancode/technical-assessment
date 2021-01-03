using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;
using Technical.Assessment.Domain.Interfaces;

namespace Technical.Assessment.Domain.Services
{
    public class RespondentService : IRespondentService, IDisposable
    {
        /// <summary>
        /// Repository's instance
        /// </summary>
        private readonly IRepository<Respondent> repository;

        const string INVALID_USER = "Invalid username or password";
        const string EXIST_USER = "Username is already exist";
        const string USER_DOES_NOT_EXIST = "Username does not exist";

        /// <summary>
        /// constructor's method
        /// </summary>
        /// <param name="repository"></param>
        public RespondentService(IRepository<Respondent> repository)
        {
            this.repository = repository;
        }

        public async Task<string> AuthenticaUserAsync(Respondent entity)
        {
            Respondent respondent = (await GetAllAsync(c=> c.Email == entity.Email,null,null,false).ConfigureAwait(false)).FirstOrDefault();
            if (respondent != null)
            {
                string password = ComputeSha256Hash(entity.HashedPassword);
                if (respondent.HashedPassword.ToUpper().Equals(password.ToUpper()))
                {
                    return GetToken(respondent);
                }
                else
                {
                    throw new InvalidOperationException(INVALID_USER);
                }
            }else
            {
                throw new InvalidOperationException(INVALID_USER);
            }
        }

        public async Task DeleteAsync(string email)
        {
            IEnumerable<Respondent> respondents = await GetAllAsync(c => c.Email == email, null, null, false).ConfigureAwait(false);
            if (respondents.Any())
            {
                Respondent entity = respondents.FirstOrDefault();
                repository.Delete(entity);
                await this.repository.SaveChangesAsync().ConfigureAwait(false);
            } else
            {
                throw new ArgumentNullException(USER_DOES_NOT_EXIST);
            }
        }

        public async Task<IEnumerable<Respondent>> GetAllAsync(Expression<Func<Respondent, bool>> filter, Func<IQueryable<Respondent>, IOrderedQueryable<Respondent>> orderBy, string includeProperties, bool isTracking)
        {
            return await this.repository.GetAllAsync(filter, orderBy, includeProperties, isTracking).ConfigureAwait(false);
        }

        public async Task<Respondent> GetAsync(int id)
        {
            return await this.repository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task InsertAsync(Respondent entity)
        {
            IEnumerable<Respondent> respondents = await GetAllAsync(c => c.Email == entity.Email, null, null, false).ConfigureAwait(false);
            if (!respondents.Any())
            {
                string password = ComputeSha256Hash(entity.HashedPassword);
                entity.HashedPassword = password;
                this.repository.Insert(entity);
                await this.repository.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                throw new InvalidOperationException(EXIST_USER);
            }
        }

        public async Task UpdateAsync(Respondent entity)
        {
            Respondent respondent = (await GetAllAsync(c => c.Email == entity.Email, null, null, false).ConfigureAwait(false)).FirstOrDefault();
            if (respondent != null)
            {
                respondent.Name = entity.Name;
                this.repository.Update(respondent);
                await this.repository.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentNullException(USER_DOES_NOT_EXIST);
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string GetToken(Respondent respondent)
        {
            string secret = "my_secret_key_999444222888555_TEAM_INTERNATIONAL"; //Secret key which will be used later during validation
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secret);

            ClaimsIdentity Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserName",respondent.Email==null?"":respondent.Email),
                    new Claim("UserId",respondent.Id.ToString()),
                    new Claim("Name",respondent.Name.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                });


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = Subject,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.repository.Dispose();
        }
    }
}
