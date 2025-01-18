using FitTrack.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.DL.Interfaces
{
    public interface ISubscriptionRepository
    {
        IEnumerable<Subscription> GetAll();
        Subscription GetById(string id);
        void Create(Subscription entity);
        void Update(string id, Subscription entity);
        void Delete(string id);
    }
}
