using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

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

        public async Task Set(string key, string value)
        {
            var result = _redis.GetDatabase().StringSet(key, value, TimeSpan.FromSeconds(60));
        }

        public async Task<string> Get(string key)
        {
            string value =  _redis.GetDatabase().StringGet(key);

            return value;
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
    }
}