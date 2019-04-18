using MongoDB.GenericRepository.MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Models
{
    public class User : EntityBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
