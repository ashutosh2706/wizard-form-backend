using AutoMapper;

namespace WizardFormBackend.Mappings
{
    public class MapperProfile<Source, Target> : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Source, Target>();
        }
    }
}
