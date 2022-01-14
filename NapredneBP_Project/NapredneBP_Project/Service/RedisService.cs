using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace NapredneBP_Project
{
    public class RedisService
    {
        private ConnectionMultiplexer _redis;

        public RedisService(IConfiguration config)
        {
        }

        public void Connect()
        {
            try
            {
                _redis = ConnectionMultiplexer.Connect("localhost");
            }
            catch (RedisConnectionException err)
            {
                Console.WriteLine(err.ToString());
            }
            Console.WriteLine("Connected to Redis");
        }

        public async Task Set(string usr, string value)
        {
            var result = _redis.GetDatabase().ListLeftPushAsync(usr, value);
        }

        public async Task<RedisValue[]> Get(string key)
        {
            long value = _redis.GetDatabase().ListLength(key);

            if (value != 0)
            {
                var allMovies = await _redis.GetDatabase().ListRangeAsync(key, 0, value - 1);

                return allMovies;
            }
            else return null;
            
        }
        public async Task DeleteMovies(string usr)
        {
            RedisValue[] result = await Get(usr);
            foreach (var obj in result)
            {
                await _redis.GetDatabase().ListRemoveAsync(usr, obj, 0);
            }
        }

        public async Task AddUser(string value)
        {
            var result = await _redis.GetDatabase().ListLeftPushAsync("allusers", value);
        }

        public async Task<RedisValue[]> GetAllUser()
        {
            long value = _redis.GetDatabase().ListLength("allusers");
            var users = await _redis.GetDatabase().ListRangeAsync("allusers", 0, value - 1);

            return users;
        }

        public async Task DeleteUser(string value)
        {
            RedisValue[] result = await GetAllUser();
            foreach(var obj in result)
            {
                if (obj == value)
                    await _redis.GetDatabase().ListRemoveAsync("allusers", value, 0);
            }
        }

        public async Task SetComments(string key, string usr, string comment)
        {
            var res = await GetComments(key, usr);
            if (!res.IsNull)
            {
                var result = _redis.GetDatabase().HashSetAsync(key, usr, res + "| " + comment); // + "~" + DateTime.Now
            }
            else { var result = _redis.GetDatabase().HashSetAsync(key, usr, comment); } // + "~" + DateTime.Now
        }

        public async Task<RedisValue> GetComments(string key, string usr)
        {
            var result = await _redis.GetDatabase().HashGetAsync(key, usr);

            //System.Diagnostics.Debug.WriteLine(result.ToString());
            return result;
        }

        public async Task<HashEntry[]> GetCommentsForMovie(string key)
        {
            var result = await _redis.GetDatabase().HashGetAllAsync(key);
            return result;
        }

    }
}