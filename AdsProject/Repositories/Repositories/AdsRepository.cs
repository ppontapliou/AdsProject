using ConnectionModels;
using Microsoft.Extensions.Options;
using Models.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class AdsRepository : Repository, IAdsRepository
    {
        public AdsRepository(IOptions<DBConfig> config) : base(config.Value)
        {

        }
        public void AddAdAsync(Ad ad)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", ad.Name),
                new SqlParameter("@Title", ad.Title),
                new SqlParameter("@DateCreation", DateTime.Now),
                new SqlParameter("@Image", ad.Image),
                new SqlParameter("@Category", ad.Category),
                new SqlParameter("@Type",ad.Type),
                new SqlParameter("@State",ad.State),
                new SqlParameter("@Address",ad.Address),
                new SqlParameter("@UserId",ad.UserId),
            };
            SendRequest("EXEC AddAd @Name, @Category, @Type, @State, @Title, @DateCreation, @UserId, @Address, @Image", sqlParameters);
        }

        public void ChangAdAsync(Ad ad)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id",ad.Id),
                new SqlParameter("@Name", ad.Name),
                new SqlParameter("@Title", ad.Title),
                new SqlParameter("@DateCreation", DateTime.Now),
                new SqlParameter("@Image", ad.Image),
                new SqlParameter("@Category", ad.Category),
                new SqlParameter("@Type", ad.Type),
                new SqlParameter("@State", ad.State),
                new SqlParameter("@Address",ad.Address),
            };
            SendRequest("EXEC ChangeAd @Id, @Name, @Category, @Type, @State, @Title, @DateCreation, @Address, @Image", sqlParameters);
        }

        public Task<int> CheckAdAsync(int id, int userId)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@AdId", id),
                new SqlParameter("@UserId", userId),
            };
            return ReturnScalar("EXEC CheckAd @AdId, @UserId", sqlParameters);
        }

        public void DeleteAdAsync(int id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@AdId", id),
            };
            SendRequest("EXEC DeleteAd @AdId", sqlParameters);
        }

        public async Task<Ad> GetAdAsync(int id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@AdId", id),
            };
            List<Ad> ads = await ReturnData<Ad>("EXEC GetAd @AdId", sqlParameters);
            if (ads.Count == 1)
            {
                return ads[0];
            }
            return null;
        }

        public async Task<List<Ad>> GetAdsAsync(Ad ad)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                (string.IsNullOrEmpty(ad.Name))?new SqlParameter("@Name", DBNull.Value):new SqlParameter("@Name", ad.Name),
                (ad.Category==0)?new SqlParameter("@Category", DBNull.Value):new SqlParameter("@Category", ad.Category),
                (ad.Type==0)?new SqlParameter("@Type", DBNull.Value):new SqlParameter("@Type", ad.Type),
                (ad.State==0)?new SqlParameter("@State", DBNull.Value):new SqlParameter("@State", ad.State)
            };
            return await ReturnData<Ad>("exec getads @Name, @Category, @Type, @State", sqlParameters);
        }
    }
}
