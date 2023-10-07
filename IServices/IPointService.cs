using AutoMapper.Execution;
using CodeTest.Models;

namespace CodeTest.IServices
{
    public interface IPointService
    {
        public Task<MemberDetail> CalculatePoint(Purchase purchase);
    }
}
