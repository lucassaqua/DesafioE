using DesafioE.Domain;
using DesafioE.Models;
using AutoMapper;

namespace DesafioE.Mappers
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Proxys, DBRegisterModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartTimeExecution,
                    opt => opt.MapFrom(src => src.StartTimeExecution))
                .ForMember(dest => dest.ExecutionEndTime,
                    opt => opt.MapFrom(src => src.ExecutionEndTime))
                .ForMember(dest => dest.NumberOfPages,
                        opt => opt.MapFrom(src => src.NumberOfPages))
                .ForMember(dest => dest.NumberOfLines,
                    opt => opt.MapFrom(src => src.NumberOfLines))
                .ForMember(dest => dest.ListToSave,
                        opt => opt.MapFrom(src => src.ListToSave));
                    
        }
    }
}
