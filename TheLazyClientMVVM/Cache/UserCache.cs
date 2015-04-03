using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.Cache
{
    public class UserCache
    {
        private List<UserEntity> users { get; set; }
        public UserCache()
        {
            users = new List<UserEntity>();
        }
        public UserEntity getUser(int id, bool direct = false)
        {
            Predicate<UserEntity> match = new Predicate<UserEntity>(e => id == e.id);
            UserEntity cached = users.FirstOrDefault(e => match(e) && e.isValid());
            if (cached != null && !direct) 
            {
                Com.main.cached_query_count++;
                return cached;
            }
            else
            {
                UserEntity tocache = DbClient.DbUserClient.getUserInfo(id);
                users.RemoveAll(match);
                users.Add(tocache);
                return tocache;
            }
        }
        public UserEntity getUser(string username, bool direct = false)
        {
            Predicate<UserEntity> match = new Predicate<UserEntity>(e => username == e.username);
            UserEntity cached = users.FirstOrDefault(e => match(e) && e.isValid());
            if (cached != null && !direct)
            {
                Com.main.cached_query_count++;
                return cached;
            }
            else
            {
                UserEntity tocache = DbClient.DbUserClient.getUserInfo(username);
                users.RemoveAll(match);
                users.Add(tocache);
                return tocache;
            }
        }
    }
}
