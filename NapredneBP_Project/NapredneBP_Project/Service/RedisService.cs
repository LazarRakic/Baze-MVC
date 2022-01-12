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
            var result = _redis.GetDatabase().StringSet("mykey", "c");
            var result1 = _redis.GetDatabase().StringSet("mykey1", "a");
            var result2 = _redis.GetDatabase().StringSet("mykey2", "b");
        }

        public async Task Get(string key)
        {
            string value =  _redis.GetDatabase().StringGet("mykey");
            Console.WriteLine(value);
        }
    }
}