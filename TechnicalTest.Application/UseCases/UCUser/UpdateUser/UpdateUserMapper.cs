using AutoMapper;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.UseCases.UCUser.UpdateUser
{
    public sealed class UpdateUserMapper : Profile
    {
        public UpdateUserMapper()
        {
            CreateMap<UpdateUserRequest, User>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<User, UpdateUserResponse>();
        }
    }
}
