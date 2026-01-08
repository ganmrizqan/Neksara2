using NeksaraArief.Models;

namespace NeksaraArief.Service.Interfaces
{
    public interface ITestimoniService
    {
        void Submit(Testimoni testimoni);
        List<Testimoni> GetApproved();

        List<Testimoni> GetPending(
            string search,
            string sort,
            int? rating
        );

        void Approve(int id);
        void Reject(int id);
    }
}